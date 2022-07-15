using Carter;
using WhasInt.Infrastructure.Entities;
using Whatsint.Model;
using WhatsInt.Interface.Services;

namespace WhatsInt.Interface.Features
{
    public class UserModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string basePath = "/user";

            app.MapPost($"{basePath}/login", Login).AllowAnonymous();
        }

        private async Task<IResult> Login(HttpContext context, UserService userService, UserDto user)
        {
            await userService.Login(user);

            return user.Id.Length > 0 ? Results.Ok(user) : Results.Unauthorized();
        }
    }
}
