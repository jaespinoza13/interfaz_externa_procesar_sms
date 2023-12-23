namespace WsInterfazProcesarSms.Model
{
    public class ServiceSettings
    {
        public string path_logs_interface { get; set; } = String.Empty;
        public List<string>? lst_atributos_sin_logs { get; set; } = new();
        public string api_gateway_procesarsms { get; set; } = String.Empty;
        public string auth_interfaz_externa { get; set; } = String.Empty;
        public string auth_api_gateway { get; set; } = String.Empty;
        public string str_observacion_msg { get; set; } = String.Empty;
     
    }
}
