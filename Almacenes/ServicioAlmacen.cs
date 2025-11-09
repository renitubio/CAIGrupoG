using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CAIGrupoG.Almacenes
{
    static class ServicioAlmacen
    {
        public static List<ServicioEntidad> servicios = new List<ServicioEntidad>(); // Inicialización segura
        static ServicioAlmacen()
        {
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaCompleta = Path.Combine(rutaBase, @"Datos\Servicios.json"); // Calcula la ruta completa

            // CORRECCIÓN CRÍTICA: Usar la rutaCompleta
            if (File.Exists(rutaCompleta))
            {
                // CORRECCIÓN: Usar la rutaCompleta para leer el archivo
                var servicioJson = File.ReadAllText(rutaCompleta);
                servicios = System.Text.Json.JsonSerializer.Deserialize<List<ServicioEntidad>>(servicioJson) ?? new List<ServicioEntidad>();
            }
        }

        // El método Grabar también debe usar la ruta absoluta
        public static void Grabar()
        {
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaCompleta = Path.Combine(rutaBase, @"Datos\Servicios.json");

            var servicioJson = System.Text.Json.JsonSerializer.Serialize(servicios);
            File.WriteAllText(rutaCompleta, servicioJson);
        }
    }
}
