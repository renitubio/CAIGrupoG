using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class ServicioAlmacen
    {

        private static List<ServicioEntidad> servicios = new List<ServicioEntidad>();

        public static IReadOnlyCollection<ServicioEntidad> Servicios => servicios.AsReadOnly();

        static ServicioAlmacen()
        {
            if (File.Exists(@"Datos\Servicios.json"))
            {
                var servicioJson = File.ReadAllText(@"Datos\Servicios.json");
                servicios = System.Text.Json.JsonSerializer.Deserialize<List<ServicioEntidad>>(servicioJson) ?? new List<ServicioEntidad>();
            }
        }

        public static void Grabar()
        {

            var servicioJson = System.Text.Json.JsonSerializer.Serialize(servicios);
            File.WriteAllText(@"Datos\Servicios.json", servicioJson);
        }
    }
}
