using CAIGrupoG.Almacenes;
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

        public ImposicionDeEncomiendaCDModelo()
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

        /// <summary>
        /// Devuelve el Centro de Distribución (CD) correspondiente a una ciudad.
        /// </summary>
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

        /// <summary>
        /// Devuelve una lista con el CD y las agencias de la ciudad indicada.
        /// </summary>
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

        #endregion

        #region Lógica de Confirmación (Generación de Guías en CD)

        public List<string> ConfirmarAdmision(int cantidadEncomiendas)
        {
            if (_clienteActual == null)
                throw new InvalidOperationException("Debe seleccionar un cliente antes de confirmar la admisión.");

            var guiasCreadas = new List<string>();

            for (int i = 0; i < cantidadEncomiendas; i++)
            {
                string numeroGuia = $"GUI{_proximoNumeroGuia++:D3}";

                var entidad = new GuiaEntidad
                {
                    NumeroGuia = numeroGuia,
                    ClienteCUIT = _clienteActual.ClienteCUIT,
                    FechaAdmision = DateTime.Now,
                    Estado = EstadoEncomiendaEnum.AdmitidoCDOrigen,
                    RetiroDomicilio = false
                };

                GuiaAlmacen.Nuevo(entidad);
                guiasCreadas.Add(numeroGuia);
            }

            GuiaAlmacen.Grabar();
            return guiasCreadas;
        }

        #endregion
    }
}
