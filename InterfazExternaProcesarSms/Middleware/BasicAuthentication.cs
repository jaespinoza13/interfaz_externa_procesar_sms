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
                    httpContext.Response.StatusCode = Convert.ToInt32(System.Net.HttpStatusCode.Unauthorized);
                }

            }
            else
            {
                httpContext.Response.StatusCode = Convert.ToInt32(System.Net.HttpStatusCode.Unauthorized);
            }

        }
    }
}
