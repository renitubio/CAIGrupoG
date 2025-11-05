using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CAIGrupoG.Imposicion.ImpAgencia
{
    public class ImposicionDeEncomiendaAgenciaModelo
    {

        // Propiedad para almacenar el cliente encontrado
        private ClienteEntidad _clienteActual;

        // --- Lógica del Contador de Guías ---
        // Ya que el Almacen es "pasivo", el Modelo debe manejar la generación
        // del próximo número de guía, tal como lo hacía el modelo ficticio.
        private static int _proximoNumeroGuia = 1;
        private const string RUTA_CONTADOR = @"Datos\ContadorGuia.json";

        /// Constructor: Carga el estado persistente (el contador de guías).
        public ImposicionDeEncomiendaAgenciaModelo()
        {
            CargarContadorGuia();
        }

        #region Lógica del Contador de Guías (Persistente)

        private static void CargarContadorGuia()
        {
            try
            {
                // Asegurarse que el directorio exista (por si es la primera vez)
                var directorio = Path.GetDirectoryName(RUTA_CONTADOR);
                if (!string.IsNullOrEmpty(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                if (File.Exists(RUTA_CONTADOR))
                {
                    var json = File.ReadAllText(RUTA_CONTADOR);
                    _proximoNumeroGuia = JsonSerializer.Deserialize<int>(json);
                    if (_proximoNumeroGuia == 0) _proximoNumeroGuia = 1;
                }
            }
            catch (Exception)
            {
                _proximoNumeroGuia = 1; // En caso de error, resetea a 1
            }
        }

        private static void GrabarContadorGuia()
        {
            try
            {
                var json = JsonSerializer.Serialize(_proximoNumeroGuia);
                File.WriteAllText(RUTA_CONTADOR, json);
            }
            catch (Exception ex)
            {
                // Manejar error de guardado
                Console.WriteLine($"Error al grabar contador: {ex.Message}");
            }
        }

        #endregion

        #region Métodos Solicitados (con Lógica de Linq)

        /// <summary>
        /// Busca un cliente en la colección del Almacén usando Linq.
        /// </summary>
        public Cliente BuscarCliente(string cuit)
        {
            // Usamos Linq (FirstOrDefault) sobre la propiedad pública del Almacén
            var clienteEntidad = ClienteAlmacen.Clientes
                                     .FirstOrDefault(c => c.ClienteCUIT == cuit);

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

        /// <summary>
        /// Obtiene las "Ciudades" (Centros de Distribución) desde el Almacén.
        /// </summary>
        public List<Ciudad> ObtenerCiudades()
        {
            // ASUMO: 'CentroDistribucionAlmacen' tiene 'IReadOnlyCollection<CentroDistribucionEntidad> CentrosDistribucion'
            // ASUMO: 'CentroDistribucionEntidad' tiene 'CD_ID' y 'Nombre'
            return CentroDistribucionAlmacen.CentrosDistribucion
                .Select(cd => new Ciudad // Mapeo
                {
                    Id = cd.CD_ID,
                    Nombre = cd.Nombre,
                    CD_ID = cd.CD_ID
                }).ToList();
        }

        /// <summary>
        /// Obtiene los Tipos de Caja desde el Almacén.
        /// </summary>
        public List<TipoCaja> ObtenerTiposCaja()
        {
            // ASUMO: 'TipoCajaAlmacen' tiene 'IReadOnlyCollection<TipoCajaEntidad> TiposCaja'
            // ASUMO: 'TipoCajaEntidad' tiene 'Nombre'
            return TipoCajaAlmacen.TiposCaja
                .Select(tc => new TipoCaja // Mapeo
                {
                    Nombre = tc.Nombre
                }).ToList();
        }

        /// <summary>
        /// Obtiene el tarifario extra desde el Almacén.
        /// </summary>
        public List<TarifarioExtraEntidad> ObtenerTarifarioExtra()
        {
            // ASUMO: 'TarifarioExtraAlmacen' tiene 'IReadOnlyCollection<TarifarioExtraEntidad> TarifariosExtra'
            return TarifarioExtraAlmacen.TarifariosExtra.ToList();
        }

        #endregion

        #region Métodos Requeridos por el Formulario (con Lógica de Linq)

        /// <summary>
        /// Obtiene las Agencias filtradas por Ciudad usando Linq.
        /// </summary>
        public List<AgenciaCD> ObtenerAgenciasPorCiudad(int ciudadId)
        {
            // ASUMO: 'AgenciaAlmacen' tiene 'IReadOnlyCollection<AgenciaEntidad> Agencias'
            return AgenciaAlmacen.Agencias
                .Where(a => a.CiudadID == ciudadId) // Filtro con Linq
                .Select(a => new AgenciaCD // Mapeo
                {
                    Id = a.AgenciaID,
                    Nombre = a.Nombre,
                    CiudadId = a.CiudadID
                }).ToList();
        }

        /// <summary>
        /// Obtiene solo el CD de una ciudad, asumiendo que el ID de Agencia es igual al ID de Ciudad.
        /// </summary>
        public List<AgenciaCD> ObtenerCDPorCiudad(int ciudadId)
        {
            // ASUMO: 'AgenciaAlmacen' tiene 'IReadOnlyCollection<AgenciaEntidad> Agencias'
            return AgenciaAlmacen.Agencias
                .Where(a => a.CiudadID == ciudadId && a.AgenciaID == ciudadId) // Filtro con Linq
                .Select(a => new AgenciaCD // Mapeo
                {
                    Id = a.AgenciaID,
                    Nombre = a.Nombre,
                    CiudadId = a.CiudadID
                }).ToList();
        }

        /// <summary>
        /// Crea las Guías, las agrega al Almacén y persiste los cambios.
        /// </summary>
        public List<string> ConfirmarImposicion(int cantidadTotalCajas, string codigoDestino)
        {
            if (_clienteActual == null)
            {
                throw new InvalidOperationException("No se encontró un cliente. La búsqueda de CUIT debe ser exitosa antes de confirmar.");
            }

            var guiasGeneradas = new List<string>();
            var guiasEntidad = new List<GuiaEntidad>();

            // 1. El Modelo crea las entidades
            for (int i = 0; i < cantidadTotalCajas; i++)
            {
                string numeroGuia = $"{codigoDestino}-{_proximoNumeroGuia++:D6}";
                guiasGeneradas.Add(numeroGuia);

                var entidad = new GuiaEntidad
                {
                    NumeroGuia = numeroGuia,
                    Estado = EstadoEncomiendaEnum.ImpuestoEnAgencia, // ASUMO que este Enum existe
                    ClienteCUIT = _clienteActual.ClienteCUIT,
                    CDOrigenID = _clienteActual.CDOrigen, // ASUMO que ClienteEntidad tiene CDOrigen
                    FechaAdmision = DateTime.Now,
                    EntregaGuíaAgencia = true,

                    // TODO: El Formulario no está pasando el DNI, Domicilio, TipoPaquete, etc.
                    // Estos campos necesitarán ser pasados a este método o guardados en el Modelo.
                    // DNIAutorizadoRetirar = ...,
                    // DomicilioDestino = ...,
                    // EntregaDomicilio = ...,
                    // AgenciaDestinoID = ...,
                    // TipoPaquete = ...,
                    // Importe = ...
                };

                // 2. Le pasamos las nuevas entidades al Almacén
                // ASUMO: 'GuiaAlmacen' tiene un método estático 'Nuevo(GuiaEntidad)'
                GuiaAlmacen.Nuevo(entidad);
            }

            // 3. Le decimos al Almacén que se guarde
            // ASUMO: 'GuiaAlmacen' tiene 'Grabar()'
            GuiaAlmacen.Grabar();

            // 4. Guardamos nuestro contador de guías
            GrabarContadorGuia();

            // 5. Devolvemos los números (strings) que la UI espera
            return guiasGeneradas;
        }

    }

    #endregion

}