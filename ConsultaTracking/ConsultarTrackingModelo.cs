using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.ConsultaTracking
{
    public class ConsultarTrackingModelo
    {
        private readonly List<Guia> _guias;

        public ConsultarTrackingModelo()
        {
            _guias = new List<Guia>();
            CargarDatosFicticios();
        }


        /// Busca una guía por su número de seguimiento.
        public Guia BuscarGuia(string numeroGuia)
        {
            return _guias.FirstOrDefault(g => g.NumeroGuia.Equals(numeroGuia, StringComparison.OrdinalIgnoreCase));
        }

        private void CargarDatosFicticios()
        {
            _guias.Add(new Guia { NumeroGuia = "TRK001", Estado = EstadoGuia.AdmitidoEnCDOrigen });
            _guias.Add(new Guia { NumeroGuia = "TRK002", Estado = EstadoGuia.EnTransito });
            _guias.Add(new Guia { NumeroGuia = "TRK003", Estado = EstadoGuia.EnCDDestino });
            _guias.Add(new Guia { NumeroGuia = "TRK004", Estado = EstadoGuia.DistribucionUltimaMillaDomicilio });
            _guias.Add(new Guia { NumeroGuia = "TRK005", Estado = EstadoGuia.Entregado });
            _guias.Add(new Guia { NumeroGuia = "TRK006", Estado = EstadoGuia.Rechazado });
            _guias.Add(new Guia { NumeroGuia = "TRK007", Estado = EstadoGuia.PendienteDeRetiroEnAgencia });
        }
    }
}
