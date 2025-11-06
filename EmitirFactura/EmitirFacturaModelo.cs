using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAIGrupoG.Almacenes;
using System.IO;

namespace CAIGrupoG.EmitirFactura
{
 
    public class EmitirFacturaModelo
    {
        // El lock es crucial ya que GuiaAlmacen.Guias es un recurso estático (compartido).
        private static readonly object _guiaAlmacenLock = new object();

        public EmitirFacturaModelo()
        {
            // El constructor queda simple. La carga se hace en cada método.
        }

        // --- Lógica de Búsqueda (Diagrama de Secuencia: Buscar) ---

        /// Implementa la lógica de búsqueda de guías aptas para facturar (Entregado=13, NumFactura=0).
        public List<Guia> BuscarGuiasPorCUIT(string cuit)
        {
            lock (_guiaAlmacenLock)
            {
                // Cargar la lista fresca para reflejar cambios persistidos.
                var guiasActuales = GuiaAlmacen.Guias.ToList();

                // 1. Filtrar por CUIT, Estado=Entregado (13) y NumFactura==0
                var guiasEncontradas = guiasActuales
                    .Where(g => g.ClienteCUIT == cuit)
                    .Where(g => g.Estado == EstadoEncomiendaEnum.Entregado)
                    .Where(g => g.NumeroFactura == 0)
                    .ToList();

                if (!guiasEncontradas.Any())
                    return new List<Guia>();

                // 2. [MODELO] -> [CLIENTE ALMACEN]: Buscar la Razón Social.
                var cliente = ClienteAlmacen.Clientes.FirstOrDefault(c => c.ClienteCUIT == cuit);
                string razonSocial = cliente?.RazonSocial ?? $"CUIT {cuit} (Razón Social no encontrada)";

                // 3. Mapear al DTO Guia para la presentación
                return guiasEncontradas.Select(g => new Guia
                {
                    NumeroGuia = g.NumeroGuia,
                    // FIX: Convert EstadoEncomiendaEnum to EstadoGuia using a mapping.
                    Estado = g.Estado == EstadoEncomiendaEnum.Entregado ? EstadoGuia.Entregada
                            : g.Estado == EstadoEncomiendaEnum.Rechazado ? EstadoGuia.Devuelta
                            : g.NumeroFactura != 0 ? EstadoGuia.Facturada
                            : EstadoGuia.Entregada, // Default/fallback, adjust as needed

                    RazonSocial = razonSocial,
                    Importe = g.Importe,
                    CUIT = g.ClienteCUIT
                }).ToList();
            }
        }

        // --- Lógica de Emisión de Factura (Diagrama de Secuencia: Emitir) ---

        /// Cambia el estado de las guías, genera una nueva factura y persiste ambos cambios.
        public int EmitirFacturas(List<Guia> guiasAFacturar)
        {
            if (!guiasAFacturar.Any()) return 0;

            string cuit = guiasAFacturar.First().CUIT;
            decimal importeTotal = guiasAFacturar.Sum(g => g.Importe);
            int nuevoNumeroFactura;

            // Sincronizar TODA la operación de escritura.
            lock (_guiaAlmacenLock)
            {
                // Cargar la lista fresca otra vez para asegurar que ningún otro hilo haya modificado las guías
                var guiasActuales = GuiaAlmacen.Guias.ToList();

                // 1. Determinar el próximo número de factura.
                int ultimoNro = FacturaAlmacen.Facturas.Any()
                                ? FacturaAlmacen.Facturas.Max(f => f.NumeroFactura)
                                : 10000;
                nuevoNumeroFactura = ultimoNro + 1;

                // 2. Crear la nueva FacturaEntidad 
                var nuevaFactura = new FacturaEntidad
                {
                    NumeroFactura = nuevoNumeroFactura,
                    Monto = importeTotal,
                    FechaEmision = DateTime.Now,
                    ClienteCUIT = cuit
                };

                // 3. [MODELO] -> [FACTURA ALMACEN]: Nuevo(nuevaFactura)
                FacturaAlmacen.Nuevo(nuevaFactura);

                // 4. [MODELO] -> [FACTURA ALMACEN]: Grabar()
                FacturaAlmacen.Grabar();

                // 5. Actualizar guías en la colección CREADA (guiasActuales)
                foreach (var guiaFactura in guiasAFacturar)
                {
                    // Buscar la guía dentro de la lista CARGADA del almacén.
                    var guiaEnDB = guiasActuales.FirstOrDefault(g =>
                        g.NumeroGuia == guiaFactura.NumeroGuia &&
                        g.ClienteCUIT == guiaFactura.CUIT);

                    if (guiaEnDB != null)
                    {
                        guiaEnDB.NumeroFactura = nuevoNumeroFactura;
                    }
                }
                // 6. [MODELO] -> [GUIA ALMACEN]: Grabar()
                GuiaAlmacen.Grabar();
            }
            // 7. Devolver Numero de Factura
            return nuevoNumeroFactura;
        }
    }

}