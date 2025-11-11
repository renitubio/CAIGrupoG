using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CAIGrupoG.Imposicion.ImpAgencia
{
    public class ImposicionDeEncomiendaAgenciaModelo
    {
        private ClienteEntidad ?_clienteActual;

        private static int _proximoNumeroGuia = 1;

        //Preguntar a andres.
        public ImposicionDeEncomiendaAgenciaModelo()
        {
            BuscarUltimaGuia();
        }

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
        public List<string> ConfirmarImposicion(DatosImposicion datosImposicion)
        {
            if (_clienteActual == null)
            {
                throw new InvalidOperationException("Cliente no encontrado. Se debe buscar un cliente válido primero.");
            }
            int cdOrigenID = _clienteActual.CDOrigen;
            var guiasGeneradas = new List<string>();

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
                        Estado = EstadoEncomiendaEnum.ImpuestoAgencia, // Estado 2
                        RetiroDomicilio = false, // Se entrega en agencia
                        EntregaAgencia = !datosImposicion.EntregaDomicilio, // Es true si NO es a domicilio

                        CDOrigenID = cdOrigenID,
                        TipoPaquete = tipoPaquete,
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
                    guiasGeneradas.Add(numeroGuia);
                }
            }

            GuiaAlmacen.Grabar();

            return guiasGeneradas;
        }

    }

}