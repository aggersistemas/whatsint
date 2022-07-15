using Microsoft.OpenApi.Models;
using WhatsInt.Interface.Helpers;

namespace WhatsInt.Interface.Extensions.ServiceCollections
{
    internal static partial class ServiceCollection
    {
        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddEndpointsApiExplorer();

            var configurationSection = webApplicationBuilder.Configuration.GetSection("MainUri").Get<string>().ToUri();

            var authUri = new Uri(configurationSection, "auth");

            webApplicationBuilder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "API",
                    Title = "Hack Day",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Agger Sistemas",
                        Url = new Uri("https://www.agger.com.br")
                    }
                });

                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            ClientCredentials = new OpenApiOAuthFlow
                            {
                                TokenUrl = authUri
                            }
                        }
                    });

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header
                            },
                            new List<string>()
                        }
                    });
            });

            return webApplicationBuilder;
        }
    }

}
