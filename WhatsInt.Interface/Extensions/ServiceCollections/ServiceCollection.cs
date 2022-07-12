using Carter;
using Microsoft.Extensions.Options;

namespace WhatsInt.Interface.Extensions.ServiceCollections
{
    internal static partial class ServiceCollection
    {
        public static WebApplicationBuilder Configure(this WebApplicationBuilder webApplicationBuilder)
        {
            //const string settingsName = nameof(DatabaseSettings);

            //var configurationSection = webApplicationBuilder.Configuration.GetSection(settingsName);

            //webApplicationBuilder.Services.Configure<DatabaseSettings>(configurationSection);

            //webApplicationBuilder
            //    .AddSerilog()
            //    .AddSwagger()
            //    .AddAuthentication()
            //    .AddAuthorization();

            webApplicationBuilder.Services
                .AddHttpContextAccessor()
                .AddInjections()
                .AddCors()
                .AddCarter();

            return webApplicationBuilder;
        }

        public static IServiceCollection AddInjections(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddSingleton(typeof(IRepository<>), typeof(MongoRepository<>));

            //serviceCollection.AddSingleton<LoginService>();

            //serviceCollection.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            return serviceCollection;
        }
    }
}
