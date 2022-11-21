﻿using MediatR;
using SharedHome.Shared.Application.Responses;

namespace SharedHome.Application.Authentication.Commands.Register
{
    public record RegisterCommand(string Email, string FirstName, string LastName, string Password) : IRequest<Response<string>>;
}