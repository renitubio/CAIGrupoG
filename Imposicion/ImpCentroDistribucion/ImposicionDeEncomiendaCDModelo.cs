using CAIGrupoG.Almacenes;
using CAIGrupoG.Imposicion.ImpAgencia;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Imposicion.ImpCentroDistribucion
{
    public class ImposicionDeEncomiendaCDModelo
    {
        private ClienteEntidad _clienteActual;
        private static int _proximoNumeroGuia = 1;
        private static int _proximoIdHDR = 1;
        private int _cdOrigenIDSeleccionado;

        public ImposicionDeEncomiendaCDModelo(int cdOrigenIDSeleccionado)
        {
            _cdOrigenIDSeleccionado = cdOrigenIDSeleccionado;
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

        #region Métodos de Búsqueda (Clientes, Ciudades, Agencias y CD)
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
            return CiudadAlmacen.Ciudades
                .Select(c => new Ciudad
                {
                    Id = c.CiudadID,
                    Nombre = c.Nombre,
                    CD_ID = c.CDID
                })
                .ToList();
        }

        public List<TipoCaja> ObtenerTiposCaja()
        {
            return Enum.GetNames(typeof(TipoPaqueteEnum))
                .Select(nombre => new TipoCaja { Nombre = nombre })
                .ToList();
        }

        /// Devuelve el Centro de Distribución (CD) correspondiente a una ciudad.
        public AgenciaCD ObtenerCDPorCiudad(int ciudadId)
        {
            var ciudad = CiudadAlmacen.Ciudades.FirstOrDefault(c => c.CiudadID == ciudadId);
            if (ciudad == null)
                return null;

            var cd = CentroDistribucionAlmacen.CentrosDistribucion
                .FirstOrDefault(c => c.CD_ID == ciudad.CDID);

            if (cd == null)
                return null;

            return new AgenciaCD
            {
                Id = cd.CD_ID,
                Nombre = cd.Nombre + " (Centro de Distribución)",
                CiudadId = ciudadId
            };
        }

        /// Devuelve una lista con el CD y las agencias de la ciudad indicada.
        public List<AgenciaCD> ObtenerAgenciasPorCiudad(int ciudadId)
        {
            var lista = new List<AgenciaCD>();

            // Agregar CD principal
            var cd = ObtenerCDPorCiudad(ciudadId);
            if (cd != null)
                lista.Add(cd);

            // Agregar agencias asociadas
            var agencias = AgenciaAlmacen.Agencias
                .Where(a => a.CiudadID == ciudadId)
                .Select(a => new AgenciaCD
                {
                    Id = a.AgenciaID,
                    Nombre = a.Nombre,
                    CiudadId = a.CiudadID
                });

            lista.AddRange(agencias);

            return lista;
        }

        private FleteroEntidad BuscarFletero(int cdOrigen)
        {
            // ASUMO que FleteroEntidad.cs está corregida
            var fletero = FleteroAlmacen.Fleteros.FirstOrDefault(f => f.CD_ID == cdOrigen);
            if (fletero == null)
            {
                throw new InvalidOperationException($"No se encontró un fletero para el CD Origen ID: {cdOrigen}");
            }
            return fletero;
        }

        private decimal CalcularImporte(TipoPaqueteEnum tipoPaquete, int cdOrigen, int cdDestino)
        {
            if (_clienteActual == null || _clienteActual.Tarifas == null)
            {
                throw new InvalidOperationException("Cliente o tarifario no cargado.");
            }

            // Busca en el tarifario específico del cliente
            var tarifaEncontrada = _clienteActual.Tarifas.FirstOrDefault(t =>
                t.TipoPaquete == tipoPaquete &&
                t.CDOrigen == cdOrigen &&
                t.CDDestino == cdDestino);

            if (tarifaEncontrada != null)
            {
                // Si encuentra la tarifa, devuelve el precio
                return tarifaEncontrada.Precio;
            }

            // Si no encuentra una tarifa, lanza un error.
            // Es mejor que devolver 0, para no permitir envíos gratis por error.
            throw new InvalidOperationException($"No se encontró una tarifa para el cliente {_clienteActual.ClienteCUIT}, Paquete: {tipoPaquete}, Origen: {cdOrigen}, Destino: {cdDestino}");
        }

        #endregion

        #region Lógica de Confirmación (Crear Guía y Hoja de Ruta)

        public List<string> ConfirmarAdmision(DatosImposicion datosImposicion)
        {
            if (_clienteActual == null)
            {
                throw new InvalidOperationException("Cliente no encontrado. Se debe buscar un cliente válido primero.");
            }
            int cdOrigenID = _cdOrigenIDSeleccionado;
            var guiasGeneradas = new List<GuiaEntidad>();
            var fleteroAsignado = BuscarFletero(cdOrigenID);

            foreach (var item in datosImposicion.Items)
            {
                // Convertimos el string "S" de nuevo al Enum S
                TipoPaqueteEnum tipoPaquete = (TipoPaqueteEnum)Enum.Parse(typeof(TipoPaqueteEnum), item.Key);
                int cantidad = item.Value;

                // Creamos 'cantidad' guías de este 'tipoPaquete'
                for (int i = 0; i < cantidad; i++)
                {
                    string numeroGuia = $"GUI{_proximoNumeroGuia++:D3}";
                    int cdDestinoReal = ObtenerCDPorCiudad(datosImposicion.CDDestinoID)?.Id
                                       ?? datosImposicion.CDDestinoID;
                    decimal importeCalculado = CalcularImporte(tipoPaquete, cdOrigenID, cdDestinoReal);
                    var entidad = new GuiaEntidad
                    {
                        NumeroGuia = numeroGuia,
                        ClienteCUIT = _clienteActual.ClienteCUIT,
                        FechaAdmision = DateTime.Now,

                        // Lógica de Imposición en Agencia
                        Estado = EstadoEncomiendaEnum.AdmitidoCDOrigen, // Estado2
                        RetiroDomicilio = false, // Se entrega en agencia
                        EntregaAgencia = !datosImposicion.EntregaDomicilio, // Es true si NO es a domicilio
                        CDOrigenID = cdOrigenID,
                        TipoPaquete = tipoPaquete,
                        DNIAutorizadoRetirar = datosImposicion.DNIAutorizadoRetirar,
                        EntregaDomicilio = datosImposicion.EntregaDomicilio,
                        DomicilioDestino = datosImposicion.EntregaDomicilio ? datosImposicion.DomicilioDestino : "",
                        AgenciaDestinoID = datosImposicion.AgenciaDestinoID,
                        CDDestinoID = datosImposicion.CDDestinoID,

                        Importe = importeCalculado,
                        NumeroFactura = 0,
                        Fecha = DateTime.Now // Asignamos la propiedad 'Fecha'
                    };

                    GuiaAlmacen.Nuevo(entidad);
                    guiasGeneradas.Add(entidad);
                }
            }

            GuiaAlmacen.Grabar();
            CrearHojaDeRuta(guiasGeneradas, fleteroAsignado, TipoHDREnum.Transporte);
            return guiasGeneradas.Select(g => g.NumeroGuia).ToList();
        }
        private void CrearHojaDeRuta(List<GuiaEntidad> guias, FleteroEntidad fletero, TipoHDREnum tipo)
        {
            if (guias == null || guias.Count == 0 || fletero == null)
                return;

            int cdOrigen = guias.First().CDOrigenID;
            int cdDestino = guias.First().CDDestinoID;
            int servicioIdAsignado = 0;

            try
            {
                // Usamos el ServicioAlmacen que ya cargó el JSON
                var servicioDisponible = ServicioAlmacen.Servicios
                    .FirstOrDefault(s => s.CDOrigen == cdOrigen && s.CDDestino == cdDestino);

                if (servicioDisponible != null)
                {
                    servicioIdAsignado = servicioDisponible.ServicioID;
                }
                else
                {
                    Debug.WriteLine($"⚠ No se encontró servicio para Origen {cdOrigen} y Destino {cdDestino}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error buscando servicio: {ex.Message}");
            }

            var nuevaHdr = new HojaDeRutaEntidad
            {
                HDR_ID = _proximoIdHDR++,
                FechaCreacion = DateTime.Now,
                FleteroDNI = fletero.FleteroDNI,
                Tipo = tipo,
                Completada = false,
                Guias = guias,
                ServicioID = servicioIdAsignado
            };

            HojaDeRutaAlmacen.Nuevo(nuevaHdr);
            HojaDeRutaAlmacen.Grabar();
        }


        #endregion
    }
}
