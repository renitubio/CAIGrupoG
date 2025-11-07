using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using CAIGrupoG.Almacenes;
// using CAIGrupoG.RendiciónFletero; // Deberías eliminar o corregir esta línea
// Necesitas que GuiaEntidad, TipoHDREnum y EstadoEncomiendaEnum estén definidos y accesibles.

namespace CAIGrupoG.Modelos
{
    public class RendicionFleteroModelo
    {
        // Propiedades para la información que se mostrará en la vista
        public List<GuiaEntidad> EncomiendasEntrantes { get; private set; }
        public List<GuiaEntidad> EncomiendasSalientes { get; private set; }

        // Campo para almacenar las hojas de ruta encontradas, necesarias para la rendición
        private List<HojaDeRutaEntidad> _hojasDeRutaPendientes;

        /// <summary>
        /// Busca el fletero por DNI y obtiene SOLO las guías que su estado en GuiaAlmacen NO es Entregado.
        /// </summary>
        public bool BuscarGuiasAsociadas(string dniFletero)
        {
            GuiaAlmacen.Recargar();

            const EstadoEncomiendaEnum ESTADO_EXCLUIDO = EstadoEncomiendaEnum.Entregado;

            // 1. Verificar si el fletero existe
            var fleteroExiste = FleteroAlmacen.Fleteros.Any(f => f.FleteroDNI == dniFletero); // Asumo DNI

            if (!fleteroExiste)
            {
                throw new KeyNotFoundException($"No se encontró un fletero con DNI: {dniFletero}.");
            }

            // 2. Obtener Hojas de Ruta Pendientes (Completada = false)
            _hojasDeRutaPendientes = HojaDeRutaAlmacen.HojasDeRuta
                .Where(hdr => hdr.FleteroDNI == dniFletero && !hdr.Completada)
                .ToList();
            if (!_hojasDeRutaPendientes.Any())
            {
                EncomiendasEntrantes = new List<GuiaEntidad>();
                EncomiendasSalientes = new List<GuiaEntidad>();
                return false;
            }

            // --- LÓGICA DE FILTRADO CRUZADO PARA EXCLUIR GUÍAS YA RENDIDAS ---

            // 3. Obtener un HashSet de los Números de Guía que están en estado 'Entregado' en el ALMACÉN PRINCIPAL
            var guiasRendidasNumeros = GuiaAlmacen.Guias
                .Where(g => g.Estado == ESTADO_EXCLUIDO)
                .Select(g => g.NumeroGuia)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            // 4. Compilar y clasificar las guías, EXCLUYENDO aquellas que ya están en el ESTADO_EXCLUIDO.

            // Guías Entrantes
            EncomiendasEntrantes = _hojasDeRutaPendientes
                .Where(hdr => hdr.Tipo == TipoHDREnum.ENTRANTE)
                .SelectMany(hdr => hdr.Guias)
                .Where(g => !guiasRendidasNumeros.Contains(g.NumeroGuia)) // Filtro por exclusión
                .ToList();

            // Guías Salientes
            EncomiendasSalientes = _hojasDeRutaPendientes
                .Where(hdr => hdr.Tipo == TipoHDREnum.Distribucion)
                .SelectMany(hdr => hdr.Guias)
                .Where(g => !guiasRendidasNumeros.Contains(g.NumeroGuia)) // Filtro por exclusión
                .ToList();

            return EncomiendasEntrantes.Any() || EncomiendasSalientes.Any();
        }

        // ---------------------------------------------------------------------------------------------------

        /// <summary>
        /// Realiza la rendición: actualiza el estado de las guías a Entregado y marca las HDRs como completadas.
        /// </summary>
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
    }
}