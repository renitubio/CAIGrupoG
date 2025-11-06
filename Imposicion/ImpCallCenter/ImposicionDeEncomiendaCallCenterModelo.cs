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
        private static int _proximoIdHDR = 1;

        public ImposicionDeEncomiendaCallCenterModelo()
        {
            BuscarUltimaGuia();
            BuscarUltimoIdHDR();
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
            // ASUMO que HojaDeRutaEntidad.cs se corrigió para usar List<string> Guias
            // y [JsonPropertyName], de lo contrario esto crashea.
            var hdrs = HojaDeRutaAlmacen.HojasDeRuta;

            if (hdrs == null || hdrs.Count == 0)
            {
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
                _proximoIdHDR = 4568;
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
                return new Cliente
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
            // Asumiendo que CiudadEntidad.cs usa 'int CiudadID'
            return CiudadAlmacen.Ciudades
                .Select(c => new Ciudad
                {
                    Id = c.CiudadID,
                    Nombre = c.Nombre,
                    CD_ID = c.CDID
                }).ToList();
        }

        public List<TipoCaja> ObtenerTiposCaja()
        {
            return Enum.GetNames(typeof(TipoPaqueteEnum))
                .Select(nombre => new TipoCaja { Nombre = nombre })
                .ToList();
        }

        public List<AgenciaCD> ObtenerAgenciasPorCiudad(int ciudadId)
        {
            var opciones = new List<AgenciaCD>();

            // 1. Buscar el CD asociado a esa Ciudad
            var ciudadSeleccionada = CiudadAlmacen.Ciudades.FirstOrDefault(c => c.CiudadID == ciudadId);

            if (ciudadSeleccionada != null)
            {
                var cd = CentroDistribucionAlmacen.CentrosDistribucion
                           .FirstOrDefault(c => c.CD_ID == ciudadSeleccionada.CDID);

                if (cd != null)
                {
                    opciones.Add(new AgenciaCD
                    {
                        Id = cd.CD_ID,
                        Nombre = cd.Nombre + " (Centro de Distribución)",
                        CiudadId = ciudadId
                    });
                }
            }

            // 2. Buscar las Agencias asociadas a esa Ciudad
            // ASUMO que AgenciaEntidad.cs está corregida (usa Dictionary<string, decimal>)
            var agencias = AgenciaAlmacen.Agencias
                .Where(a => a.CiudadID == ciudadId)
                .Select(a => new AgenciaCD
                {
                    Id = a.AgenciaID,
                    Nombre = a.Nombre,
                    CiudadId = a.CiudadID
                });

            opciones.AddRange(agencias);

            return opciones;
        }

        private FleteroEntidad BuscarFletero(int cdOrigen)
        {
            // ASUMO que FleteroEntidad.cs está corregida (usa [JsonPropertyName] y List<Dictionary...>)
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

            var fleteroAsignado = BuscarFletero(_clienteActual.CDOrigen);
            var guiasEntidadCreadas = new List<GuiaEntidad>();

            for (int i = 0; i < cantidadTotalCajas; i++)
            {
                string numeroGuia = $"GUI{_proximoNumeroGuia++:D3}";

                var entidad = new GuiaEntidad
                {
                    NumeroGuia = numeroGuia,
                    ClienteCUIT = _clienteActual.ClienteCUIT,
                    CDOrigenID = _clienteActual.CDOrigen,
                    FechaAdmision = DateTime.Now,
                    Estado = EstadoEncomiendaEnum.ImpuestoCallCenter, // Estado 1
                    RetiroDomicilio = true,
                    // Se quita DNIFletero, ya que no existe en GuiaEntidad
                };

                GuiaAlmacen.Nuevo(entidad);
                guiasEntidadCreadas.Add(entidad);
            }

            GuiaAlmacen.Grabar();

            CrearHojaDeRuta(guiasEntidadCreadas, fleteroAsignado, TipoHDREnum.Retiro);

            return guiasEntidadCreadas.Select(g => g.NumeroGuia).ToList();
        }

        private void CrearHojaDeRuta(List<GuiaEntidad> guias, FleteroEntidad fletero, TipoHDREnum tipo)
        {
            if (guias == null || guias.Count == 0 || fletero == null)
                return;

            var nuevaHdr = new HojaDeRutaEntidad
            {
                HDR_ID = _proximoIdHDR++,
                FechaCreacion = DateTime.Now,
                FleteroDNI = fletero.FleteroDNI,
                Tipo = tipo,
                Completada = false,

                // --- ¡CORRECCIÓN! ---
                // Convertimos la lista de OBJETOS (List<GuiaEntidad>)
                // en la lista de STRINGS (List<string>) que la Entidad espera.
                Guias = guias.Select(g => g.NumeroGuia).ToList(),

                ServicioID = 0
            };

            HojaDeRutaAlmacen.Nuevo(nuevaHdr);
            HojaDeRutaAlmacen.Grabar();
        }

        #endregion
    }
}
