using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class EmpresaTransporteAlmacen
    {

        private static List<EmpresaTransporteEntidad> empresasTransporte = new List<EmpresaTransporteEntidad>();

        public static IReadOnlyCollection<EmpresaTransporteEntidad> EmpresasTransporte => empresasTransporte.AsReadOnly();

        static EmpresaTransporteAlmacen()
        {
            if (File.Exists(@"Datos\EmpresasTransporte.json"))
            {
                var empresaTransporteJson = File.ReadAllText(@"Datos\EmpresasTransporte.json");
                empresasTransporte = System.Text.Json.JsonSerializer.Deserialize<List<EmpresaTransporteEntidad>>(empresaTransporteJson) ?? new List<EmpresaTransporteEntidad>();
            }
        }

        public static void Grabar()
        {

            var empresaTransporteJson = System.Text.Json.JsonSerializer.Serialize(empresasTransporte);
            File.WriteAllText(@"Datos\EmpresasTransporte.json", empresaTransporteJson);
        }
    }
}
