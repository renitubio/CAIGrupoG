using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // ⬅️ NECESARIO para Directory, Path, File

namespace CAIGrupoG.Almacenes
{
    static class CiudadAlmacen
    {

        private static List<CiudadEntidad> ciudades = new List<CiudadEntidad>();

        public static IReadOnlyCollection<CiudadEntidad> Ciudades => ciudades.AsReadOnly();

        static CiudadAlmacen()
        {
            // 1. Definir la ruta absoluta
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaCompleta = Path.Combine(rutaBase, @"Datos\Ciudades.json");

            // 2. Usar la ruta completa para la verificación y lectura
            if (File.Exists(rutaCompleta))
            {
                var ciudadJson = File.ReadAllText(rutaCompleta); // ⬅️ Usar rutaCompleta
                ciudades = System.Text.Json.JsonSerializer.Deserialize<List<CiudadEntidad>>(ciudadJson) ?? new List<CiudadEntidad>();
            }
        }

        public static void Grabar()
        {
            // 1. Definir la ruta absoluta para grabar
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaCompleta = Path.Combine(rutaBase, @"Datos\Ciudades.json");

            // 2. Usar la ruta completa para la escritura
            var ciudadJson = System.Text.Json.JsonSerializer.Serialize(ciudades);
            File.WriteAllText(rutaCompleta, ciudadJson); // ⬅️ Usar rutaCompleta
        }
    }
}
