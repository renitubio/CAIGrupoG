using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
                NuestroCD = 0; // No lanzar excepción
            }
            PruebaDebug(); // <-- Esto fuerza la ejecución del debug al crear el modelo
        }

        // Si se mantiene el constructor con parámetro, se usa este:
        public PlayeroModelo(int cdSeleccionado)
        {
            NuestroCD = cdSeleccionado;
        }

            /// Busca el nombre de un CD a partir de su ID.
            private string ObtenerNombreCD(int cdId)
        {
                // Asumo que tu almacén se llama 'CentroDistribucionAlmacen' 
                // y la lista 'CentrosDistribucion'
                var cd = CentroDistribucionAlmacen.CentrosDistribucion
           .FirstOrDefault(c => c.CD_ID == cdId);

            return cd != null ? cd.Nombre : cdId.ToString(); // Devuelve el nombre o el ID si no lo encuentra
            }

        public (List<Guia> Cargas, List<Guia> Descargas) BuscarGuiasPorPatente(string patente)
        {
            if (NuestroCD == 0)
 return (new List<Guia>(), new List<Guia>()); // No hay CD seleccionado, devolver vacío

  var patenteUpper = patente.ToUpper();
    var cargas = new List<Guia>();
    var descargas = new List<Guia>();

    // 1. Buscar TODOS los servicios de CARGA (Salida desde NuestroCD)
    var serviciosCarga = ServicioAlmacen.Servicios
        .Where(s => s.Patente.ToUpper() == patenteUpper && s.CDOrigen == NuestroCD)
        .ToList();

    // 2. Buscar TODOS los servicios de DESCARGA (Llegada a NuestroCD)
    var serviciosDescarga = ServicioAlmacen.Servicios
  .Where(s => s.Patente.ToUpper() == patenteUpper && s.CDDestino == NuestroCD)
        .ToList();

    // 3. Obtener Guías de Carga - Buscar en las HDRs del servicio
    foreach (var servicioCarga in serviciosCarga)
    {
        // Buscar TODAS las HDRs asociadas a este servicio de carga
   var hojasCarga = HojaDeRutaAlmacen.HojasDeRuta
  .Where(h => h.ServicioID == servicioCarga.ServicioID && h.Completada == false)
 .ToList();

        foreach (var hojaCarga in hojasCarga)
      {
       if (hojaCarga.Guias != null)
            {
     var numerosGuiaCarga = hojaCarga.Guias.Select(g => g.NumeroGuia);

         // Buscar guías que estén en la HDR y en estado AdmitidoCDOrigen
     var guiasCarga = GuiaAlmacen.Guias
      .Where(g => numerosGuiaCarga.Contains(g.NumeroGuia) &&
g.Estado == EstadoEncomiendaEnum.AdmitidoCDOrigen)
           .Select(g => new Guia
          {
        NumeroGuia = g.NumeroGuia,
  TipoPaquete = (TipoPaquete)((int)g.TipoPaquete - 1),
              CUIT = g.ClienteCUIT,
       CDOrigen = ObtenerNombreCD(g.CDOrigenID),
        CDDestino = ObtenerNombreCD(servicioCarga.CDDestino), // Mostrar destino del servicio, no destino final
             Estado = (EstadoGuia)(int)g.Estado,
         })
   .ToList();

         cargas.AddRange(guiasCarga);
    }
        }
    }

    // 4. Obtener Guías de Descarga - TODAS las HDRs asociadas a TODOS los servicios
    foreach (var servicioDescarga in serviciosDescarga)
    {
        // Buscar TODAS las HDRs asociadas a este servicio (no solo la primera)
 var hojasDescarga = HojaDeRutaAlmacen.HojasDeRuta
            .Where(h => h.ServicioID == servicioDescarga.ServicioID && h.Completada == false)
         .ToList();

     foreach (var hojaDescarga in hojasDescarga)
      {
      if (hojaDescarga.Guias != null)
    {
          var numerosGuiaDescarga = hojaDescarga.Guias.Select(g => g.NumeroGuia);

                // Solo guías en estado EnTransito
        var guiasDescarga = GuiaAlmacen.Guias
               .Where(g => numerosGuiaDescarga.Contains(g.NumeroGuia) &&
   g.Estado == EstadoEncomiendaEnum.EnTransito)
    .Select(g => new Guia
       {
         NumeroGuia = g.NumeroGuia,
           TipoPaquete = (TipoPaquete)((int)g.TipoPaquete -1),
   CUIT = g.ClienteCUIT,
       CDOrigen = ObtenerNombreCD(servicioDescarga.CDOrigen), // Mostrar origen del servicio
          CDDestino = ObtenerNombreCD(servicioDescarga.CDDestino), // Mostrar destino del servicio
       Estado = (EstadoGuia)(int)g.Estado
  })
  .ToList();

         descargas.AddRange(guiasDescarga);
    }
        }
    }

    return (cargas, descargas);
}

        // Método principal para confirmar y procesar la operación
        public Dictionary<string, List<GuiaEntidad>> ConfirmarOperacion(List<Guia> cargasSeleccionadas, List<Guia> descargasSeleccionadas)
        {
            if (NuestroCD == 0)
                return new Dictionary<string, List<GuiaEntidad>>(); // No hay CD seleccionado, no procesar

    if ((cargasSeleccionadas == null || !cargasSeleccionadas.Any()) && (descargasSeleccionadas == null || !descargasSeleccionadas.Any()))
    {
        throw new ArgumentException("Debe seleccionar al menos una guía para Cargar o Descargar.");
    }

    var serviciosAfectados = new HashSet<int>();
    var agrupadasParaDistribucion = new Dictionary<string, List<GuiaEntidad>>();

    // 1. Actualización de Estados para Cargas (Estado 5 -> 6: EnTransito)
    if (cargasSeleccionadas != null && cargasSeleccionadas.Any())
    {
        var cargasSeleccionadasEntidad = cargasSeleccionadas
            .Select(g => GuiaAlmacen.Guias.FirstOrDefault(ge => ge.NumeroGuia == g.NumeroGuia))
            .Where(ge => ge != null)
            .ToList();

        // NO crear una nueva HDR, la guía ya está en una HDR existente
        // Solo cambiar el estado de la guía a EnTransito

 foreach (var guiaEntidad in cargasSeleccionadasEntidad)
    {
        guiaEntidad.Estado = EstadoEncomiendaEnum.EnTransito;
GuiaAlmacen.Actualizar(guiaEntidad);

     // Buscar la HDR que contiene esta guía para marcarla como afectada
var hdr = HojaDeRutaAlmacen.HojasDeRuta.FirstOrDefault(h =>
 h.Guias.Any(g => g.NumeroGuia == guiaEntidad.NumeroGuia) &&
         h.Completada == false &&
     h.ServicioID > 0);

     if (hdr != null)
    {
       serviciosAfectados.Add(hdr.ServicioID);
        }
    }
}
    // 2. Actualización de Estados para Descargas (Estado 6 -> 7: AdmitidoCDDestino)
    if (descargasSeleccionadas != null && descargasSeleccionadas.Any())
    {
        var descargasSeleccionadasEntidad = descargasSeleccionadas
            .Select(g => GuiaAlmacen.Guias.FirstOrDefault(ge => ge.NumeroGuia == g.NumeroGuia))
     .Where(ge => ge != null)
    .ToList();

    foreach (var guiaEntidad in descargasSeleccionadasEntidad)
    {
        // Primero cambiar a AdmitidoCDDestino
      guiaEntidad.Estado = EstadoEncomiendaEnum.AdmitidoCDDestino;
        GuiaAlmacen.Actualizar(guiaEntidad);

        var hoja = HojaDeRutaAlmacen.HojasDeRuta.FirstOrDefault(h =>
  h.Guias.Any(g => g.NumeroGuia == guiaEntidad.NumeroGuia) &&
        h.Completada == false &&
     h.ServicioID > 0);

     if (hoja != null)
        {
          serviciosAfectados.Add(hoja.ServicioID);

      var servicioActual = ServicioAlmacen.Servicios.FirstOrDefault(s => s.ServicioID == hoja.ServicioID);
            if (servicioActual != null)
        {
          // Verificar si el CD actual es el destino final de la guía
                bool esDestinoFinal = NuestroCD == guiaEntidad.CDDestinoID;

  if (!esDestinoFinal)
       {
            // No es destino final, buscar siguiente tramo usando las HDRs existentes
   // Buscar otra HDR que tenga esta guía y cuyo servicio salga del CD actual
     var siguienteHDR = HojaDeRutaAlmacen.HojasDeRuta
   .Where(h => h.Guias.Any(g => g.NumeroGuia == guiaEntidad.NumeroGuia) &&
    h.ServicioID != hoja.ServicioID &&
          h.Completada == false)
                 .Select(h => new { HDR = h, Servicio = ServicioAlmacen.Servicios.FirstOrDefault(s => s.ServicioID == h.ServicioID) })
        .FirstOrDefault(x => x.Servicio != null && x.Servicio.CDOrigen == NuestroCD);

 if (siguienteHDR != null)
     {
      // Hay un siguiente tramo, preparar la guía para carga
      guiaEntidad.CDOrigenID = siguienteHDR.Servicio.CDOrigen;
       guiaEntidad.Estado = EstadoEncomiendaEnum.AdmitidoCDOrigen;
GuiaAlmacen.Actualizar(guiaEntidad);
    }
                }
          // Si es destino final, la guía queda en estado AdmitidoCDDestino (estado 7)
          }
        }
    }

// Agrupación para distribución (igual que antes)
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

    if (agrupadasParaDistribucion.Any())
    {
        CrearHojasDeRutaDistribucion(agrupadasParaDistribucion);
    }
}

    foreach (var servicioID in serviciosAfectados)
    {
        MarcarHojaDeRutaComoCompletaSiEsNecesario(servicioID);
    }

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
                var servicio = ServicioAlmacen.Servicios.FirstOrDefault(s => s.ServicioID == servicioID);

                EstadoEncomiendaEnum estadoFinalRequerido;

                if (servicio != null)
                {
                      // ⚠️ ARREGLO: Solo nos importa si es un servicio de DESCARGA
                      if (servicio.CDDestino == NuestroCD)
                    {
                        // Es un servicio de DESCARGA: la guía debe estar en AdmitidoCDDestino (Estado 7).
                        estadoFinalRequerido = EstadoEncomiendaEnum.AdmitidoCDDestino;

                        // 2. Verificar si TODAS las guías de esta HDR han alcanzado el estado final REQUERIDO.
                        bool todasCompletadas = hojaDeRuta.Guias.All(guiaHDR =>
                        GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == guiaHDR.NumeroGuia)?.Estado == estadoFinalRequerido);

                        // 3. Si todas las guías de la HDR están listas, marcamos la HDR como Completa.
                        if (todasCompletadas)
                        {
                            hojaDeRuta.Completada = true;
                        }
                    }
                      // Si es un servicio de CARGA (CDOrigen == NuestroCD), no hacemos nada.
                      // La HDR debe permanecer "Completada": false hasta que llegue a destino.
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

        public void PruebaDebug()
        {
            var guia = GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == "GUI017");
            Debug.WriteLine("=== DEBUG DE GUIA ===");
            Debug.WriteLine($"Guía: {guia.NumeroGuia}, Estado: {guia.Estado}, CDDestinoID: {guia.CDDestinoID}, DNI: {guia.DNIAutorizadoRetirar}");
        }


    }
}