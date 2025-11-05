using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class EgresosAlmacen
    {

        private static List<EgresosEntidad> egresos = new List<EgresosEntidad>();

        public static IReadOnlyCollection<EgresosEntidad> Egresos => egresos.AsReadOnly();

        
        public static void Nuevo(EgresosEntidad egreso)
        {
            if (egreso.MontoPago == null)
                throw new ArgumentException("El MONTO no puede ser nulo");
            egresos.Add(egreso);
        }

        public static void Borrar(int MontoPago)
        {
            egresos.RemoveAll(e => e.MontoPago == MontoPago);
        }
        
        static EgresosAlmacen()
        {
            if (File.Exists(@"Datos\Clientes.json"))
            {
                var clienteJson = File.ReadAllText(@"Datos\Egresos.json");
                egresos = System.Text.Json.JsonSerializer.Deserialize<List<EgresosEntidad>>(clienteJson) ?? new List<EgresosEntidad>();
            }
        }

        public static void Grabar()
        {

            var clienteJson = System.Text.Json.JsonSerializer.Serialize(egresos);
            File.WriteAllText(@"Datos\Egresos.json", clienteJson);
        }
    }
}
