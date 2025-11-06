using CAIGrupoG.Almacenes;
using CAIGrupoG.Imposicion.ImpCentroDistribucion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Imposicion.ImpCallCenter
{
    public class ImposicionDeEncomiendaCallCenterModelo
    {
        private ClienteEntidad _clienteActual;
        private static int _proximoNumeroGuia = 1;
        private static int _proximoIdHDR = 1; // Contador para Hoja de Ruta

        public ImposicionDeEncomiendaCallCenterModelo()
        {
            BuscarUltimaGuia();
            BuscarUltimoIdHDR(); // Carga el último ID de Hoja de Ruta
        }

        #region Lógica de Contadores (Guía y HDR)

        private static void BuscarUltimaGuia()
        {
            var guias = GuiaAlmacen.Guias;
            if (guias == null || guias.Count == 0)
            {
                _proximoNumeroGuia = 1;
                return;
            }
            try
            {
                int maxNumero = guias
                    .Where(g => g.NumeroGuia != null && g.NumeroGuia.StartsWith("GUI"))
                    .Select(g => int.Parse(g.NumeroGuia.Substring(3)))
                    .DefaultIfEmpty(0)
                    .Max();
                _proximoNumeroGuia = maxNumero + 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al parsear guías: {ex.Message}");
                _proximoNumeroGuia = 1;
            }
        }

        private static void BuscarUltimoIdHDR()
        {
            // Asumimos que HojaDeRutaAlmacen ya cargó su JSON
            var hdrs = HojaDeRutaAlmacen.HojasDeRuta;
            if (hdrs == null || hdrs.Count == 0)
            {
                // Si el JSON está vacío, usamos el 4567 de tu ejemplo como base
                _proximoIdHDR = 4568;
                return;
            }
            try
            {
                int maxId = hdrs.Max(h => h.HDR_ID);
                _proximoIdHDR = maxId + 1;
            }
            catch
            {
                _proximoIdHDR = 4568; // Fallback
            }
        }

        #endregion

        #region Métodos de Búsqueda (Clientes, Ciudades, Agencias, etc.)

        public Cliente BuscarCliente(string cuit)
        {
            var clienteEntidad = ClienteAlmacen.Clientes.FirstOrDefault(c => c.ClienteCUIT == cuit);
            if (clienteEntidad != null)
            {
                _clienteActual = clienteEntidad;
                return new Cliente // Mapeo a la clase que espera la UI
                {
                    CUIT = clienteEntidad.ClienteCUIT,
                    RazonSocial = clienteEntidad.RazonSocial
                };
            }
            _clienteActual = null;
            return null;
        }

        public List<Ciudad> ObtenerCiudades()
        {
            return CentroDistribucionAlmacen.CentrosDistribucion
                .Select(cd => new Ciudad
                {
                    Id = cd.CD_ID,
                    Nombre = cd.Nombre,
                    CD_ID = cd.CD_ID
                }).ToList();
        }

        public List<TipoCaja> ObtenerTiposCaja()
        {
            // Mapeamos el Enum real (TipoPaqueteEnum) a la clase 'TipoCaja'
            return Enum.GetNames(typeof(TipoPaqueteEnum))
                .Select(nombre => new TipoCaja { Nombre = nombre })
                .ToList();
        }

        public List<AgenciaCD> ObtenerAgenciasPorCiudad(int ciudadId)
        {
            return AgenciaAlmacen.Agencias
                .Where(a => a.CiudadID == ciudadId)
                .Select(a => new AgenciaCD
                {
                    Id = a.AgenciaID,
                    Nombre = a.Nombre,
                    CiudadId = a.CiudadID
                }).ToList();
        }

        private FleteroEntidad BuscarFletero(int cdOrigen)
        {
            // Busca el primer fletero que trabaje para ese CD de Origen
            // (Gracias a tu FleteroEntidad.cs corregido, esto ahora carga)
            var fletero = FleteroAlmacen.Fleteros.FirstOrDefault(f => f.CD_ID == cdOrigen);
            if (fletero == null)
            {
                throw new InvalidOperationException($"No se encontró un fletero para el CD Origen ID: {cdOrigen}");
            }
            return fletero;
        }

        #endregion

        #region Lógica de Confirmación (Crear Guía y Hoja de Ruta)

        public List<string> ConfirmarImposicion(int cantidadTotalCajas, string codigoDestino)
        {
            if (_clienteActual == null)
            {
                throw new InvalidOperationException("Cliente no encontrado.");
            }

            // 1. Buscar Fletero (antes de crear las guías)
            var fleteroAsignado = BuscarFletero(_clienteActual.CDOrigen);
            var guiasEntidadCreadas = new List<GuiaEntidad>();

            // 2. Crear las Guías
            for (int i = 0; i < cantidadTotalCajas; i++)
            {
                string numeroGuia = $"GUI{_proximoNumeroGuia++:D3}";

                var entidad = new GuiaEntidad
                {
                    NumeroGuia = numeroGuia,
                    ClienteCUIT = _clienteActual.ClienteCUIT,
                    CDOrigenID = _clienteActual.CDOrigen,
                    FechaAdmision = DateTime.Now,

                    // Lógica de Call Center
                    Estado = EstadoEncomiendaEnum.ImpuestoCallCenter, // Estado 1
                    RetiroDomicilio = true, // El fletero lo retira

                    // AHORA COMPILA: Asignamos el fletero
                    DNIFletero = fleteroAsignado.FleteroDNI,

                    // TODO: Faltan datos que el Form no pasa
                    // (TipoPaquete, DNIAutorizadoRetirar, DomicilioDestino, etc.)
                };

                GuiaAlmacen.Nuevo(entidad);
                guiasEntidadCreadas.Add(entidad);
            }

            // 3. Grabar las Guías
            GuiaAlmacen.Grabar();

            // 4. Crear la Hoja de Ruta
            // ASUMO que TipoHDREnum existe y tiene el valor 'Retiro'
            CrearHojaDeRuta(guiasEntidadCreadas, fleteroAsignado, TipoHDREnum.Retiro);

            // 5. Devolver los números de guía al formulario
            return guiasEntidadCreadas.Select(g => g.NumeroGuia).ToList();
        }

        private void CrearHojaDeRuta(List<GuiaEntidad> guias, FleteroEntidad fletero, TipoHDREnum tipo)
        {
            if (guias == null || guias.Count == 0 || fletero == null)
                return;

            var nuevaHdr = new HojaDeRutaEntidad
            {
                HDR_ID = _proximoIdHDR++, // Asigna el próximo ID
                FechaCreacion = DateTime.Now,
                FleteroDNI = fletero.FleteroDNI,
                Tipo = tipo,
                Completada = false,
                // Convierte la List<GuiaEntidad> a List<string> (solo los IDs)
                GuiasAsignadas = guias.Select(g => g.NumeroGuia).ToList(),
                ServicioID = 0 // Usamos 0 o algún placeholder
            };

            HojaDeRutaAlmacen.Nuevo(nuevaHdr);
            HojaDeRutaAlmacen.Grabar();
        }

        #endregion
    }
}
