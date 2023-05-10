using RestSharp;
using System.Net;
using System.Text.Json;
using System.Reflection;
using WsInterfazProcesarSms.Log;
using WsInterfazProcesarSms.Model;

namespace WsInterfazProcesarSms.Neg
{
    public class InterfazProcesarSmsNeg
    {
        private readonly ServiceSettings serviceSettings;
        private InfoLog infoLog;
        private const string str_salida_error = "e:< ";
        private const string str_solicitud = "s:< ";
        private const string str_respuesta = "r:< ";

        public InterfazProcesarSmsNeg(ServiceSettings serviceSettings)
        {
            this.serviceSettings = serviceSettings;
        }

        public object ProcesarSolicitud(ReqProcesarSms sol_tran, string str_operacion, string str_token)
        {
            object respuesta = new();
            ResComun res = new();

            // Llenar el request del header para registrar los logs
            res.LlenarResHeader(sol_tran);
            sol_tran.dt_fecha_operacion = res.dt_fecha_operacion;

            var diccionario = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(sol_tran));
            object sol_tran_2 = JsonSerializer.Serialize(sol_tran);

            LogServicios.limpiar_objeto_logs(serviceSettings, diccionario!);
            infoLog.str_clase = GetType().FullName;
            infoLog.str_operacion = str_operacion;
            infoLog.str_objeto = diccionario!;
            infoLog.str_metodo = MethodBase.GetCurrentMethod()!.Name;
            infoLog.str_fecha = DateTime.Now;
            infoLog.str_tipo = str_solicitud;
            LogServicios.RegistrarTramas(infoLog.str_tipo, infoLog, "IntfProcesarSms");

            try
            {
                // Se llama al método que consume el servicio del api gateway
                respuesta = respuesta_servicio(sol_tran_2, serviceSettings.api_gateway_procesarsms + str_operacion, serviceSettings.auth_api_gateway, str_token);

                var diccionario_resp = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(respuesta));
                infoLog.str_clase = GetType().FullName!;
                infoLog.str_operacion = str_operacion;
                infoLog.str_objeto = diccionario_resp!;
                infoLog.str_metodo = MethodBase.GetCurrentMethod()!.Name;
                infoLog.str_fecha = DateTime.Now;
                infoLog.str_tipo = str_respuesta;
                LogServicios.RegistrarTramas(infoLog.str_tipo, infoLog!, "IntfProcesarSms");
            }
            catch (Exception ex)
            {
                respuesta = new
                {
                    codigo = Convert.ToInt32(HttpStatusCode.InternalServerError).ToString(),
                    mensaje = "Ocurrió un problema en la interfaz, intente nuevamente más tarde" + ex.ToString()
                };

                infoLog.str_tipo = str_salida_error;
                infoLog.str_objeto = respuesta;
                infoLog.str_operacion = str_operacion;
                infoLog.str_metodo = MethodBase.GetCurrentMethod()!.Name;
                LogServicios.RegistrarTramas(str_salida_error, infoLog, serviceSettings.path_logs_interface);
            }
            return respuesta;
        }

        public object respuesta_servicio(object sol_tran, string str_url_servicio, string auth, string str_token)
        {
            object respuesta = new();
            try
            {
                var options = new RestClientOptions(str_url_servicio)
                {
                    ThrowOnAnyError = true,
                    MaxTimeout = 300000
                };
                var client = new RestClient(options);
                // Construir lapeticion con todos los headers de autenticacion y el cuerpo de la petición
                var request = new RestRequest()
                    .AddHeader("Authorization-Gateway", "Auth-Gateway " + auth)
                    .AddHeader("Authorization", str_token)
                    .AddHeader("Content-Type", "application/json")
                    .AddParameter("application/json", sol_tran, ParameterType.RequestBody);
                Console.WriteLine(request.ToString());
                var response = client.PostAsync(request).Result;
                Console.WriteLine(response);
                if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    respuesta = new {
                        codigo = Convert.ToInt32(HttpStatusCode.Unauthorized).ToString(),
                        mensaje = "La sesión ha caducado"

                    };
                }

                var data = JsonSerializer.Deserialize<ResInterface>(response.Content!)!;

                if(data != null)
                {
                    // Construir respuesta segun requerimiento de Eclipsoft
                    respuesta = new
                    {
                        codigo = data.str_res_codigo,
                        mensaje = data.str_res_codigo == "000" ? "OK" : data.str_res_info_adicional
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                respuesta = new
                {
                    codigo = Convert.ToInt32(HttpStatusCode.InternalServerError).ToString(),
                    mensaje = "Ocurrió un problema, intente nuevamente más tarde"
                };
            }
            return respuesta;
        }
    }
}
