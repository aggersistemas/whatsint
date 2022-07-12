using Carter;
using WhatsInt.Interface.Extensions.ApplicationBuilders;
using WhatsInt.Interface.Extensions.ServiceCollections;
using WhatsInt.Interface.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configure();

var app = builder.Build();

app
    .UseExceptionHandling(app.Environment)
    .UseSwaggerEndpoints()
    .UseAppCors()
    .UseAuthentication()
    .UseAuthorization()
    .UseMiddleware<ErrorHandler>();

app.MapCarter();

app.Run();