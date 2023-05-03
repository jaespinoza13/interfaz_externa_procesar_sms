using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsInterfazProcesarSms.Model
{
    public class ReqEmergenciasFinancieras : Header
    {
        public string str_cedula_socio { get; set; } = String.Empty;
        public int int_opcion_bloqueo { get; set; }
        public int? int_id_pregunta { get; set; }
        public int? int_numero_pregunta { get; set; }
        public int? int_respuesta_pregunta { get; set; }
        public string str_pregunta { get; set; } = String.Empty;
        public string codigo { get; set; } = String.Empty;
        public string str_sub_opcion_bloqueo { get; set; } = String.Empty;
        //Bloqueo Tarjetas
        public int? int_tipo_tarjeta { get; set; }
        public string str_documento { get; set; } = String.Empty;

        //Bloqueo Canales
        public int int_id_cliente { get; set; }
        public string str_canal { get; set; } = String.Empty;
        public int int_id_motivo { get; set; }
        public string str_nem_canal_usuario { get; set; } = String.Empty;
        public string str_estado  { get; set; } = String.Empty;
        public int int_motivo { get; set; } 
        public string str_num_documento { get; set; } = String.Empty;
        public string str_observacion { get; set; } = String.Empty;
        public string str_sistema { get; set; } = String.Empty;
        public string str_identificador { get; set; } = String.Empty;
        public int? int_id_servicio { get; set; }
    }
}
