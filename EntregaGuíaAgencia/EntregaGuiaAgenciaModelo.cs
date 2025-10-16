using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.EntregaGuíaAgencia
{
    public class EntregaGuiaAgenciaModelo
    {
        private readonly List<Guia> _guias;

        public EntregaGuiaAgenciaModelo()
        {
            _guias = new List<Guia>();
            CargarDatosFicticios();
        }

        /// <summary>
        /// Busca guías asociadas a un DNI que estén pendientes de retiro.
        /// </summary>
        /// <param name="dni">DNI del destinatario.</param>
        /// <returns>Una lista de guías encontradas.</returns>
        public List<Guia> BuscarGuiasPorDNI(string dni)
        {
            return _guias
                .Where(g => g.DniDestinatario == dni && g.Estado == EstadoGuia.PendienteDeRetiroEnAgencia)
                .ToList();
        }

        /// <summary>
        /// Cambia el estado de una lista de guías a "Retirado".
        /// </summary>
        /// <param name="guiasARetirar">La lista de guías que se van a retirar.</param>
        public void ConfirmarRetiro(List<Guia> guiasARetirar)
        {
            foreach (var guia in guiasARetirar)
            {
                // Buscamos la guía en la lista original y cambiamos su estado
                var guiaOriginal = _guias.FirstOrDefault(g => g.NumeroGuia == guia.NumeroGuia);
                if (guiaOriginal != null)
                {
                    guiaOriginal.Estado = EstadoGuia.Retirado;
                }
            }
        }

        /// <summary>
        /// Método privado para generar datos de prueba.
        /// </summary>
        private void CargarDatosFicticios()
        {
            // Guías para el DNI 25111222
            _guias.Add(new Guia { NumeroGuia = "AGE001", DniDestinatario = "25111222", Estado = EstadoGuia.PendienteDeRetiroEnAgencia, TipoPaquete = TipoPaquete.S });
            _guias.Add(new Guia { NumeroGuia = "AGE002", DniDestinatario = "25111222", Estado = EstadoGuia.PendienteDeRetiroEnAgencia, TipoPaquete = TipoPaquete.L });

            // Guías para el DNI 32333444
            _guias.Add(new Guia { NumeroGuia = "AGE003", DniDestinatario = "32333444", Estado = EstadoGuia.PendienteDeRetiroEnAgencia, TipoPaquete = TipoPaquete.M });

            // Guías para el DNI 38555666 (una ya fue retirada para probar el filtro)
            _guias.Add(new Guia { NumeroGuia = "AGE004", DniDestinatario = "38555666", Estado = EstadoGuia.PendienteDeRetiroEnAgencia, TipoPaquete = TipoPaquete.XL });
            _guias.Add(new Guia { NumeroGuia = "AGE005", DniDestinatario = "38555666", Estado = EstadoGuia.Retirado, TipoPaquete = TipoPaquete.S });
        }
    }
}
