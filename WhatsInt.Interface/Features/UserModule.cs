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

            //app.MapPost($"{basePath}/login", Login).AllowAnonymous();
        }     
    }
}
