using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SharedHome.Identity.Entities;
using SharedHome.Identity.Exceptions;
using SharedHome.Infrastructure.Identity.Services;
using SharedHome.Shared.Abstractions.Commands;
using SharedHome.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHome.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : ICommandHandler<ConfirmEmailCommand, Unit>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IIdentityService _identityService;
        private readonly ILogger<ConfirmEmailCommandHandler> _logger;

        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager, IIdentityService identityService, ILogger<ConfirmEmailCommandHandler> logger)
        {
            _userManager = userManager;
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                throw new InvalidCredentialsException();
            }

            if (user.EmailConfirmed)
            {
                return Unit.Value;
            }

            var token = _identityService.Decode(request.Code);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new IdentityException(_identityService.MapIdentityErrorToIEnumerableString(result.Errors));
            }

            _logger.LogInformation("User with id '{userId}' confirmed email.", user.Id);

            return Unit.Value;
        }
    }
}
