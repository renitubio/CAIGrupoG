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
                throw new InvalidOperationException("Cliente no encontrado. Se debe buscar un cliente válido primero.");

            int cdOrigenID = _clienteActual.CDOrigen;
            int cdDestinoID = datosImposicion.CDDestinoID;
            var guiasGeneradas = new List<GuiaEntidad>();

            // Obtener el ID del CD principal de la ciudad destino
            var ciudadDestino = CiudadAlmacen.Ciudades.FirstOrDefault(c => c.CiudadID == cdDestinoID);
            int cdPrincipalID = ciudadDestino != null ? ciudadDestino.CDID :0;

            foreach (var item in datosImposicion.Items)
            {
                TipoPaqueteEnum tipoPaquete = (TipoPaqueteEnum)Enum.Parse(typeof(TipoPaqueteEnum), item.Key);
                int cantidad = item.Value;

                for (int i =0; i < cantidad; i++)
                {
                    string numeroGuia = $"GUI{_proximoNumeroGuia++:D3}";
                    decimal importeCalculado = CalcularImporte(tipoPaquete, cdOrigenID, cdDestinoID);

                    var entidad = new GuiaEntidad
                    {
                        NumeroGuia = numeroGuia,
                        ClienteCUIT = _clienteActual.ClienteCUIT,
                        FechaAdmision = DateTime.Now,
                        Estado = EstadoEncomiendaEnum.AdmitidoCDOrigen,
                        RetiroDomicilio = false,
                        // EntregaAgencia = true solo si NO se seleccionó el CD principal
                        EntregaAgencia = datosImposicion.AgenciaDestinoID != cdPrincipalID,
                        CDOrigenID = cdOrigenID,
                        TipoPaquete = tipoPaquete,
                        DNIAutorizadoRetirar = datosImposicion.DNIAutorizadoRetirar,
                        EntregaDomicilio = datosImposicion.EntregaDomicilio,
                        DomicilioDestino = datosImposicion.EntregaDomicilio ? datosImposicion.DomicilioDestino : "",
                        AgenciaDestinoID = datosImposicion.AgenciaDestinoID,
                        CDDestinoID = cdDestinoID, 
                        Importe = importeCalculado,
                        NumeroFactura = 0,
                        Fecha = DateTime.Now
                    };

                    GuiaAlmacen.Nuevo(entidad);
                    guiasGeneradas.Add(entidad);
                }
            }

            GuiaAlmacen.Grabar();

            // Buscar la ruta de servicios (tramos) entre origen y destino
            var tramos = BuscarRutaDeServicios(cdOrigenID, cdDestinoID);
            if (tramos == null || tramos.Count ==0)
                throw new InvalidOperationException("No existe ruta de servicios entre el CD de origen y el CD de destino.");

            // Para cada tramo, crear una HDR y asociar la guía original (sin modificar sus campos)
            foreach (var tramo in tramos)
            {
                var fleteroAsignado = BuscarFletero(tramo.CDOrigen);

                // NO modificar los campos de la guía original aquí
                CrearHojaDeRuta(guiasGeneradas, fleteroAsignado, TipoHDREnum.Transporte, tramo.ServicioID);
            }

            return guiasGeneradas.Select(g => g.NumeroGuia).ToList();
        }

        // Estructura para representar un tramo de servicio
        private class TramoServicio
        {
            public int CDOrigen { get; set; }
            public int CDDestino { get; set; }
            public int ServicioID { get; set; }
        }

        // Busca la ruta de servicios entre dos CDs (puede ser directa o con escalas)
        private List<TramoServicio> BuscarRutaDeServicios(int cdOrigen, int cdDestino)
        {
            var servicios = ServicioAlmacen.Servicios;
            var ruta = new List<TramoServicio>();

            // Intentar encontrar un servicio directo
            var directo = servicios
                .Where(s => s.CDOrigen == cdOrigen && s.CDDestino == cdDestino)
                .OrderBy(s => s.FechaHoraSalida)
                .FirstOrDefault();
            
            if (directo != null)
            {
                ruta.Add(new TramoServicio { CDOrigen = cdOrigen, CDDestino = cdDestino, ServicioID = directo.ServicioID });
                return ruta;
            }

            // Si no hay directo, buscar escalas ordenadas por fecha de salida
            var primerosTramos = servicios
                .Where(s => s.CDOrigen == cdOrigen)
            .OrderBy(s => s.FechaHoraSalida)
             .ToList();

            foreach (var escala in primerosTramos)
            {
                // Buscar el siguiente tramo que conecte con el destino final
                var siguiente = servicios
     .Where(s => s.CDOrigen == escala.CDDestino && s.CDDestino == cdDestino)
            .OrderBy(s => s.FechaHoraSalida)
            .FirstOrDefault();
   
                if (siguiente != null)
  {
         ruta.Add(new TramoServicio { CDOrigen = escala.CDOrigen, CDDestino = escala.CDDestino, ServicioID = escala.ServicioID });
            ruta.Add(new TramoServicio { CDOrigen = siguiente.CDOrigen, CDDestino = siguiente.CDDestino, ServicioID = siguiente.ServicioID });
            return ruta;
        }
    }

    // Si no se encuentra ruta, retorna vacío
 return new List<TramoServicio>();
}
            
            // Modifica CrearHojaDeRuta para aceptar los parámetros de tramo y servicio
            private void CrearHojaDeRuta(List<GuiaEntidad> guias, FleteroEntidad fletero, TipoHDREnum tipo, int servicioId)
            {
                if (guias == null || guias.Count ==0 || fletero == null)
                    return;

                var nuevaHdr = new HojaDeRutaEntidad
                {
                    HDR_ID = _proximoIdHDR++,
                    FechaCreacion = DateTime.Now,
                    FleteroDNI = tipo == TipoHDREnum.Transporte ? "0" : fletero.FleteroDNI,
                    Tipo = tipo,
                    Completada = false,
                    Guias = guias.Select(g => g).ToList(),
                    ServicioID = servicioId
                };

                HojaDeRutaAlmacen.Nuevo(nuevaHdr);
                HojaDeRutaAlmacen.Grabar();
            }


            #endregion
        }
    }
