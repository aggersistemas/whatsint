using Serilog;
using Serilog.Events;
using Serilog.Sinks.SpectreConsole;

namespace WhatsInt.Interface.Extensions.ServiceCollections
{
    internal static partial class ServiceCollection
    {
        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder webApplicationBuilder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.SpectreConsole("{Timestamp:HH:mm:ss} [{Level:u4}] {Message:lj}{NewLine}{Exception}", LogEventLevel.Information)
                .CreateLogger();

            webApplicationBuilder.Host.UseSerilog();

            return webApplicationBuilder;
        }
    }
}
