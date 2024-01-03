namespace WsInterfazProcesarSms.Model
{
    public class ResErrorBloqueoServicio : Header
    {
        public string str_res_original_id_msj { get; set; } = String.Empty;
        public string str_res_original_id_servicio { get; set; } = String.Empty;
        public DateTime dt_res_fecha_msj_crea { get; set; } = DateTime.Now;
        public string str_res_estado_transaccion { get; set; } = String.Empty;
        public string str_res_codigo { get; set; } = String.Empty;
        public string str_res_info_adicional { get; set; } = String.Empty;
        public void LlenarResHeaderBeebot(BeebotResponseModel beebot)
        {
            str_res_codigo = beebot.codigo;
            str_id_transaccion = Guid.NewGuid().ToString();
            str_nemonico_canal = "CANCCE";
            str_app = "APP_CCE";
            str_id_servicio = "RES_BLOQUEO_SERVICIO";
            str_res_info_adicional = beebot.str_informacion_adicional;
            str_res_estado_transaccion = beebot.str_res_estado_transaccion;
            str_tipo_peticion = "REQ";
            dt_fecha_operacion = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", null);
        }
    }
}
