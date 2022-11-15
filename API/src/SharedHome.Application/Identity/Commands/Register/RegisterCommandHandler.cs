using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SharedHome.Application.Identity.Commands.Register;
using SharedHome.Domain.Persons;
using SharedHome.Domain.Persons.Repositories;
using SharedHome.Identity;
using SharedHome.Identity.Entities;
using SharedHome.Infrastructure.Identity.Services;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Email;
using SharedHome.Shared.Abstractions.Exceptions;
using SharedHome.Shared.Abstractions.Responses;

namespace SharedHome.Application.Identity.Commands.Identity
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityEmailSender _emailSender;
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<IdentityService> _logger;
        private readonly IIdentityService _identityService;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IIdentityEmailSender emailSender, IPersonRepository personRepository, ILogger<IdentityService> logger, IIdentityService identityService)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _personRepository = personRepository;
            _logger = logger;
            _identityService = identityService;
        }

        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
                throw new IdentityException(_identityService.MapIdentityErrorToIEnumerableString(result.Errors.Where(x => x.Code != "DuplicateUserName")));
            }
            _logger.LogInformation("User with id '{userId}' created.", applicationUser.Id);

            await _userManager.AddToRoleAsync(applicationUser, AppIdentityConstants.Roles.User);

            var person = Person.Create(new Guid(applicationUser.Id), applicationUser.FirstName, applicationUser.LastName, applicationUser.Email);
            await _personRepository.AddAsync(person);
            _logger.LogInformation("Person with id '{userId}' created.", person.Id);

            if (_userManager.Options.SignIn.RequireConfirmedEmail)
            {
                var code = await _identityService.GetEmailConfirmationTokenAsync(applicationUser);

                await _emailSender.SendAsync(applicationUser.Email, code);

                return new Response<string>("Thanks for signing up. Check your mailbox and confirm email to get fully access.");
            }

            return new Response<string>("Thanks for signing up.");
        }
    }
}
