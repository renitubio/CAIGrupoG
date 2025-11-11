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
            var lista = new List<AgenciaCD>();

            // 1️⃣ Buscar el CD correspondiente a la ciudad
            var cd = ObtenerCDPorCiudad(ciudadId);
            if (cd != null)
            {
                lista.Add(cd); // se agrega al inicio
            }

            // 2️⃣ Agregar las agencias de la ciudad
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

        public AgenciaCD ObtenerCDPorCiudad(int ciudadId)
        {
            var ciudad = CiudadAlmacen.Ciudades.FirstOrDefault(c => c.CiudadID == ciudadId);
            if (ciudad == null)
                return null;

            var cd = CentroDistribucionAlmacen.CentrosDistribucion
                .FirstOrDefault(c => c.CD_ID == ciudad.CDID);

            if (cd == null)
                return null;

            // Se devuelve también como AgenciaCD para que el ComboBox pueda mostrarlo igual
            return new AgenciaCD
            {
                Id = cd.CD_ID,
                Nombre = cd.Nombre + " (Centro de Distribución)",
                CiudadId = ciudadId
            };
        }

        #endregion

        #region Lógica de Confirmación (Crear Guía y Hoja de Ruta)

        public List<string> ConfirmarImposicion(DatosImposicion datosImposicion)
        {
            if (_clienteActual == null)
            {
                throw new InvalidOperationException("Cliente no encontrado.");
            }

            int cdOrigenID = _clienteActual.CDOrigen;
            var guiasEntidadCreadas = new List<GuiaEntidad>();

            foreach (var item in datosImposicion.Items)
            {
                // Convertimos el string "S" de nuevo al Enum S
                TipoPaqueteEnum tipoPaquete = (TipoPaqueteEnum)Enum.Parse(typeof(TipoPaqueteEnum), item.Key);
                int cantidad = item.Value;

                // Creamos 'cantidad' guías de este 'tipoPaquete'
                for (int i = 0; i < cantidad; i++)
                {
                    string numeroGuia = $"GUI{_proximoNumeroGuia++:D3}";

                    var entidad = new GuiaEntidad
                    {
                        NumeroGuia = numeroGuia,
                        ClienteCUIT = _clienteActual.ClienteCUIT,
                        FechaAdmision = DateTime.Now,

                        // Lógica de Imposición en Agencia
                        Estado = EstadoEncomiendaEnum.ImpuestoCallCenter,
                        RetiroDomicilio = false, // Se entrega en agencia
                        EntregaAgencia = !datosImposicion.EntregaDomicilio, // Es true si NO es a domicilio
                        TipoPaquete = tipoPaquete,
                        CDOrigenID = cdOrigenID,
                        DNIAutorizadoRetirar = datosImposicion.DNIAutorizadoRetirar,
                        EntregaDomicilio = datosImposicion.EntregaDomicilio,
                        DomicilioDestino = datosImposicion.EntregaDomicilio ? datosImposicion.DomicilioDestino : "",
                        AgenciaDestinoID = datosImposicion.AgenciaDestinoID,
                        CDDestinoID = datosImposicion.CDDestinoID,

                        Importe = 0, // El precio se calculará después
                        NumeroFactura = 0,
                        Fecha = DateTime.Now // Asignamos la propiedad 'Fecha'
                    };

                    GuiaAlmacen.Nuevo(entidad);
                    guiasEntidadCreadas.Add(entidad);
                }
            }

            GuiaAlmacen.Grabar();
            return guiasEntidadCreadas.Select(g => g.NumeroGuia).ToList();
        }

        #endregion
    }
}
