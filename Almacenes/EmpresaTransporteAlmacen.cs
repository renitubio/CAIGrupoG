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

        public static void Nuevo(EmpresaTransporteEntidad empresaTransporte)
        {
            if (empresaTransporte.CUITEmpresaTransporte == null)
                throw new ArgumentException("El CUIT no puede ser nulo");
            empresasTransporte.Add(empresaTransporte);
        }

        public static void Borrar(string CUITEmpresaTransporte)
        {
            empresasTransporte.RemoveAll(e => e.CUITEmpresaTransporte == CUITEmpresaTransporte);
        }

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
