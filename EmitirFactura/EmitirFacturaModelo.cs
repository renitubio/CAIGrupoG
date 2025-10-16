using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.EmitirFactura
{
    public class EmitirFacturaModelo
    {
        private readonly List<Guia> _guias;

        public EmitirFacturaModelo()
        {
            _guias = new List<Guia>();
            CargarDatosFicticios();
        }

        /// Busca guías que coincidan con un CUIT y que estén pendientes de facturar.
        public List<Guia> BuscarGuiasPorCUIT(string cuit)
        {
            return _guias
                .Where(g => g.CUIT == cuit && (g.Estado == EstadoGuia.Entregada || g.Estado == EstadoGuia.Devuelta))
                .ToList();
        }

        /// Cambia el estado de las guías a "Facturada".
        public void EmitirFacturas(List<Guia> guiasAFacturar)
        {
            foreach (var guia in guiasAFacturar)
            {
                var guiaEnDB = _guias.FirstOrDefault(g => g.NumeroGuia == guia.NumeroGuia);
                if (guiaEnDB != null)
                {
                    guiaEnDB.Estado = EstadoGuia.Facturada;
                }
            }
        }
        /// Carga datos de prueba.

        private void CargarDatosFicticios()
        {
            // Guías para CUIT 30-71001920-3
            _guias.Add(new Guia { CUIT = "30-71001920-3", NumeroGuia = "FAC001", Estado = EstadoGuia.Entregada, RazonSocial = "Empresa A S.A.", Importe = 1500.50m });
            _guias.Add(new Guia { CUIT = "30-71001920-3", NumeroGuia = "FAC002", Estado = EstadoGuia.Devuelta, RazonSocial = "Empresa A S.A.", Importe = 850.00m });

            // Guías para CUIT 20-30541611-9
            _guias.Add(new Guia { CUIT = "20-30541611-9", NumeroGuia = "FAC003", Estado = EstadoGuia.Entregada, RazonSocial = "Consultora B SRL", Importe = 12345.67m });
            _guias.Add(new Guia { CUIT = "20-30541611-9", NumeroGuia = "FAC004", Estado = EstadoGuia.Entregada, RazonSocial = "Consultora B SRL", Importe = 980.20m });
            _guias.Add(new Guia { CUIT = "20-30541611-9", NumeroGuia = "FAC005", Estado = EstadoGuia.Facturada, RazonSocial = "Consultora B SRL", Importe = 500m }); // Esta no debería aparecer
        }
    }
}
