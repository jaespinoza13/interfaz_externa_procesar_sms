using System.Text.Json;
using WsInterfazProcesarSms.Model;
using Microsoft.Extensions.Options;

namespace InterfazExternaProcesarSms.Middleware
{
    
    public class BasicAuthentication 
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ServiceSettings _serviceSettings;

        public BasicAuthentication(RequestDelegate requestDelegate, IOptionsMonitor<ServiceSettings> serviceSettings)
        {
            _requestDelegate = requestDelegate;
            _serviceSettings = serviceSettings.CurrentValue;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string authHeader = httpContext.Request.Headers["Authorization"];
            Console.WriteLine(authHeader);

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {

                string encodeAuthorization = authHeader.Substring("Basic ".Length).Trim();
                Console.WriteLine(encodeAuthorization);
                Console.WriteLine(_serviceSettings.auth_interfaz_externa);
                if (encodeAuthorization.Equals(_serviceSettings.auth_interfaz_externa))
                {
                    await _requestDelegate.Invoke(httpContext);
                }
                else
                {
                    await ResException(httpContext, "Credenciales erroneas", Convert.ToInt32(System.Net.HttpStatusCode.Unauthorized), System.Net.HttpStatusCode.Unauthorized.ToString());

                }

            }
            else
            {
                await ResException(httpContext, "No autorizado", Convert.ToInt32(System.Net.HttpStatusCode.Unauthorized), System.Net.HttpStatusCode.Unauthorized.ToString());
            }

        }

        internal static async Task ResException(HttpContext httpContext, String infoAdicional, int statusCode, string str_res_id_servidor)
        {
            ResConsumidor respuesta = new();

            httpContext.Response.ContentType = "application/json; charset=UTF-8";
            httpContext.Response.StatusCode = statusCode;

            respuesta.codigo = "001";
            respuesta.mensaje = infoAdicional;

            string str_respuesta = JsonSerializer.Serialize(respuesta);

            await httpContext.Response.WriteAsync(str_respuesta);
        }
    }
}
