using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SharedHome.Domain.Persons;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Identity;
using SharedHome.Identity.Entities;
using SharedHome.Shared.Application.Responses;
using SharedHome.Shared.Email.Senders;
using SharedHome.Shared.Exceptions.Common;
using System.Text;

namespace SharedHome.Application.Identity.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityEmailSender<ConfirmationEmailSender> _emailSender;
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<RegisterCommandHandler> _logger;

        public RegisterCommandHandler(
            UserManager<ApplicationUser> userManager,
            IIdentityEmailSender<ConfirmationEmailSender> emailSender,
            IPersonRepository personRepository,
            ILogger<RegisterCommandHandler> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var applicationUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
            };

            var result = await _userManager.CreateAsync(applicationUser, request.Password);
            if (!result.Succeeded)
            {
                throw new IdentityException(result);
            }
            _logger.LogInformation("User with id '{userId}' created.", applicationUser.Id);

            await _userManager.AddToRoleAsync(applicationUser, AppIdentityConstants.Roles.User);

            var person = Person.Create(new Guid(applicationUser.Id), applicationUser.FirstName, applicationUser.LastName, applicationUser.Email);
            await _personRepository.AddAsync(person);
            _logger.LogInformation("Person with id '{userId}' created.", person.Id);

            if (_userManager.Options.SignIn.RequireConfirmedEmail)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                await _emailSender.SendAsync(applicationUser.Email, code);

                return new ApiResponse<string>("Thanks for signing up. Check your mailbox and confirm email to get fully access.");
            }

            return new ApiResponse<string>("Thanks for signing up.");
        }
    }
}
