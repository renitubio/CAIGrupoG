using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAIGrupoG.Playero
{
    public class PlayeroModelo
    {
        public int NuestroCD { get; set; }

        public int cdSeleccionado = CentroDistribucionAlmacen.CentroDistribucionActual.CD_ID;

        public PlayeroModelo()
        {
            // Si se llama al constructor vacío, usa el valor mock inicializado.
            NuestroCD = cdSeleccionado;
            if (NuestroCD == 0)
            {
                throw new InvalidOperationException("No se ha seleccionado un Centro de Distribución en el Menú Principal.");
            }
        }
       
        // Si se mantiene el constructor con parámetro, se usa este:
        public PlayeroModelo(int cdSeleccionado)
        {
            NuestroCD = cdSeleccionado;
        }

        public (List<Guia> Cargas, List<Guia> Descargas) BuscarGuiasPorPatente(string patente)
        {
            var patenteUpper = patente.ToUpper();

            // 1. Identificar el Servicio de CARGA PENDIENTE (Salida desde NuestroCD)
            var servicioCarga = ServicioAlmacen.servicios
                .Where(s => s.Patente.ToUpper() == patenteUpper && s.CDOrigen == NuestroCD)
                .OrderBy(s => s.FechaHoraSalida)
                .FirstOrDefault();

            // 2. Identificar el Servicio de DESCARGA PENDIENTE (Llegada a NuestroCD)
            var servicioDescarga = ServicioAlmacen.servicios
                .Where(s => s.Patente.ToUpper() == patenteUpper && s.CDDestino == NuestroCD)
                .OrderBy(s => s.FechaHoraLlegada)
                .FirstOrDefault();

            var cargas = new List<Guia>();
            var descargas = new List<Guia>();

            // 3. Obtener Guías de Carga
            if (servicioCarga != null)
            {
                var hojaCarga = HojaDeRutaAlmacen.HojasDeRuta.FirstOrDefault(h =>
                    h.ServicioID == servicioCarga.ServicioID &&
                    //h.Tipo == TipoHDREnum.Transporte && // Asumo Tipo=2 para Transporte
                    h.Completada == false); // El servicio está pendiente de realizar

                if (hojaCarga != null && hojaCarga.Guias != null)
                {
                    var numerosGuiaCarga = hojaCarga.Guias.Select(g => g.NumeroGuia);

                    // Reemplazar la asignación de 'cargas' para convertir de GuiaEntidad a Guia
                    cargas = GuiaAlmacen.Guias
                        .Where(g => numerosGuiaCarga.Contains(g.NumeroGuia))
                        .Select(g => new Guia
                        {
                            NumeroGuia = g.NumeroGuia,
                            Estado = (EstadoGuia)(int)g.Estado // Conversión explícita de enum si es necesario
                            // Agregar otros campos si la clase Guia lo requiere
                        })
                        .ToList();
                }
            }

            // 4. Obtener Guías de Descarga
            if (servicioDescarga != null)
            {
                // CS0103 CORREGIDO: Declaración de hojaDescarga aquí
                var hojaDescarga = HojaDeRutaAlmacen.HojasDeRuta.FirstOrDefault(h =>
                    h.ServicioID == servicioDescarga.ServicioID &&
                    //h.Tipo == TipoHDREnum.Transporte && // Tipo=2 para Transporte
                    h.Completada == false); // El servicio está pendiente de realizar

                if (hojaDescarga != null && hojaDescarga.Guias != null)
                {
                    var numerosGuiaDescarga = hojaDescarga.Guias.Select(g => g.NumeroGuia);

                    // Reemplaza la asignación de 'descargas' para convertir de GuiaEntidad a Guia
                    descargas = GuiaAlmacen.Guias
                        .Where(g => numerosGuiaDescarga.Contains(g.NumeroGuia))
                        .Select(g => new Guia
                        {
                            NumeroGuia = g.NumeroGuia,
                            Estado = (EstadoGuia)(int)g.Estado
                            // Agrega otros campos si la clase Guia lo requiere
                        })
                        .ToList();
                }
            }

            return (cargas, descargas);
        }

        // Método principal para confirmar y procesar la operación
        public Dictionary<string, List<GuiaEntidad>> ConfirmarOperacion(List<Guia> cargasSeleccionadas, List<Guia> descargasSeleccionadas)
        {
            if ((cargasSeleccionadas == null || !cargasSeleccionadas.Any()) && (descargasSeleccionadas == null || !descargasSeleccionadas.Any()))
            {
                throw new ArgumentException("Debe seleccionar al menos una guía para Cargar o Descargar.");
            }

            // 1. Actualización de Estados para Cargas (Estado 5 -> 6: EnTransito)
            if (cargasSeleccionadas != null)
            {
                foreach (var guia in cargasSeleccionadas)
                {
                    var guiaEnAlmacen = GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == guia.NumeroGuia);
                    if (guiaEnAlmacen != null)
                    {
                        guiaEnAlmacen.Estado = EstadoEncomiendaEnum.EnTransito; // Estado 6
                        GuiaAlmacen.Actualizar(guiaEnAlmacen);
                    }
                }
            }

            var agrupadasParaDistribucion = new Dictionary<string, List<GuiaEntidad>>();

            // 2. Actualización de Estados para Descargas (Estado 6 -> 7: AdmitidoCDDestino)
            if (descargasSeleccionadas != null)
            {
                // Casteo de descargasSeleccionadas a GuiaEntidad
                var descargasSeleccionadasEntidad = descargasSeleccionadas
                    .Select(g => GuiaAlmacen.Guias.FirstOrDefault(ge => ge.NumeroGuia == g.NumeroGuia))
                    .Where(ge => ge != null)
                    .ToList();

                foreach (var guiaEntidad in descargasSeleccionadasEntidad)
                {
                    guiaEntidad.Estado = EstadoEncomiendaEnum.AdmitidoCDDestino; // Estado 7
                    GuiaAlmacen.Actualizar(guiaEntidad);
                }

                // 3. Agrupar Descargas para la futura Hoja de Ruta de Distribución

                var porDomicilio = descargasSeleccionadasEntidad
                    .Where(g => g.EntregaDomicilio)
                    .GroupBy(g => g.DomicilioDestino)
                    .ToDictionary(g => $"Domicilio: {g.Key}", g => g.ToList());
                foreach (var kvp in porDomicilio)
                    agrupadasParaDistribucion[kvp.Key] = kvp.Value;

                var porAgencia = descargasSeleccionadasEntidad
                    .Where(g => g.EntregaAgencia)
                    .GroupBy(g => g.AgenciaDestinoID)
                    .ToDictionary(g => $"Agencia: {g.Key}", g => g.ToList());
                foreach (var kvp in porAgencia)
                    agrupadasParaDistribucion[kvp.Key] = kvp.Value;

                var enCD = descargasSeleccionadasEntidad
                    .Where(g => !g.EntregaDomicilio && !g.EntregaAgencia)
                    .ToList();
                if (enCD.Any())
                    agrupadasParaDistribucion["Entrega en CD"] = enCD;

                // 4. Crear Hoja de Ruta de Distribución solo si hay guías para descargar/distribuir
                if (agrupadasParaDistribucion.Any())
                {
                    CrearHojasDeRutaDistribucion(agrupadasParaDistribucion);
                }
            }

            // 5. Persistencia de datos
            GuiaAlmacen.Grabar();

            return agrupadasParaDistribucion;
        }

        // Método para crear las Hojas de Ruta de Distribución
        public List<HojaDeRutaEntidad> CrearHojasDeRutaDistribucion(Dictionary<string, List<GuiaEntidad>> agrupadas)
        {
            var hojas = new List<HojaDeRutaEntidad>();
            // 1. Obtener el último ID de HDR y sumarle 1
            int ultimoId = HojaDeRutaAlmacen.HojasDeRuta.Any() ? HojaDeRutaAlmacen.HojasDeRuta.Max(h => h.HDR_ID) : 0;

            // 2. Obtener el DNI del Fletero asociado al CD actual
            var fletero = FleteroAlmacen.Fleteros.FirstOrDefault(f => f.CD_ID == NuestroCD);
            string fleteroDNI = fletero?.FleteroDNI ?? string.Empty;

            foreach (var grupo in agrupadas)
            {
                var hoja = new HojaDeRutaEntidad
                {
                    HDR_ID = ++ultimoId,
                    ServicioID = 0,
                    FechaCreacion = DateTime.Now,
                    FleteroDNI = fleteroDNI,
                    Tipo = TipoHDREnum.Distribucion, // Tipo 3 para Distribución
                    Completada = false,
                    Guias = grupo.Value.Select(g => new GuiaEntidad { NumeroGuia = g.NumeroGuia }).ToList()
                };

                HojaDeRutaAlmacen.Nuevo(hoja);
                hojas.Add(hoja);
            }

            // 3. Persistencia de la Hoja de Ruta
            HojaDeRutaAlmacen.Grabar();
            return hojas;
        }
    }
}