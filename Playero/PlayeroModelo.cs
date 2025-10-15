using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Playero
{
    public class PlayeroModelo
    {
        // Lista interna para almacenar los datos de los vehículos y sus guías.
        private readonly List<Vehiculo> _vehiculos;

        // Definimos cuál es nuestro Centro de Distribución (CD) actual.
        private const string NuestroCD = "CD01";

        public PlayeroModelo()
        {
            // Inicializamos y cargamos los datos ficticios al crear el modelo.
            _vehiculos = new List<Vehiculo>();
            CargarDatosFicticios();
        }

        /// <summary>
        /// Busca un vehículo por su patente y clasifica sus guías en Carga y Descarga.
        /// </summary>
        /// <param name="patente">La patente del vehículo a buscar.</param>
        /// <returns>Una tupla con dos listas: guías para cargar y guías para descargar.</returns>
        public (List<Guia> Cargas, List<Guia> Descargas) BuscarGuiasPorPatente(string patente)
        {
            var vehiculo = _vehiculos.FirstOrDefault(v => v.Patente.Equals(patente, StringComparison.OrdinalIgnoreCase));

            var guiasCarga = new List<Guia>();
            var guiasDescarga = new List<Guia>();

            if (vehiculo != null)
            {
                // Guías de Carga: son las que tienen nuestro CD como origen.
                guiasCarga = vehiculo.GuiasAsignadas
                    .Where(g => g.CDOrigen == NuestroCD && g.Estado == EstadoGuia.Pendiente)
                    .ToList();

                // Guías de Descarga: son las que tienen nuestro CD como destino.
                guiasDescarga = vehiculo.GuiasAsignadas
                    .Where(g => g.CDDestino == NuestroCD && g.Estado == EstadoGuia.EnTransito)
                    .ToList();
            }

            return (guiasCarga, guiasDescarga);
        }

        /// <summary>
        /// Cambia el estado de las guías procesadas.
        /// </summary>
        /// <param name="cargas">Lista de guías que se cargaron.</param>
        /// <param name="descargas">Lista de guías que se descargaron.</param>
        public void ConfirmarOperacion(List<Guia> cargas, List<Guia> descargas)
        {
            // A las guías cargadas se les cambia el estado a "En Tránsito".
            foreach (var guia in cargas)
            {
                guia.Estado = EstadoGuia.EnTransito;
            }

            // A las guías descargadas se les cambia el estado a "Procesado".
            foreach (var guia in descargas)
            {
                guia.Estado = EstadoGuia.Procesado;
            }
        }

        /// <summary>
        /// Método privado para generar y cargar los datos de prueba.
        /// </summary>
        private void CargarDatosFicticios()
        {
            // Vehículo 1
            var vehiculo1 = new Vehiculo { Patente = "AA123BC" };
            vehiculo1.GuiasAsignadas.Add(new Guia { NumeroGuia = "G001", TipoPaquete = TipoPaquete.S, CUIT = "30-11111111-1", CDOrigen = NuestroCD, CDDestino = "CD02", Estado = EstadoGuia.Pendiente });
            vehiculo1.GuiasAsignadas.Add(new Guia { NumeroGuia = "G002", TipoPaquete = TipoPaquete.M, CUIT = "30-22222222-2", CDOrigen = NuestroCD, CDDestino = "CD03", Estado = EstadoGuia.Pendiente });
            vehiculo1.GuiasAsignadas.Add(new Guia { NumeroGuia = "G003", TipoPaquete = TipoPaquete.L, CUIT = "30-33333333-3", CDOrigen = "CD04", CDDestino = NuestroCD, Estado = EstadoGuia.EnTransito });

            // Vehículo 2
            var vehiculo2 = new Vehiculo { Patente = "AD456FE" };
            vehiculo2.GuiasAsignadas.Add(new Guia { NumeroGuia = "G004", TipoPaquete = TipoPaquete.XL, CUIT = "30-44444444-4", CDOrigen = "CD05", CDDestino = NuestroCD, Estado = EstadoGuia.EnTransito });
            vehiculo2.GuiasAsignadas.Add(new Guia { NumeroGuia = "G005", TipoPaquete = TipoPaquete.S, CUIT = "30-55555555-5", CDOrigen = "CD02", CDDestino = NuestroCD, Estado = EstadoGuia.EnTransito });

            // Vehículo 3
            var vehiculo3 = new Vehiculo { Patente = "AE789GH" };
            vehiculo3.GuiasAsignadas.Add(new Guia { NumeroGuia = "G006", TipoPaquete = TipoPaquete.M, CUIT = "30-66666666-6", CDOrigen = NuestroCD, CDDestino = "CD05", Estado = EstadoGuia.Pendiente });
            vehiculo3.GuiasAsignadas.Add(new Guia { NumeroGuia = "G007", TipoPaquete = TipoPaquete.L, CUIT = "30-77777777-7", CDOrigen = NuestroCD, CDDestino = "CD04", Estado = EstadoGuia.Pendiente });

            _vehiculos.Add(vehiculo1);
            _vehiculos.Add(vehiculo2);
            _vehiculos.Add(vehiculo3);
        }
    }
}
