using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class FacturaAlmacen
    {

        private static List<FacturaEntidad> facturas = new List<FacturaEntidad>();

        public static IReadOnlyCollection<FacturaEntidad> Facturas => facturas.AsReadOnly();

        public static void Nuevo(FacturaEntidad factura)
        {
            if (factura.NumeroFactura == null)
                throw new ArgumentException("El CUIT no puede ser nulo");
            facturas.Add(factura);
        }

        public static void Borrar(int NumeroFactura)
        {
            facturas.RemoveAll(f => f.NumeroFactura == NumeroFactura);
        }

        static FacturaAlmacen()
        {
            if (File.Exists(@"Datos\Facturas.json"))
            {
                var facturaJson = File.ReadAllText(@"Datos\Facturas.json");
                facturas = System.Text.Json.JsonSerializer.Deserialize<List<FacturaEntidad>>(facturaJson) ?? new List<FacturaEntidad>();
            }
        }

        public static void Grabar()
        {

            var facturaJson = System.Text.Json.JsonSerializer.Serialize(facturas);
            File.WriteAllText(@"Datos\Facturas.json", facturaJson);
        }
    }
}
