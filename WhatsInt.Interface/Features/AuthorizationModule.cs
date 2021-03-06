using Carter;
using WhatsInt.Model;
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

        private async Task<IResult> Auth(HttpContext context, AuthorizationService service)
        {
            var bearer = await service.Authorize();

            return Results.Ok(bearer);
        }
    }
}
