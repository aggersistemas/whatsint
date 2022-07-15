using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WhatsInt.Interface.Extensions.ServiceCollections
{
    internal static partial class ServiceCollection
    {
        public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder webApplicationBuilder)
        {
            const string token = "AggerPassword";

            var authenticationBuilder = webApplicationBuilder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            authenticationBuilder.AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token)), 
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return webApplicationBuilder;
        }

        public static WebApplicationBuilder AddAuthorization(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });

            return webApplicationBuilder;
        }
    }

}
