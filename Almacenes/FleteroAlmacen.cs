using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class FleteroAlmacen
    {

        private static List<FleteroEntidad> fleteros = new List<FleteroEntidad>();

        public static IReadOnlyCollection<FleteroEntidad> Fleteros => fleteros.AsReadOnly();

        static FleteroAlmacen()
        {
            if (File.Exists(@"Datos\Fleteros.json"))
            {
                var fleteroJson = File.ReadAllText(@"Datos\Fleteros.json");
                fleteros = System.Text.Json.JsonSerializer.Deserialize<List<FleteroEntidad>>(fleteroJson) ?? new List<FleteroEntidad>();
            }
        }

        public static void Grabar()
        {

            var fleteroJson = System.Text.Json.JsonSerializer.Serialize(fleteros);
            File.WriteAllText(@"Datos\Fleteros.json", fleteroJson);
        }
    }
}
