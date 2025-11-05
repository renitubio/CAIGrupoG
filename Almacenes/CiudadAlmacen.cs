using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class CiudadAlmacen
    {

        private static List<CiudadEntidad> ciudades = new List<CiudadEntidad>();

        public static IReadOnlyCollection<CiudadEntidad> Ciudades => ciudades.AsReadOnly();

        static CiudadAlmacen()
        {
            if (File.Exists(@"Datos\Ciudades.json"))
            {
                var ciudadJson = File.ReadAllText(@"Datos\Ciudades.json");
                ciudades = System.Text.Json.JsonSerializer.Deserialize<List<CiudadEntidad>>(ciudadJson) ?? new List<CiudadEntidad>();
            }
        }

        public static void Grabar()
        {

            var ciudadJson = System.Text.Json.JsonSerializer.Serialize(ciudades);
            File.WriteAllText(@"Datos\Ciudades.json", ciudadJson);
        }
    }
}
