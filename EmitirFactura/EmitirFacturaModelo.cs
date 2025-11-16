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
        private static readonly object _guiaAlmacenLock = new object();

        public EmitirFacturaModelo()
        {
            
        }

        /// Guías aptas para facturar (Estado Entregado=13, NumFactura=0).
        public List<Guia> BuscarGuiasPorCUIT(string cuit)
        {
            lock (_guiaAlmacenLock)
            {
                var guiasActuales = GuiaAlmacen.Guias.ToList();
                var egresosActuales = EgresosAlmacen.Egresos.ToList();
                var tarifariosExtra = TarifarioExtraAlmacen.TarifariosExtra.ToList();

                var guiasEncontradas = guiasActuales
                    .Where(g => g.ClienteCUIT == cuit)
                    .Where(g => g.NumeroFactura ==0)
                    .Where(g => g.Estado == EstadoEncomiendaEnum.Entregado || g.Estado == EstadoEncomiendaEnum.Rechazado)
                    .ToList();

                if (!guiasEncontradas.Any())
                    return new List<Guia>();

                var cliente = ClienteAlmacen.Clientes.FirstOrDefault(c => c.ClienteCUIT == cuit);
                string razonSocial = cliente?.RazonSocial ?? $"CUIT {cuit} (Razón Social no encontrada)";

                var resultado = new List<Guia>();
                foreach (var g in guiasEncontradas)
                {
                    decimal importe = g.Importe;
                    bool persistirGuia = false;
                    // Si está Rechazada, duplicar importe y egresos
                    if (g.Estado == EstadoEncomiendaEnum.Rechazado)
                    {
                        importe *=2;
                        g.Importe = importe; // Persistir en almacén
                        persistirGuia = true;
                        var egresosGuia = egresosActuales.Where(e => e.NumeroGuia == g.NumeroGuia).ToList();
                        foreach (var egreso in egresosGuia)
                        {
                            egreso.MontoPago *=2;
                        }
                    }
                    // Si está Entregada y tuvo PrimerIntentoDeEntrega
                    if (g.Estado == EstadoEncomiendaEnum.Entregado)
                    {
                        var extraEntregaDomicilio = tarifariosExtra.FirstOrDefault(t => t.Tipo == TipoExtraEnum.EntregaDomicilio);
                        if (extraEntregaDomicilio != null)
                        {
                            importe += extraEntregaDomicilio.Precio *2;
                            g.Importe = importe; // Persistir en almacén
                            persistirGuia = true;
                        }
                        var egresosFlete = egresosActuales
                            .Where(e => e.NumeroGuia == g.NumeroGuia && e.TipoEgreso == TipoEgresoEnum.ComisionFlete)
                            .ToList();
                        foreach (var egreso in egresosFlete)
                        {
                            egreso.MontoPago *=2;
                        }
                    }
                    resultado.Add(new Guia
                    {
                        NumeroGuia = g.NumeroGuia,
                        Estado = g.Estado == EstadoEncomiendaEnum.Entregado ? EstadoGuia.Entregado : EstadoGuia.Rechazado,
                        RazonSocial = razonSocial,
                        Importe = importe,
                        CUIT = g.ClienteCUIT
                    });
                }
                // Persistir cambios en almacenes
                GuiaAlmacen.Grabar();
                EgresosAlmacen.Grabar();
                return resultado;
            }
        }


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
                        guiaEnDB.Estado = EstadoEncomiendaEnum.Facturada;
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