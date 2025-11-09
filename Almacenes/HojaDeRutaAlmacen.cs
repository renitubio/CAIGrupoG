using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json; // ⬅️ NECESARIO para JsonSerializer
using System.Text.Json.Serialization; // ⬅️ NECESARIO para JsonNumberHandling

namespace CAIGrupoG.Almacenes
{
    static class HojaDeRutaAlmacen
    {

        private static List<HojaDeRutaEntidad> hojasDeRuta = new List<HojaDeRutaEntidad>();

        public static IReadOnlyCollection<HojaDeRutaEntidad> HojasDeRuta => hojasDeRuta.AsReadOnly();

        public static void Nuevo(HojaDeRutaEntidad hojaDeRuta)
        {
            // Nota: HDR_ID es int, por lo que no puede ser null.
            hojasDeRuta.Add(hojaDeRuta);
        }

        public static void Borrar(int HDR_ID)
        {
            hojasDeRuta.RemoveAll(h => h.HDR_ID == HDR_ID);
        }

        static HojaDeRutaAlmacen()
        {
            // 1. Definir la ruta absoluta
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaCompleta = Path.Combine(rutaBase, @"Datos\HojaDeRuta.json");

            // 🛑 OPCIONES DE DESERIALIZACIÓN TOLERANTES 🛑
            var options = new JsonSerializerOptions
            {
                // Permite leer números que están envueltos en comillas (ej: "2" en lugar de 2)
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };


            // 2. Usar la ruta completa para la verificación y lectura
            if (File.Exists(rutaCompleta))
            {
                var HojaDeRutajson = File.ReadAllText(rutaCompleta);
                // ⬅️ USAR OPCIONES TOLERANTES AQUÍ
                hojasDeRuta = JsonSerializer.Deserialize<List<HojaDeRutaEntidad>>(HojaDeRutajson, options)
                              ?? new List<HojaDeRutaEntidad>();
            }
        }

        public static void Grabar()
        {
            // 1. Definir la ruta absoluta para grabar
            string rutaBase = Directory.GetCurrentDirectory();
            string rutaCompleta = Path.Combine(rutaBase, @"Datos\HojaDeRuta.json");

            // 2. Usar la ruta completa para la escritura
            var HojaDeRutajson = System.Text.Json.JsonSerializer.Serialize(hojasDeRuta);
            File.WriteAllText(rutaCompleta, HojaDeRutajson);
        }
    }
}
