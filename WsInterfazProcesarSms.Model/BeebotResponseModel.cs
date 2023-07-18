using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsInterfazProcesarSms.Model
{
    public class BeebotResponseModel
    {
        public object cuerpo { get; set; } = String.Empty;
        public string codigo { get; set; } = String.Empty;
        public string str_informacion_adicional { get; set; } = String.Empty;
        public string str_car_print_factura { get; set; } = String.Empty;
        public string str_car_print_otros { get; set; } = String.Empty;
        public string str_car_print_recibo { get; set; } = String.Empty;
        public string str_car_printa_factura { get; set; } = String.Empty;
        public double dcm_comision_externa { get; set; }
        public string str_cedula { get; set; } = String.Empty;
        public string str_ente { get; set; } = String.Empty;
        public string str_nombres { get; set; } = String.Empty;
        public bool bln_validacion_numero { get; set; }
        public Diccionario diccionario { get; set; } = new Diccionario();
        public string str_res_estado_transaccion { get; set; } = String.Empty;
    }

    public class Diccionario
    {
        public string ERROR { get; set; } = String.Empty;
    }


}
