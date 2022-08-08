using Carter;
using WhatsInt.Interface.Services;

namespace WhatsInt.Interface.Features
{
    public class AuthorizationModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            const string basePath = "/auth";

            app.MapPost($"{basePath}", Auth).AllowAnonymous();
        }

        private static async Task<IResult> Auth(HttpContext context, AuthorizationService service)
        {
            var generatedBearer = await service.Authorize();

            return Results.Ok(generatedBearer);
        }
    }
}
