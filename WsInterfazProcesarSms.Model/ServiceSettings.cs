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
        public int BLOQ_TDD { get; set; }
        public int BLOQ_CANALES { get; set; }
        public int ANUL_CARTOLAS { get; set; }
        public int ANUL_LIBRETIN { get; set; }
        public int BLOQ_CTA_RETIRO { get; set; }
        public int int_motivo_bloq_bvi { get; set; }
        public int int_motivo_bloq_bmo { get; set; }
    }
}
