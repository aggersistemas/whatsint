using System.Text.Json;
using WhatsInt.Infrastructure.Exceptions;

namespace WhatsInt.Interface.Middlewares
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _requestDelegate;

        public ErrorHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (AppException appException)
            {
                var response = context.Response;

                response.ContentType = "application/json";

                response.StatusCode = (int)appException.StatusCode;

                var errorResult = new { appException.Message };

                var resultSerialized = JsonSerializer.Serialize(errorResult);

                await response.WriteAsync(resultSerialized);
            }
        }
    }
}
