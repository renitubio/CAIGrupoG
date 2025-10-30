using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CAIGrupoG.RendiciónFletero
{
    public class RendicionFleteroModelo
    {
        private readonly List<Fletero> _fleteros;

        public RendicionFleteroModelo()
        {
            _fleteros = new List<Fletero>();
            CargarDatosFicticios();
        }

        /// <summary>
        /// Busca un fletero por DNI y clasifica sus guías en Admisión y Retiro.
        /// </summary>
        /// <param name="dni">DNI del fletero a buscar.</param>
        /// <returns>Una tupla con dos listas: guías de admisión y guías de retiro.</returns>
        public (List<Guia> Admision, List<Guia> Retiro) BuscarGuiasPorDNI(string dni)
        {
            var fletero = _fleteros.FirstOrDefault(f => f.DNI == dni);

            var guiasAdmision = new List<Guia>();
            var guiasRetiro = new List<Guia>();

            if (fletero != null)
            {
                // Guías de Admisión: Las que el fletero trae al CD.
                guiasAdmision = fletero.GuiasAsignadas
                    .Where(g => g.Estado == EstadoEncomienda.DistribucionUltimaMillaAgencia ||
                                 g.Estado == EstadoEncomienda.PrimerIntentoDeEntrega ||
                                 g.Estado == EstadoEncomienda.EnCaminoARetirarDomicilio ||
                                 g.Estado == EstadoEncomienda.EnCaminoARetirarAgencia)
                    .ToList();

                // Guías de Retiro: Las que el fletero se lleva del CD.
                guiasRetiro = fletero.GuiasAsignadas
                    .Where(g => g.Estado == EstadoEncomienda.AdmitidoEnCDDestino)
                    .ToList();
            }

            return (guiasAdmision, guiasRetiro);
        }

        internal void CambioEstadoGuiasSelecc(List<Guia> guiasAdmSeleccion, List<Guia> guiasRetSeleccion)
        {
            if (guiasAdmSeleccion == null) guiasAdmSeleccion = new List<Guia>();
            if (guiasRetSeleccion == null) guiasRetSeleccion = new List<Guia>();

            // Actualiza las guías seleccionadas en Admisión según su estado actual
            foreach (var guia in guiasAdmSeleccion)
            {
                switch (guia.Estado)
                {
                    // "en camino a retirar..." -> "Admitido en CD origen"
                    case EstadoEncomienda.EnCaminoARetirarDomicilio:
                    case EstadoEncomienda.EnCaminoARetirarAgencia:
                        guia.Estado = EstadoEncomienda.AdmitidoEnCDOrigen;
                        break;

                    // "Distribución última milla Agencia" -> se mapea a "EnCDDestino" (se interpreta como pendiente de retiro en agencia)
                    case EstadoEncomienda.DistribucionUltimaMillaAgencia:
                        guia.Estado = EstadoEncomienda.AdmitidoEnCDDestino;
                        break;

                    // "Distribución última milla Domicilio" -> "Entregado"
                    case EstadoEncomienda.DistribucionUltimaMillaDomicilio:
                        guia.Estado = EstadoEncomienda.Entregado;
                        break;

                    default:
                        // Otros estados: no se cambia
                        break;
                }
            }

            // Actualiza las guías seleccionadas para retiro (se asume que se llevan desde CD origen)
            foreach (var guia in guiasRetSeleccion)
            {
                // "Admitido en CD destino" -> pasar a distribución última milla domicilio/agencia según destino
                if (!string.IsNullOrEmpty(guia.Destino) &&
                    guia.Destino.IndexOf("Agencia", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    guia.Estado = EstadoEncomienda.DistribucionUltimaMillaAgencia;
                }
                else
                {
                    guia.Estado = EstadoEncomienda.DistribucionUltimaMillaDomicilio;
                }
                break;
            }

            // Guías no seleccionadas en Admisión:
            //  - las que estaban en DistribucionUltimaMillaDomicilio pasan a PrimerIntentoDeEntrega.
            //  - las que ya estaban en PrimerIntentoDeEntrega y no fueron seleccionadas pasan a Rechazado.
            var todasGuias = _fleteros.SelectMany(f => f.GuiasAsignadas).ToList();

            // Capturar primero las guías que ya estaban en PrimerIntentoDeEntrega y no fueron seleccionadas,
            // para convertirlas en Rechazado según el nuevo requerimiento.
            var primerIntentoPrevioNoSeleccionadas = todasGuias
                .Where(g => g.Estado == EstadoEncomienda.PrimerIntentoDeEntrega &&
                            !guiasAdmSeleccion.Any(sel => sel.NumeroGuia == g.NumeroGuia))
                .ToList();

            var distribDomicilioNoSeleccionadas = todasGuias
                .Where(g => g.Estado == EstadoEncomienda.DistribucionUltimaMillaDomicilio &&
                            !guiasAdmSeleccion.Any(sel => sel.NumeroGuia == g.NumeroGuia))
                .ToList();

            // Las que estaban en Distribución última milla domicilio no seleccionadas -> Primer intento de entrega
            foreach (var guia in distribDomicilioNoSeleccionadas)
            {
                guia.Estado = EstadoEncomienda.PrimerIntentoDeEntrega;
            }

            // Las que ya estaban en PrimerIntentoDeEntrega y no fueron seleccionadas -> Rechazado
            foreach (var guia in primerIntentoPrevioNoSeleccionadas)
            {
                guia.Estado = EstadoEncomienda.Rechazado;
            }
        }





        /// <summary>
        /// Método privado para generar los datos de prueba.
        /// </summary>
        private void CargarDatosFicticios()
        {
            // Fletero 1
            var fletero1 = new Fletero { DNI = "30123456", Nombre = "Juan Perez" };
            fletero1.GuiasAsignadas.Add(new Guia { NumeroGuia = "GA001", Estado = EstadoEncomienda.AdmitidoEnCDDestino, TipoPaquete = TipoPaquete.M, CUIT = "20-11111111-1", DniAutorizadoRetirar = "32345655", Destino = "Av. Corrientes 1234, CABA" });
            fletero1.GuiasAsignadas.Add(new Guia { NumeroGuia = "GA002", Estado = EstadoEncomienda.DistribucionUltimaMillaAgencia, TipoPaquete = TipoPaquete.S, CUIT = "20-22222222-2", DniAutorizadoRetirar = "12345678", Destino = "Agencia Flores" });
            fletero1.GuiasAsignadas.Add(new Guia { NumeroGuia = "GA003", Estado = EstadoEncomienda.PrimerIntentoDeEntrega, TipoPaquete = TipoPaquete.L, CUIT = "20-33333333-3", DniAutorizadoRetirar = "87654321", Destino = "Agencia Belgrano" });

            // Fletero 2
            var fletero2 = new Fletero { DNI = "35987654", Nombre = "Maria Lopez" };
            fletero2.GuiasAsignadas.Add(new Guia { NumeroGuia = "GB004", Estado = EstadoEncomienda.AdmitidoEnCDDestino, TipoPaquete = TipoPaquete.XL, CUIT = "27-44444444-4", DniAutorizadoRetirar = "22345600", Destino = "Calle Falsa 123, Springfield" });
            fletero2.GuiasAsignadas.Add(new Guia { NumeroGuia = "GB005", Estado = EstadoEncomienda.AdmitidoEnCDDestino, TipoPaquete = TipoPaquete.S, CUIT = "27-55555555-5", DniAutorizadoRetirar = "33345600", Destino = "Agencia Caballito" });

            // Fletero 3 (solo tiene guías de admisión)
            var fletero3 = new Fletero { DNI = "40111222", Nombre = "Carlos Garcia" };
            fletero3.GuiasAsignadas.Add(new Guia { NumeroGuia = "GC006", Estado = EstadoEncomienda.EnCaminoARetirarDomicilio, TipoPaquete = TipoPaquete.M, CUIT = "20-66666666-6", DniAutorizadoRetirar = "22333444", Destino = "Agencia Palermo" });

            _fleteros.Add(fletero1);
            _fleteros.Add(fletero2);
            _fleteros.Add(fletero3);
        }
        

    }
}
