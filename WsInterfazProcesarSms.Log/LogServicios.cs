using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WsInterfazProcesarSms.Model;

namespace WsInterfazProcesarSms.Log
{
    public static class LogServicios
    {
        private static readonly object objetoBloqueoJson = new();

        public static void limpiar_objeto_logs(ServiceSettings settings, Dictionary<string, object> diccionario)
        {
            settings.lst_atributos_sin_logs!.ForEach(atributo =>
            {
                if (diccionario.ContainsKey(atributo))
                {
                    diccionario[atributo] = "";
                }
            });

            recorrer_diccionario(settings, diccionario);
        }

        private static void recorrer_diccionario(ServiceSettings settings, Dictionary<string, object> diccionario)
        {
            foreach (var obj in diccionario)
            {
                try
                {
                    if (obj.Value != null)
                    {
                        var cadena_value = obj.Value.ToString();

                        if (!String.IsNullOrEmpty(cadena_value) && cadena_value!.Substring(0, 1) == "{" && cadena_value!.Substring(cadena_value.Length - 1) == "]")
                        {
                            var diccionario_interno = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(obj.Value));
                            settings.lst_atributos_sin_logs!.ForEach(atributo =>
                            {
                                if (diccionario_interno!.ContainsKey(atributo))
                                {
                                    diccionario[atributo] = "";
                                }
                            });
                            diccionario[obj.Key] = diccionario_interno!;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error de limpiar logs: " + ex.ToString());
                }
            }
        }

        public static void RegistrarTramas(string str_tipo, object obj, string ruta)
        {
            string str_ruta_archivo_log = "C:\\Logs\\" + ruta;
            try
            {
                lock (objetoBloqueoJson)
                {
                    if (!File.Exists(str_ruta_archivo_log))
                    {
                        Directory.CreateDirectory(str_ruta_archivo_log);
                    }
                    string str_nombre_archivo = Path.Combine(str_ruta_archivo_log, DateTime.Now.ToString("yyyyMMdd") + ".txt");

                    using (var fs = File.Open(str_nombre_archivo, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (var writer = new StreamWriter(fs))
                        {
                            writer.WriteLine(DateTime.Now.ToString("HHmmssff") + " " + str_tipo + JsonSerializer.Serialize(obj));
                        }
                        
                    }
                }
            } catch(Exception ex)
            {
                throw new ArgumentNullException(ex.ToString());
            }
        }
    }
}
