using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CAIGrupoG.Almacenes;
using CAIGrupoG.RendiciónFletero;


namespace CAIGrupoG.Modelos
{
    public class RendicionFleteroModelo
    {
        // Propiedades para la información que se mostrará en la vista
        public List<Guia> EncomiendasEntrantes { get; private set; } = new();
        public List<Guia> EncomiendasSalientes { get; private set; } = new();

        // Campo para almacenar las hojas de ruta encontradas, necesarias para la rendición
        private List<HojaDeRutaEntidad> _hojasDeRutaPendientes;

        /// <summary>
                /// Traduce el enum de TipoPaquete del Almacén al enum de la Vista.
                /// </summary>
        private TipoPaquete MapearTipoPaquete(Almacenes.TipoPaqueteEnum tipoAlmacen)
        {
            
            return tipoAlmacen switch
            {
                Almacenes.TipoPaqueteEnum.S => TipoPaquete.S,
                Almacenes.TipoPaqueteEnum.M => TipoPaquete.M,
                Almacenes.TipoPaqueteEnum.L => TipoPaquete.L,
                Almacenes.TipoPaqueteEnum.XL => TipoPaquete.XL,
                _ => TipoPaquete.S
            };
        }

        /// <summary>
        /// Traduce el enum de Estado del Almacén al enum de la Vista.
        /// </summary>
        private EstadoEncomienda MapearEstado(Almacenes.EstadoEncomiendaEnum estadoAlmacen)
        {
          
            return estadoAlmacen switch
            {
                Almacenes.EstadoEncomiendaEnum.ImpuestoCallCenter => EstadoEncomienda.ImpuestoCallCenter,
                Almacenes.EstadoEncomiendaEnum.ImpuestoAgencia => EstadoEncomienda.ImpuestoAgencia,
                Almacenes.EstadoEncomiendaEnum.EnCaminoARetirarDomicilio => EstadoEncomienda.EnCaminoARetirarDomicilio,
                Almacenes.EstadoEncomiendaEnum.EnCaminoARetirarAgencia => EstadoEncomienda.EnCaminoARetirarAgencia,
                Almacenes.EstadoEncomiendaEnum.AdmitidoCDOrigen => EstadoEncomienda.AdmitidoCDOrigen,
                Almacenes.EstadoEncomiendaEnum.EnTransito => EstadoEncomienda.EnTransito,
                Almacenes.EstadoEncomiendaEnum.AdmitidoCDDestino => EstadoEncomienda.AdmitidoCDDestino,
                Almacenes.EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio => EstadoEncomienda.DistribucionUltimaMillaDomicilio,
                Almacenes.EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia => EstadoEncomienda.DistribucionUltimaMillaAgencia,
                Almacenes.EstadoEncomiendaEnum.AgenciaDestino => EstadoEncomienda.AgenciaDestino,
                Almacenes.EstadoEncomiendaEnum.PrimerIntentoDeEntrega => EstadoEncomienda.PrimerIntentoDeEntrega,
                Almacenes.EstadoEncomiendaEnum.Rechazado => EstadoEncomienda.Rechazado,
                Almacenes.EstadoEncomiendaEnum.Entregado => EstadoEncomienda.Entregado,
                Almacenes.EstadoEncomiendaEnum.Facturada => EstadoEncomienda.Facturada,
                _ => EstadoEncomienda.PrimerIntentoDeEntrega // Un default seguro
            };
        }

        public class GuiasPorDNIResultado
        {
            public List<Guia> Admision { get; set; } = new();
            public List<Guia> Retiro { get; set; } = new();
        }

        /// Busca el fletero por DNI y obtiene SOLO las guías que su estado en GuiaAlmacen NO es Entregado.

        public GuiasPorDNIResultado BuscarGuiasPorDNI(string dniFletero)
        {
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
                EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia,
                EstadoEncomiendaEnum.AdmitidoCDOrigen
            };

            var estadosSalientes = new[]
            {
                EstadoEncomiendaEnum.ImpuestoCallCenter,
                EstadoEncomiendaEnum.ImpuestoAgencia,
                EstadoEncomiendaEnum.AdmitidoCDDestino
            };

            // 4. Buscar guías según HDR
            var guiasDeHDR = new List<GuiaEntidad>();
            foreach (var hdr in _hojasDeRutaPendientes)
            {
                foreach (var guiaHDR in hdr.Guias)
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

            // 5. Separar guías en admisión y retiro
            resultado.Admision = guiasDeHDR
        .Where(g => estadosEntrantes.Contains(g.Estado))
        .Select(g => new Guia
        {
            NumeroGuia = g.NumeroGuia,
            CUIT = g.ClienteCUIT,
            DniAutorizadoRetirar = g.DNIAutorizadoRetirar,
            Destino = g.DomicilioDestino,
            TipoPaquete = MapearTipoPaquete(g.TipoPaquete),
            Estado = MapearEstado(g.Estado)              
        })
        .ToList();

            resultado.Retiro = guiasDeHDR
              .Where(g => estadosSalientes.Contains(g.Estado))
              .Select(g => new Guia
              {
                  NumeroGuia = g.NumeroGuia,
                  CUIT = g.ClienteCUIT,
                  DniAutorizadoRetirar = g.DNIAutorizadoRetirar,
                  Destino = g.DomicilioDestino,
                  TipoPaquete = MapearTipoPaquete(g.TipoPaquete),
                  Estado = MapearEstado(g.Estado)              
              })
              .ToList();

            // *** ¡¡ARREGLO IMPORTANTE!! ***
            // Faltaba esto: guarda las listas para que 'Rendir' funcione
            this.EncomiendasEntrantes = resultado.Admision;
            this.EncomiendasSalientes = resultado.Retiro;

            return resultado;
        }

        public List<Guia> ObtenerGuiasSeleccionadas(ListView listView, List<Guia> fuenteGuias)
        {
            var seleccion = new List<Guia>();
            if (listView == null || fuenteGuias == null) return seleccion;

            var agregados = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // 1) Elementos seleccionados (SelectedItems)
            foreach (ListViewItem item in listView.SelectedItems)
            {
                if (item.Tag is GuiaEntidad guiaFromTag)
                {
                    if (!string.IsNullOrWhiteSpace(guiaFromTag.NumeroGuia) && agregados.Add(guiaFromTag.NumeroGuia.Trim()))
                        seleccion.Add(new Guia
                        {
                            NumeroGuia = guiaFromTag.NumeroGuia,
                            TipoPaquete = MapearTipoPaquete(guiaFromTag.TipoPaquete),
                            Estado = (EstadoEncomienda)guiaFromTag.Estado,
                            CUIT = guiaFromTag.ClienteCUIT,
                            DniAutorizadoRetirar = guiaFromTag.DNIAutorizadoRetirar,
                            Destino = guiaFromTag.DomicilioDestino
                        });
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
                        seleccion.Add(new Guia
                        {
                            NumeroGuia = guiaFromTag.NumeroGuia,
                            TipoPaquete = MapearTipoPaquete(guiaFromTag.TipoPaquete),
                            Estado = (EstadoEncomienda)guiaFromTag.Estado,
                            CUIT = guiaFromTag.ClienteCUIT,
                            DniAutorizadoRetirar = guiaFromTag.DNIAutorizadoRetirar,
                            Destino = guiaFromTag.DomicilioDestino
                        });
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


        /// Realiza la rendición: actualiza el estado de las guías y marca las HDRs como completadas.
        public void Rendir(List<Guia> admisionesSeleccionadas, List<Guia> retirosSeleccionados)
        {
            if (_hojasDeRutaPendientes == null || !_hojasDeRutaPendientes.Any())
            {
                throw new InvalidOperationException("No hay hojas de ruta pendientes para rendir.");
            }


            var guiasSeleccionadasNumeros = new HashSet<string>(
                admisionesSeleccionadas.Concat(retirosSeleccionados).Select(g => g.NumeroGuia),
                StringComparer.OrdinalIgnoreCase
            );


            var todasLasGuiasEnPantalla = EncomiendasEntrantes.Concat(EncomiendasSalientes)
                                            .Select(g => g.NumeroGuia);

            var guiasNoSeleccionadasNumeros = new HashSet<string>(
                todasLasGuiasEnPantalla.Where(num => !guiasSeleccionadasNumeros.Contains(num)),
                StringComparer.OrdinalIgnoreCase
            );

            foreach (var guia in GuiaAlmacen.Guias)
            {
                // Esta lógica ahora funciona para AMBAS listas
                bool fueSeleccionada = guiasSeleccionadasNumeros.Contains(guia.NumeroGuia);
                bool noFueSeleccionada = guiasNoSeleccionadasNumeros.Contains(guia.NumeroGuia);

                switch (guia.Estado)
                {
                    // ENTRANTES
                    case EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio:
                        if (fueSeleccionada)
                            guia.Estado = EstadoEncomiendaEnum.Entregado;
                        else if (noFueSeleccionada)
                            guia.Estado = EstadoEncomiendaEnum.PrimerIntentoDeEntrega;
                        break;

                    case EstadoEncomiendaEnum.PrimerIntentoDeEntrega:
                        if (noFueSeleccionada)
                            guia.Estado = EstadoEncomiendaEnum.Rechazado;
                        break;

                    case EstadoEncomiendaEnum.EnCaminoARetirarDomicilio:
                    case EstadoEncomiendaEnum.EnCaminoARetirarAgencia:
                        if (fueSeleccionada)
                            guia.Estado = EstadoEncomiendaEnum.AdmitidoCDOrigen;
                        break;

                    case EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia:
                        if (fueSeleccionada)
                            guia.Estado = EstadoEncomiendaEnum.AgenciaDestino;
                        break;

                    // SALIENTES

                    case EstadoEncomiendaEnum.ImpuestoCallCenter:
                        if (fueSeleccionada)
                            guia.Estado = EstadoEncomiendaEnum.EnCaminoARetirarDomicilio;
                        break;

                    case EstadoEncomiendaEnum.ImpuestoAgencia:
                        if (fueSeleccionada)
                            guia.Estado = EstadoEncomiendaEnum.EnCaminoARetirarAgencia;
                        break;

                    case EstadoEncomiendaEnum.AdmitidoCDDestino:
                        if (fueSeleccionada)
                        {
                            if (guia.EntregaDomicilio)
                                guia.Estado = EstadoEncomiendaEnum.DistribucionUltimaMillaDomicilio;
                            else if (guia.EntregaAgencia)
                                guia.Estado = EstadoEncomiendaEnum.DistribucionUltimaMillaAgencia;
                        }
                        break;

                }
            }


            GuiaAlmacen.Grabar();

            var estadosFinales = new[]
            {
                EstadoEncomiendaEnum.Entregado,
                EstadoEncomiendaEnum.Rechazado,
                EstadoEncomiendaEnum.AdmitidoCDOrigen,
                EstadoEncomiendaEnum.AgenciaDestino,
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

            HojaDeRutaAlmacen.Grabar();

        }
    }
}