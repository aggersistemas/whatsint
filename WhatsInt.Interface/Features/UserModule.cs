using Carter;
using WhatsInt.Model;
using WhatsInt.Interface.Services;
using Microsoft.AspNetCore.Mvc;
using Carter.ModelBinding;

namespace WhatsInt.Interface.Features
{
    public class UserModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string basePath = "/user";

            app.MapPost(basePath, CreateUser).AllowAnonymous();
            app.MapPut(basePath, UpdateUser);
            app.MapGet($"{basePath}/{{id}}", Find).AllowAnonymous();
        }

        private async Task<IResult> Find(HttpContext context, UserService service, string id)
        {
            var user = await service.FindUserById(id);

            return Results.Ok(user);
        }
        private async Task<IResult> CreateUser(HttpContext context, UserService service, UserDto user)
        {
            var newUser = await service.Created(user);

            return Results.Created("User Created", newUser);
        }

        private async Task<IResult> UpdateUser(HttpContext context, UserService service, UserDto user)
        {
            var newUser = await service.Update(user);

            return Results.Ok(newUser);
        }
    }
}
