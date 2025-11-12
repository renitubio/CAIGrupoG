using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAIGrupoG.Playero
{
    public class PlayeroModelo
    {
        public int NuestroCD { get; set; }
        public PlayeroModelo()
        {
            // Verifica si CentroDistribucionActual es null antes de intentar acceder a CD_ID.
            if (CentroDistribucionAlmacen.CentroDistribucionActual != null)
            {
                NuestroCD = CentroDistribucionAlmacen.CentroDistribucionActual.CD_ID;
            }
            else
            {
                // Si es null, NuestroCD se mantiene en 0 (valor por defecto de int)
                // o puedes asignarle explícitamente 0, aunque ya es 0 por defecto si no se asigna.
                NuestroCD = 0;
            }
            // Si se llama al constructor vacío, usa el valor mock inicializado.
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
                            TipoPaquete = (TipoPaquete)((int)g.TipoPaquete - 1), // Map TipoPaqueteEnum (1-4) to TipoPaquete (0-3)
                            CUIT = g.ClienteCUIT, // Asumo que el CUIT del cliente es lo que quieres mostrar
                            CDOrigen = g.CDOrigenID.ToString(),
                            CDDestino = g.CDDestinoID.ToString(),
                            Estado = (EstadoGuia)(int)g.Estado
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
                            TipoPaquete = (TipoPaquete)((int)g.TipoPaquete - 1), // Map TipoPaqueteEnum (1-4) to TipoPaquete (0-3)
                            CUIT = g.ClienteCUIT, // Asumo que el CUIT del cliente es lo que quieres mostrar
                            CDOrigen = g.CDOrigenID.ToString(),
                            CDDestino = g.CDDestinoID.ToString(),
                            Estado = (EstadoGuia)(int)g.Estado
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

            var serviciosAfectados = new HashSet<int>();
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

                        var hoja = HojaDeRutaAlmacen.HojasDeRuta.FirstOrDefault(h =>
                        h.Guias.Any(g => g.NumeroGuia == guia.NumeroGuia) &&
                        h.Completada == false);

                        if (hoja != null && hoja.ServicioID > 0)
                        {
                            serviciosAfectados.Add(hoja.ServicioID);
                        }
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
                    var hoja = HojaDeRutaAlmacen.HojasDeRuta.FirstOrDefault(h =>
                    h.Guias.Any(g => g.NumeroGuia == guiaEntidad.NumeroGuia) &&
                    h.Completada == false);

                    if (hoja != null && hoja.ServicioID > 0)
                    {
                        // Un servicio de descarga es el que tiene CDDestino == NuestroCD
                        // Si la guía estaba en este servicio, se marca como afectado.
                        serviciosAfectados.Add(hoja.ServicioID);
                    }
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
            foreach (var servicioID in serviciosAfectados)
            {
                MarcarHojaDeRutaComoCompletaSiEsNecesario(servicioID);
            }
            // 5. Persistencia de datos
            GuiaAlmacen.Grabar();
            HojaDeRutaAlmacen.Grabar();

            return agrupadasParaDistribucion;
        }
        public void MarcarHojaDeRutaComoCompletaSiEsNecesario(int servicioID)
        {
            var hojaDeRuta = HojaDeRutaAlmacen.HojasDeRuta
                .FirstOrDefault(h => h.ServicioID == servicioID && h.Completada == false);

            if (hojaDeRuta != null)
            {
                var servicio = ServicioAlmacen.servicios.FirstOrDefault(s => s.ServicioID == servicioID);

                EstadoEncomiendaEnum estadoFinalRequerido;

                if (servicio != null)
                {
                    if (servicio.CDOrigen == NuestroCD)
                    {
                        // Es un servicio de CARGA: la guía debe estar en EnTransito (Estado 6).
                        estadoFinalRequerido = EstadoEncomiendaEnum.EnTransito;
                    }
                    else if (servicio.CDDestino == NuestroCD)
                    {
                        // Es un servicio de DESCARGA: la guía debe estar en AdmitidoCDDestino (Estado 7).
                        estadoFinalRequerido = EstadoEncomiendaEnum.AdmitidoCDDestino;
                    }
                    else
                    {
                        return; // Si no es ni origen ni destino de nuestro CD, no debe afectar.
                    }

                    // 2. Verificar si TODAS las guías de esta HDR han alcanzado el estado final REQUERIDO.
                    bool todasCompletadas = hojaDeRuta.Guias.All(guiaHDR =>
                        GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == guiaHDR.NumeroGuia)?.Estado == estadoFinalRequerido);

                    // 3. Si todas las guías de la HDR están listas, marcamos la HDR como Completa.
                    if (todasCompletadas)
                    {
                        hojaDeRuta.Completada = true;
                        // La persistencia se realiza con HojaDeRutaAlmacen.Grabar() al final de ConfirmarOperacion.
                    }
                }
            }
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