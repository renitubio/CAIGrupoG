using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.EntregaGuíaCD
{
    public class EntregaGuiaCDModelo
    {
        private readonly List<Guia> _guias;

        public EntregaGuiaCDModelo()
        {
            _guias = new List<Guia>();
            CargarDatosFicticios();
        }

        public List<Guia> BuscarGuiasPorDNI(string dni)
        {
            return _guias
                .Where(g => g.DniDestinatario == dni && g.Estado == EstadoGuia.PendienteDeRetiroEnCD)
                .ToList();
        }

        public void ConfirmarRetiro(List<Guia> guiasARetirar)
        {
            foreach (var guia in guiasARetirar)
            {
                var guiaOriginal = _guias.FirstOrDefault(g => g.NumeroGuia == guia.NumeroGuia);
                if (guiaOriginal != null)
                {
                    guiaOriginal.Estado = EstadoGuia.Retirado;
                }
            }
        }

        private void CargarDatosFicticios()
        {
            _guias.Add(new Guia { NumeroGuia = "CD001", DniDestinatario = "25111222", Estado = EstadoGuia.PendienteDeRetiroEnCD, TipoPaquete = TipoPaquete.S });
            _guias.Add(new Guia { NumeroGuia = "CD002", DniDestinatario = "25111222", Estado = EstadoGuia.PendienteDeRetiroEnCD, TipoPaquete = TipoPaquete.L });
            _guias.Add(new Guia { NumeroGuia = "CD003", DniDestinatario = "32333444", Estado = EstadoGuia.PendienteDeRetiroEnCD, TipoPaquete = TipoPaquete.M });
            _guias.Add(new Guia { NumeroGuia = "CD004", DniDestinatario = "38555666", Estado = EstadoGuia.PendienteDeRetiroEnCD, TipoPaquete = TipoPaquete.XL });
            _guias.Add(new Guia { NumeroGuia = "CD005", DniDestinatario = "38555666", Estado = EstadoGuia.Retirado, TipoPaquete = TipoPaquete.S });
        }
    }
}
