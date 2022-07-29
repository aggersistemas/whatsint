using WhatsInt.ViewModel;

namespace WhatsInt.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplication AddServices(this WebApplicationBuilder webBuilder)
        {
            webBuilder.Services.AddRazorPages();
            webBuilder.Services.AddMvvm();
            webBuilder.Services.AddServerSideBlazor();

            webBuilder.Services.AddTransient<LoginViewModel>();
            webBuilder.Services.AddTransient<InteractionViewModel>();

            return webBuilder.Build();
        }
    }
}
