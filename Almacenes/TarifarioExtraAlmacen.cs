using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class TarifarioExtraAlmacen
    {

        private static List<TarifarioExtraEntidad> tarifariosExtra = new List<TarifarioExtraEntidad>();

        public static IReadOnlyCollection<TarifarioExtraEntidad> TarifariosExtra => tarifariosExtra.AsReadOnly();

        static TarifarioExtraAlmacen()
        {
            if (File.Exists(@"Datos\TarifariosExtra.json"))
            {
                var tarifarioExtraJson = File.ReadAllText(@"Datos\TarifariosExtra.json");
                tarifariosExtra = System.Text.Json.JsonSerializer.Deserialize<List<TarifarioExtraEntidad>>(tarifarioExtraJson) ?? new List<TarifarioExtraEntidad>();
            }
        }

        public static void Grabar()
        {

            var tarifarioExtraJson = System.Text.Json.JsonSerializer.Serialize(tarifariosExtra);
            File.WriteAllText(@"Datos\TarifariosExtra.json", tarifarioExtraJson);
        }
    }
}
