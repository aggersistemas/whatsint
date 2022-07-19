﻿using Carter;
using WhatsInt.Infrastructure.Entities;
using WhatsInt.Model;
using WhatsInt.Interface.Services;

namespace WhatsInt.Interface.Features
{
    public class UserModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string basePath = "/user";

            app.MapPost($"{basePath}/create", CreateUser).AllowAnonymous();
            app.MapPut($"{basePath}/update", UpdateUser).AllowAnonymous();
            app.MapGet($"{basePath}/find", Find).AllowAnonymous();
        }

        private async Task<IResult> Find(HttpContext context, UserService service, string id)
        {
            var user = await service.FindUserByID(id);

            return Results.Ok(user);
        }

        private async Task<IResult> CreateUser(HttpContext context, UserService service, UserDto user)
        {
            var newUser = await service.Created(user);

            return Results.Created("User Created", newUser);
        }

        private async Task<IResult> UpdateUser(HttpContext context, UserService service, UserDto loggedUser, UserDto user)
        {
            var newUser = await service.Update(loggedUser, user);

            return Results.Ok(newUser);
        }
    }
}
