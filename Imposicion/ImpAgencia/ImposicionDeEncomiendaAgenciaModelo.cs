using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAIGrupoG.Imposicion.ImpAgencia
{
    public class ImposicionDeEncomiendaAgenciaModelo
    {
        private ClienteEntidad ?_clienteActual;

        private static int _proximoNumeroGuia = 1;

        public Cliente BuscarCliente(string cuit)
        {
            var clienteEntidad = ClienteAlmacen.Clientes
                                     .FirstOrDefault(c => c.ClienteCUIT == cuit);

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
            string[] nombresEnum = Enum.GetNames(typeof(TipoPaqueteEnum));

            List<TipoCaja> tiposCaja = new List<TipoCaja>();
            foreach (string nombre in nombresEnum)
            {
                tiposCaja.Add(new TipoCaja { Nombre = nombre });
            }
            return tiposCaja;
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

        public List<AgenciaCD> ObtenerCDPorCiudad(int ciudadId)
        {
            return AgenciaAlmacen.Agencias
                .Where(a => a.CiudadID == ciudadId && a.AgenciaID == ciudadId)
                .Select(a => new AgenciaCD
                {
                    Id = a.AgenciaID,
                    Nombre = a.Nombre,
                    CiudadId = a.CiudadID
                }).ToList();
        }

        #region Lógica del Contador de Guías (Búsqueda)
        private static void BuscarUltimaGuia()
        {
            var guias = GuiaAlmacen.Guias;

            if (guias == null || guias.Count == 0)
            {
                _proximoNumeroGuia = 1; // No hay guías, empezamos en 1
                return;
            }

            try
            {
                int maxNumero = 0;
                foreach (var guia in guias)
                {
                    // Formato esperado: "GUI001", "GUI006", etc.
                    if (guia.NumeroGuia != null && guia.NumeroGuia.StartsWith("GUI"))
                    {
                        // Extrae la parte numérica, ej: "001"
                        string numeroStr = guia.NumeroGuia.Substring(3);

                        if (int.TryParse(numeroStr, out int numero))
                        {
                            if (numero > maxNumero)
                            {
                                maxNumero = numero;
                            }
                        }
                    }
                }
                // Si maxNumero es 6, el próximo es 7.
                _proximoNumeroGuia = maxNumero + 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] Error al parsear números de guía: {ex.Message}");
                _proximoNumeroGuia = 1; // Fallback en caso de error
            }
        }

        public List<string> ConfirmarImposicion(int cantidadTotalCajas, string codigoDestino)
        {
            if (_clienteActual == null)
            {
                throw new InvalidOperationException("Cliente no encontrado. Se debe buscar un cliente válido primero.");
            }

            var guiasGeneradas = new List<string>();

            for (int i = 0; i < cantidadTotalCajas; i++)
            {
                string numeroGuia = $"GUI{_proximoNumeroGuia++:D3}";
                guiasGeneradas.Add(numeroGuia);

                var entidad = new GuiaEntidad
                {
                    NumeroGuia = numeroGuia,
                    Estado = EstadoEncomiendaEnum.ImpuestoAgencia, // ASUMO Enum existe
                    ClienteCUIT = _clienteActual.ClienteCUIT,
                    CDOrigenID = _clienteActual.CDOrigen, // ASUMO ClienteEntidad tiene CDOrigen
                    FechaAdmision = DateTime.Now,
                    EntregaAgencia = true,
                    // TODO: Faltan DNI, Domicilio, etc. que el Form no pasa.
                };

                GuiaAlmacen.Nuevo(entidad);
            }

            GuiaAlmacen.Grabar();

            return guiasGeneradas;
        }

    }

}