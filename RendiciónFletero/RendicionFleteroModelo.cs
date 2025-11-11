using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CAIGrupoG.Almacenes;
// using CAIGrupoG.RendiciónFletero; // Deberías eliminar o corregir esta línea
// Necesitas que GuiaEntidad, TipoHDREnum y EstadoEncomiendaEnum estén definidos y accesibles.

namespace CAIGrupoG.Modelos
{
    public class GuiaPresentacionDTO
    {
        public string NumeroGuia { get; set; }
        public string EstadoDescripcion { get; set; }
        public string TipoPaquete { get; set; }
        public string CUIT { get; set; }
        public string DniAutorizadoRetirar { get; set; }
        public string Destino { get; set; }
    }

    public class RendicionFleteroModelo
    {
        // Propiedades para la información que se mostrará en la vista
        public List<GuiaEntidad> EncomiendasEntrantes { get; private set; } = new();
        public List<GuiaEntidad> EncomiendasSalientes { get; private set; } = new();

        // Campo para almacenar las hojas de ruta encontradas, necesarias para la rendición
        private List<HojaDeRutaEntidad> _hojasDeRutaPendientes;


        public class GuiasPorDNIResultado
        {
            public List<GuiaEntidad> Admision { get; set; } = new();
            public List<GuiaEntidad> Retiro { get; set; } = new();
        }

        /// Busca el fletero por DNI y obtiene SOLO las guías que su estado en GuiaAlmacen NO es Entregado.

        public GuiasPorDNIResultado BuscarGuiasPorDNI(string dniFletero)
        {
            // GuiaAlmacen.Recargar(); NO ES NECESARIO

            const EstadoEncomiendaEnum ESTADO_EXCLUIDO = EstadoEncomiendaEnum.Entregado;

            // 1. Verificar si existe el fletero
            var fleteroExiste = FleteroAlmacen.Fleteros.Any(f => f.FleteroDNI == dniFletero);
            if (!fleteroExiste)
                throw new KeyNotFoundException($"No se encontró un fletero con DNI: {dniFletero}.");

            // 2. Filtrar las hojas de ruta pendientes del fletero
            _hojasDeRutaPendientes = HojaDeRutaAlmacen.HojasDeRuta
                .Where(hdr => hdr.FleteroDNI == dniFletero && !hdr.Completada)
                .ToList();

            var resultado = new GuiasPorDNIResultado();

            if (!_hojasDeRutaPendientes.Any())
                return resultado;

            // 3. Buscar las guías correspondientes en GuiaAlmacen
            var todasLasGuias = GuiaAlmacen.Guias.ToList();
            var guiasRendidasNumeros = todasLasGuias
                .Where(g => g.Estado == ESTADO_EXCLUIDO)
                .Select(g => g.NumeroGuia)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var estadosEntrantes = new[]
            {
        EstadoEncomiendaEnum.EnCaminoARetirarDomicilio,
        EstadoEncomiendaEnum.EnCaminoARetirarAgencia,
        EstadoEncomiendaEnum.PrimerIntentoDeEntrega,
        EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio,
        EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia
    };

            // 4. Buscar guías según HDR
            var guiasDeHDR = new List<GuiaEntidad>();

            foreach (var hdr in _hojasDeRutaPendientes)
            {
                foreach (var guiaHDR in hdr.Guias) // acá cada guía ya es un objeto GuiaEntidad
                {
                    if (guiaHDR == null || string.IsNullOrWhiteSpace(guiaHDR.NumeroGuia))
                        continue;

                    var guia = todasLasGuias.FirstOrDefault(g =>
                        string.Equals(g.NumeroGuia, guiaHDR.NumeroGuia, StringComparison.OrdinalIgnoreCase));

                    if (guia != null && !guiasRendidasNumeros.Contains(guia.NumeroGuia))
                    {
                        guiasDeHDR.Add(guia);
                    }
                }
            }

            // 5. Separar admisión y retiro
            resultado.Admision = guiasDeHDR
                .Where(g => estadosEntrantes.Contains(g.Estado))
                .ToList();

            resultado.Retiro = guiasDeHDR
                .Where(g => g.Estado == EstadoEncomiendaEnum.AdmitidoCDDestino)
                .ToList();

            EncomiendasEntrantes = resultado.Admision;
            EncomiendasSalientes = resultado.Retiro;

            return resultado;
        }

        public List<GuiaEntidad> ObtenerGuiasSeleccionadas(ListView listView, List<GuiaEntidad> fuenteGuias)
        {
            var seleccion = new List<GuiaEntidad>();
            if (listView == null || fuenteGuias == null) return seleccion;

            var agregados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // 1) Elementos seleccionados (SelectedItems)
            foreach (ListViewItem item in listView.SelectedItems)
            {
                if (item.Tag is GuiaEntidad guiaFromTag)
                {
                    if (!string.IsNullOrWhiteSpace(guiaFromTag.NumeroGuia) && agregados.Add(guiaFromTag.NumeroGuia.Trim()))
                        seleccion.Add(guiaFromTag);
                    continue;
                }

                var numeroGuia = item.Text?.Trim();
                if (string.IsNullOrEmpty(numeroGuia)) continue;

                var guia = fuenteGuias.FirstOrDefault(g => string.Equals(g.NumeroGuia?.Trim(), numeroGuia, StringComparison.OrdinalIgnoreCase));
                if (guia != null && agregados.Add(guia.NumeroGuia?.Trim() ?? string.Empty))
                    seleccion.Add(guia);
            }

            // 2) Elementos marcados (CheckedItems)
            foreach (ListViewItem item in listView.CheckedItems)
            {
                if (item.Tag is GuiaEntidad guiaFromTag)
                {
                    if (!string.IsNullOrWhiteSpace(guiaFromTag.NumeroGuia) && agregados.Add(guiaFromTag.NumeroGuia.Trim()))
                        seleccion.Add(guiaFromTag);
                    continue;
                }

                var numeroGuia = item.Text?.Trim();
                if (string.IsNullOrEmpty(numeroGuia)) continue;

                var guia = fuenteGuias.FirstOrDefault(g => string.Equals(g.NumeroGuia?.Trim(), numeroGuia, StringComparison.OrdinalIgnoreCase));
                if (guia != null && agregados.Add(guia.NumeroGuia?.Trim() ?? string.Empty))
                    seleccion.Add(guia);
            }

            return seleccion;
        }

        // ---------------------------------------------------------------------------------------------------


        /// Realiza la rendición: actualiza el estado de las guías a Entregado y marca las HDRs como completadas.
        public void Rendir(List<string> admisionesSeleccionadas, List<string> retirosSeleccionados)
        {
            if (_hojasDeRutaPendientes == null || !_hojasDeRutaPendientes.Any())
            {
                throw new InvalidOperationException("No hay hojas de ruta pendientes para rendir.");
            }

            // 1. ⚠️ ARREGLO: Combinamos AMBAS listas (Entrantes y Salientes) 
            //    que nos manda el Formulario.
            var guiasSeleccionadasNumeros = new HashSet<string>(
                admisionesSeleccionadas.Concat(retirosSeleccionados), // <-- USA AMBOS PARÁMETROS
                StringComparer.OrdinalIgnoreCase
            );

            // 2. ⚠️ ARREGLO: Identificamos las NO seleccionadas
            var todasLasGuiasEnPantalla = EncomiendasEntrantes.Concat(EncomiendasSalientes)
                                            .Select(g => g.NumeroGuia);

            var guiasNoSeleccionadasNumeros = new HashSet<string>(
                todasLasGuiasEnPantalla.Where(num => !guiasSeleccionadasNumeros.Contains(num)),
                StringComparer.OrdinalIgnoreCase
            );

            // 3. Aplicar reglas de negocio (Tu Switch)
            foreach (var guia in GuiaAlmacen.Guias)
            {
                // Esta lógica ahora funciona para AMBAS listas
                bool fueSeleccionada = guiasSeleccionadasNumeros.Contains(guia.NumeroGuia);
                bool noFueSeleccionada = guiasNoSeleccionadasNumeros.Contains(guia.NumeroGuia);

                // ⚠️ Pega aquí tu lógica de SWITCH completa (la que tenías antes) ⚠️
                switch (guia.Estado)
                {
                    // Caso "ENTRANTE" (Ahora 'fueSeleccionada' será TRUE)
                    case EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio:
                        if (fueSeleccionada)
                            guia.Estado = EstadoEncomiendaEnum.Entregado;
                        else if (noFueSeleccionada)
                            guia.Estado = EstadoEncomiendaEnum.PrimerIntentoDeEntrega;
                        break;
                    // Caso "ENTRANTE"
                    case EstadoEncomiendaEnum.EnCaminoARetirarDomicilio:
                    case EstadoEncomiendaEnum.EnCaminoARetirarAgencia:
                        if (fueSeleccionada)
                            guia.Estado = EstadoEncomiendaEnum.AdmitidoCDOrigen;
                        break;
                    // Caso "SALIENTE"
                    case EstadoEncomiendaEnum.AdmitidoCDDestino:
                        if (fueSeleccionada)
                        {
                            if (guia.EntregaDomicilio)
                                guia.Estado = EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio;
                            else if (guia.EntregaAgencia)
                                guia.Estado = EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia;
                        }
                        break;
                        // ... (Pega el resto de tus 'case' aquí) ...
                }
            }

            // 4. Grabar los cambios de las Guías PRIMERO
            GuiaAlmacen.Grabar();

            // 5. ⚠️ LÓGICA DE HDR CORREGIDA (para rendiciones parciales)
            var estadosFinales = new HashSet<EstadoEncomiendaEnum>
        {
            EstadoEncomiendaEnum.Entregado,
            EstadoEncomiendaEnum.Rechazado,
            EstadoEncomiendaEnum.AdmitidoCDOrigen,
            EstadoEncomiendaEnum.AgenciaDestino
        };

            foreach (var hdrPendiente in _hojasDeRutaPendientes)
            {
                var numerosDeGuiaEnHDR = hdrPendiente.Guias.Select(g => g.NumeroGuia).ToHashSet();
                var guiasEnHDR = GuiaAlmacen.Guias
                          .Where(g => numerosDeGuiaEnHDR.Contains(g.NumeroGuia))
                          .ToList();

                bool todasLasGuiasFinalizadas = guiasEnHDR.Any() &&
                                                      guiasEnHDR.All(g => estadosFinales.Contains(g.Estado));

                if (todasLasGuiasFinalizadas)
                {
                    var hdrOriginal = HojaDeRutaAlmacen.HojasDeRuta.FirstOrDefault(h => h.HDR_ID == hdrPendiente.HDR_ID);
                    if (hdrOriginal != null)
                    {
                        hdrOriginal.Completada = true;
                    }
                }
            }

            // 6. Grabar los cambios de las HDRs
            HojaDeRutaAlmacen.Grabar();

            // 7. ⚠️ NO LIMPIAR las listas internas del modelo
            // (Las líneas que limpian _hojasDeRutaPendientes, etc., fueron eliminadas)
        }

        public static string ObtenerDescripcionEstado(EstadoEncomiendaEnum estado)
        {
            return estado switch
            {
                EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia => "Distribución última milla - Agencia",
                EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio => "Distribución última milla - Domicilio",
                EstadoEncomiendaEnum.PrimerIntentoDeEntrega => "Primer Intento de Entrega",
                EstadoEncomiendaEnum.EnCaminoARetirarDomicilio => "En camino a retirar (Dom)",
                EstadoEncomiendaEnum.EnCaminoARetirarAgencia => "En camino a retirar (Age)",
                EstadoEncomiendaEnum.AdmitidoCDDestino => "En CD destino",
                EstadoEncomiendaEnum.Entregado => "Entregado",
                _ => estado.ToString()
            };
        }

        public List<GuiaPresentacionDTO> ObtenerGuiasPresentacion(List<GuiaEntidad> guias)
        {
            var lista = new List<GuiaPresentacionDTO>();
            if (guias == null) return lista;

            foreach (var g in guias)
            {
                lista.Add(new GuiaPresentacionDTO
                {
                    NumeroGuia = g.NumeroGuia,
                    EstadoDescripcion = g.Estado.ToString(),
                    TipoPaquete = g.TipoPaquete.ToString(),
                    CUIT = g.ClienteCUIT,
                    DniAutorizadoRetirar = g.DNIAutorizadoRetirar,
                    Destino = g.DomicilioDestino
                });
            }
            return lista;
        }
    }
}