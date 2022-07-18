using Carter;
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
        }

        private async Task<IResult> CreateUser(HttpContext context, UserService service, UserDto user)
        {
            var newUser = await service.Created(user);

            return newUser == null ? Results.Conflict() :
                newUser.Id.Length == 0 ? Results.UnprocessableEntity() : Results.Created("", newUser);
        }
    }
}
