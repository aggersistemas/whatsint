using Carter;
using WhatsInt.Interface.Services;
using WhatsInt.Model.Dto;

namespace WhatsInt.Interface.Features
{
    public class UserModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string basePath = "/user";

            app.MapPost(basePath, CreateUser).AllowAnonymous();
            app.MapPut(basePath, UpdateUser);
            app.MapGet($"{basePath}/{{userId}}", Find).AllowAnonymous();
        }

        private static async Task<IResult> Find(HttpContext context, UserService service, string userId)
        {
            var userDto = await service.FindUserById(userId);

            return Results.Ok(userDto);
        }

        private static async Task<IResult> CreateUser(HttpContext context, UserService userService, UserDto userDto)
        {
            var userCreated = await userService.Create(userDto);

            return Results.Created(string.Empty, userCreated);
        }

        private static async Task<IResult> UpdateUser(HttpContext context, UserService userService, UserDto userDto)
        {
            var userUpdated = await userService.Update(userDto);

            return Results.Ok(userUpdated);
        }
    }
}
