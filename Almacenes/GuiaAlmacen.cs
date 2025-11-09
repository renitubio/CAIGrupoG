using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // Necesario para File.Exists, ReadAllText, Directory, Path

namespace CAIGrupoG.Almacenes
{
    static class GuiaAlmacen
    {

        private static List<GuiaEntidad> guias = new List<GuiaEntidad>();

        public static IReadOnlyCollection<GuiaEntidad> Guias => guias.AsReadOnly();

        // ... (Métodos Actualizar, Nuevo, Borrar sin cambios) ...

        public static void Actualizar(GuiaEntidad guiaModificada)
        {
            // 1. Encontrar y remover la versión antigua por NumeroGuia
            Borrar(guiaModificada.NumeroGuia);

            // 2. Agregar la nueva versión (con el estado 'Entregado')
            guias.Add(guiaModificada);
        }
        public static void Nuevo(GuiaEntidad guia)
        {
            if (guia.NumeroGuia == null)
                throw new ArgumentException("El CUIT no puede ser nulo");
            guias.Add(guia);
        }

        public static void Borrar(string NumeroGuia)
        {
            guias.RemoveAll(g => g.NumeroGuia == NumeroGuia);
        }

        static GuiaAlmacen()
        {
            // 1. Definir la ruta absoluta
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaCompleta = Path.Combine(rutaBase, @"Datos\Guias.json");

            // 2. Usar la ruta completa para la verificación y lectura
            if (File.Exists(rutaCompleta))
            {
                var guiaJson = File.ReadAllText(rutaCompleta);
                guias = System.Text.Json.JsonSerializer.Deserialize<List<GuiaEntidad>>(guiaJson) ?? new List<GuiaEntidad>();
            }
        }

        public static void Grabar()
        {
            // 1. Definir la ruta absoluta
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaCompleta = Path.Combine(rutaBase, @"Datos\Guias.json");

            // 2. Usar la ruta completa para la escritura
            var guiaJson = System.Text.Json.JsonSerializer.Serialize(guias);
            File.WriteAllText(rutaCompleta, guiaJson);
        }

        public static void Recargar()
        {
            // 1. Definir la ruta absoluta
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaCompleta = Path.Combine(rutaBase, @"Datos\Guias.json");

            // 2. Usar la ruta completa para la verificación y lectura
            if (File.Exists(rutaCompleta))
            {
                var guiaJson = File.ReadAllText(rutaCompleta);
                // Importante: Reemplazar la lista estática interna con los nuevos datos
                guias = System.Text.Json.JsonSerializer.Deserialize<List<GuiaEntidad>>(guiaJson) ?? new List<GuiaEntidad>();
            }
        }
    }
}