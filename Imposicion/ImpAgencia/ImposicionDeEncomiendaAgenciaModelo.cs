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
        private static int _proximoIdHDR = 1;
        private static int _proximoNumeroGuia = 1;

        //Preguntar a andres.
        public ImposicionDeEncomiendaAgenciaModelo()
        {
            BuscarUltimaGuia();
            BuscarUltimoIdHDR();
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

        private decimal CalcularImporte(TipoPaqueteEnum tipoPaquete, int cdOrigen, int cdDestino, bool entregaDomicilio, bool entregaAgencia)
        {
            if (_clienteActual == null || _clienteActual.Tarifas == null)
                throw new InvalidOperationException("Cliente o tarifario no cargado.");

            // Importe base
            var tarifaEncontrada = _clienteActual.Tarifas.FirstOrDefault(t =>
                t.TipoPaquete == tipoPaquete &&
                t.CDOrigen == cdOrigen &&
                t.CDDestino == cdDestino);

            if (tarifaEncontrada == null)
                throw new InvalidOperationException($"No se encontró una tarifa para el cliente {_clienteActual.ClienteCUIT}, Paquete: {tipoPaquete}, Origen: {cdOrigen}, Destino: {cdDestino}");

            decimal importe = tarifaEncontrada.Precio;

            // Sumar siempre el extra de retiro en agencia (TipoExtraEnum.RetiroAgencia =4)
            var extraRetiroAgencia = CAIGrupoG.Almacenes.TarifarioExtraAlmacen.TarifariosExtra
                .FirstOrDefault(e => e.Tipo == TipoExtraEnum.RetiroAgencia);
            if (extraRetiroAgencia != null)
                importe += extraRetiroAgencia.Precio;

            // Sumar extra por entrega en agencia si corresponde (TipoExtraEnum.EntregaAgencia =2)
            if (entregaAgencia)
            {
                var extraAgencia = CAIGrupoG.Almacenes.TarifarioExtraAlmacen.TarifariosExtra
                    .FirstOrDefault(e => e.Tipo == TipoExtraEnum.EntregaAgencia);
                if (extraAgencia != null)
                    importe += extraAgencia.Precio;
            }
            // Sumar extra por entrega en domicilio si corresponde (TipoExtraEnum.EntregaDomicilio =3)
            if (entregaDomicilio)
            {
                var extraDomicilio = CAIGrupoG.Almacenes.TarifarioExtraAlmacen.TarifariosExtra
                    .FirstOrDefault(e => e.Tipo == TipoExtraEnum.EntregaDomicilio);
                if (extraDomicilio != null)
                    importe += extraDomicilio.Precio;
            }

            return importe;
        }
        public List<string> ConfirmarImposicion(DatosImposicion datosImposicion)
        {
            if (_clienteActual == null)
                throw new InvalidOperationException("Cliente no encontrado.");

            int cdOrigenID = _clienteActual.CDOrigen;
            var guiasEntidadCreadas = new List<GuiaEntidad>();
            var guiasGeneradas = new List<string>();

            // Obtener el ID del CD principal de la ciudad destino
            var ciudadDestino = CiudadAlmacen.Ciudades.FirstOrDefault(c => c.CiudadID == datosImposicion.CDDestinoID);
            int cdPrincipalID = ciudadDestino != null ? ciudadDestino.CDID : 0;

            // Buscar fletero asignado al CD
            var fleteroAsignado = FleteroAlmacen.Fleteros
                .FirstOrDefault(f => f.CD_ID == cdOrigenID);

            if (fleteroAsignado == null)
                throw new InvalidOperationException($"No existe fletero asignado al CD {cdOrigenID}.");

            foreach (var item in datosImposicion.Items)
            {
                TipoPaqueteEnum tipoPaquete =
                (TipoPaqueteEnum)Enum.Parse(typeof(TipoPaqueteEnum), item.Key);

                int cantidad = item.Value;

                for (int i = 0; i < cantidad; i++)
                {
                    string numeroGuia = $"GUI{_proximoNumeroGuia++:D3}";

                    int cdDestinoReal =
                    ObtenerCDPorCiudad(datosImposicion.CDDestinoID)?.Id
                    ?? datosImposicion.CDDestinoID;

                    decimal importe = CalcularImporte(
                     tipoPaquete,
                     cdOrigenID,
                     cdDestinoReal,
                     datosImposicion.EntregaDomicilio,
                     datosImposicion.AgenciaDestinoID != cdPrincipalID
                    );

                   var entidad = new GuiaEntidad
                   {
                        NumeroGuia = numeroGuia,
                        ClienteCUIT = _clienteActual.ClienteCUIT,
                        FechaAdmision = DateTime.Now,
                        Estado = EstadoEncomiendaEnum.ImpuestoAgencia,
                        RetiroDomicilio = false,
                       // EntregaAgencia = true solo si NO se seleccionó el CD principal
                        EntregaAgencia = datosImposicion.AgenciaDestinoID != cdPrincipalID,
                        TipoPaquete = tipoPaquete,
                        CDOrigenID = cdOrigenID,
                        DNIAutorizadoRetirar = datosImposicion.DNIAutorizadoRetirar,
                        EntregaDomicilio = datosImposicion.EntregaDomicilio,
                        DomicilioDestino = datosImposicion.EntregaDomicilio ? datosImposicion.DomicilioDestino : "",
                        AgenciaDestinoID = datosImposicion.AgenciaDestinoID,
                        CDDestinoID = datosImposicion.CDDestinoID,
                        Importe = importe,
                        NumeroFactura = 0,
                        Fecha = DateTime.Now
                   };

                     GuiaAlmacen.Nuevo(entidad);
                       guiasEntidadCreadas.Add(entidad);
                           guiasGeneradas.Add(numeroGuia);
                }
            }

            // Guardar guías
            GuiaAlmacen.Grabar();

            // Crear UNA sola HDR con TODAS las guías
            CrearHojaDeRuta(guiasEntidadCreadas, fleteroAsignado, TipoHDREnum.Retiro);

            return guiasGeneradas;
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
                Tipo = tipo, // RETIRO
                Completada = false,
                Guias = guias,
                ServicioID = 0 // No se usa en Retiro
            };

            HojaDeRutaAlmacen.Nuevo(nuevaHdr);
            HojaDeRutaAlmacen.Grabar();
        }



    }

}