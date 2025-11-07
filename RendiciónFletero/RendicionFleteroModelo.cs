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
        public List<GuiaEntidad> EncomiendasEntrantes { get; private set; }
        public List<GuiaEntidad> EncomiendasSalientes { get; private set; }

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
            GuiaAlmacen.Recargar();

            const EstadoEncomiendaEnum ESTADO_EXCLUIDO = EstadoEncomiendaEnum.Entregado;

            // 1. Verificar si el fletero existe
            var fleteroExiste = FleteroAlmacen.Fleteros.Any(f => f.FleteroDNI == dniFletero); // Asumo DNI

            if (!fleteroExiste)
                throw new KeyNotFoundException($"No se encontró un fletero con DNI: {dniFletero}.");

            // 2. Obtener Hojas de Ruta Pendientes (Completada = false)
            _hojasDeRutaPendientes = HojaDeRutaAlmacen.HojasDeRuta
                .Where(hdr => hdr.FleteroDNI == dniFletero && !hdr.Completada)
                .ToList();

            var resultado = new GuiasPorDNIResultado();

            if (!_hojasDeRutaPendientes.Any())
            {
                EncomiendasEntrantes = new List<GuiaEntidad>();
                EncomiendasSalientes = new List<GuiaEntidad>();
                return resultado;
            }

            // 3. Obtener un HashSet de los Números de Guía que están en estado 'Entregado' en el ALMACÉN PRINCIPAL
            var guiasRendidasNumeros = GuiaAlmacen.Guias
                .Where(g => g.Estado == ESTADO_EXCLUIDO)
                .Select(g => g.NumeroGuia)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            // 4. Compilar y clasificar las guías, EXCLUYENDO aquellas que ya están en el ESTADO_EXCLUIDO.

            // Guías Entrantes (Admisión)
            resultado.Admision = _hojasDeRutaPendientes
                .Where(hdr => hdr.Tipo == TipoHDREnum.ENTRANTE)
                .SelectMany(hdr => hdr.Guias)
                .Where(g => !guiasRendidasNumeros.Contains(g.NumeroGuia)) // Filtro por exclusión
                .ToList();

            // Guías Salientes (Retiro)
            resultado.Retiro = _hojasDeRutaPendientes
                .Where(hdr => hdr.Tipo == TipoHDREnum.Distribucion)
                .SelectMany(hdr => hdr.Guias)
                .Where(g => !guiasRendidasNumeros.Contains(g.NumeroGuia)) // Filtro por exclusión
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
        public void Rendir()
        {
            if (_hojasDeRutaPendientes == null || !_hojasDeRutaPendientes.Any())
            {
                throw new InvalidOperationException("No hay hojas de ruta pendientes para rendir.");
            }

            // 1. Actualizar el estado de las guías asociadas a Entregado
            const EstadoEncomiendaEnum ESTADO_FINAL = EstadoEncomiendaEnum.Entregado;

            var guiasARendir = EncomiendasEntrantes.Concat(EncomiendasSalientes).ToList();
            // Obtener todos los números de guía a rendir
            var guiasNumerosARendir = EncomiendasEntrantes
                .Concat(EncomiendasSalientes)
                .Select(g => g.NumeroGuia)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            // ⚠️ EL AJUSTE CLAVE ESTÁ AQUÍ: Iteramos sobre GuiaAlmacen.Guias para modificar la referencia que se va a grabar.
            // Esto es crucial porque IReadOnlyCollection<T> expone la misma lista que GuiaAlmacen serializa.
            foreach (var guiaOriginal in GuiaAlmacen.Guias)
            {
                if (guiasNumerosARendir.Contains(guiaOriginal.NumeroGuia))
                {
                    // Modificamos el estado en el objeto de referencia que está en el almacén.
                    guiaOriginal.Estado = ESTADO_FINAL;
                }
            }
            // ---------------------------------------------------------------------------------------------------

            // 2. Actualizar las Hojas de Ruta como completadas
            foreach (var hdrPendiente in _hojasDeRutaPendientes)
            {
                var hdrOriginal = HojaDeRutaAlmacen.HojasDeRuta.FirstOrDefault(h => h.HDR_ID == hdrPendiente.HDR_ID);
                if (hdrOriginal != null)
                {
                    hdrOriginal.Completada = true;
                }
            }

            // 3. Grabar todos los cambios
            GuiaAlmacen.Grabar();       // Graba las guías con el nuevo estado (ESTE DEBE SER EL ARCHIVO CONSULTADO)
            HojaDeRutaAlmacen.Grabar(); // Graba las HDRs como completadas

            // Limpieza interna del modelo después de la rendición
            EncomiendasEntrantes = new List<GuiaEntidad>();
            EncomiendasSalientes = new List<GuiaEntidad>();
            _hojasDeRutaPendientes = new List<HojaDeRutaEntidad>();
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