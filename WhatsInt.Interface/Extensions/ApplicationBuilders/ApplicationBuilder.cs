namespace WhatsInt.Interface.Extensions.ApplicationBuilders
{
    internal static partial class ApplicationBuilder
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            var isDevelopment = environment.IsDevelopment();

            if (isDevelopment) app.UseDeveloperExceptionPage();

            return app;
        }

        public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
        {
            app.UseCors(p =>
            {
                p.AllowAnyOrigin();
                p.WithMethods("GET");
                p.AllowAnyHeader();
            });

            return app;
        }
    }
}
