using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsInterfazProcesarSms.Model
{
    public class Header
    {
        public string str_id_transaccion { get; set; } = string.Empty;
        public string str_nemonico_canal { get; set; } = string.Empty;
        public string str_id_sistema { get; set; } = string.Empty;
        public string str_app { get; set; } = string.Empty;
        public string str_id_servicio { get; set; } = string.Empty;
        public string str_version_servicio { get; set; } = string.Empty;
        public string str_id_usuario { get; set; } = string.Empty;
        public string str_mac_dispositivo { get; set; } = string.Empty;
        public string str_ip_dispositivo { get; set; } = string.Empty;
        public string str_remitente { get; set; } = string.Empty;
        public string str_receptor { get; set; } = string.Empty;
        public string str_tipo_peticion { get; set; } = string.Empty;
        public string str_id_msj { get; set; } = string.Empty;
        public DateTime dt_fecha_operacion { get; set; } 
        public bool bl_posible_duplicado { get; set; } 
        public string str_token { get; set; } = string.Empty;
        public string str_prioridad { get; set; } = string.Empty;
        public string str_login { get; set; } = string.Empty;
        public string str_latitud { get; set; } = string.Empty;
        public string str_longitud { get; set; } = string.Empty;
        public string str_firma_digital { get; set; } = string.Empty;
        public string str_clave_secreta { get; set; } = string.Empty;
        public string str_pais { get; set; } = string.Empty;
        public string str_sesion { get; set; } = string.Empty;
        public string str_id_oficina { get; set; } = string.Empty;
        public string str_id_perfil { get; set; } = string.Empty;
        public string str_ente { get; set; } = string.Empty;
    }
}
