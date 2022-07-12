namespace WhatsInt.Interface.Extensions.ApplicationBuilders
{
    internal static partial class ServiceCollection
    {
        public static IApplicationBuilder UseSwaggerEndpoints(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                c.EnableDeepLinking();
                c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
