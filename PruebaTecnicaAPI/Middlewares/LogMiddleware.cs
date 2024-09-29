using NLog;
using System.Text;
using ILogger = NLog.ILogger;

namespace PruebaAPI.Middlewares
{
    public class LogMiddleware
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {

            // Registrar solicitud entrante
            await LogRequest(context);

            // Interceptar respuesta para registrar
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                // Registrar respuesta saliente
                await LogResponse(context);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        //metodo que regitra la solicitud entrante
        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            await context.Request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length));
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            context.Request.Body.Position = 0;
            Logger.Info("----------------------------------INICIO TRANSACCION--------------------------------------------\n");
            Logger.Info($"Solicitud entrante: {context.Request.Method} {context.Request.Path} {context.Request.QueryString} {bodyAsText}");
        }

        //metodo para registrar la solicitud de respuesta
        private async Task LogResponse(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            Logger.Info($"Respuesta saliente: {context.Response.StatusCode}: {text}");
            Logger.Info("----------------------------------FIN TRANSACCION--------------------------------------------\n");
        }
    }
}
