using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAIGrupoG.EntregaGuíaAgencia
{
    public class EntregaGuiaAgenciaModelo
    {
        public EntregaGuiaAgenciaModelo() { }

        public List<Guia> BuscarGuiasPorDNI(string dni)
        {
            if (AgenciaAlmacen.AgenciaActual == null)
                throw new InvalidOperationException("No se ha seleccionado una Agencia en el Menú Principal.");

            int agenciaActualID = AgenciaAlmacen.AgenciaActual.AgenciaID;
            EstadoEncomiendaEnum estadoRequerido = EstadoEncomiendaEnum.AgenciaDestino;

            var guiasEntidad = GuiaAlmacen.Guias
                .Where(g =>
                    g.Estado == estadoRequerido &&
                    g.AgenciaDestinoID == agenciaActualID &&
                    g.DNIAutorizadoRetirar == dni
                )
                .ToList();

            var guiasViewModel = guiasEntidad.Select(g => new Guia
            {
                NumeroGuia = g.NumeroGuia,
                TipoPaquete = (TipoPaquete)((int)g.TipoPaquete - 1), // Conversión correcta de TipoPaqueteEnum a TipoPaquete
                Estado = (EstadoGuia)g.Estado,
                TipoPaqueteTexto = ((TipoPaquete)g.TipoPaquete).ToString() // <-- Esto muestra S, M, L, XL
            }).ToList();

            return guiasViewModel;
        }

        public void ConfirmarRetiro(List<Guia> guiasARetirar)
        {
            EstadoEncomiendaEnum nuevoEstado = EstadoEncomiendaEnum.Entregado;

            foreach (var guiaVM in guiasARetirar)
            {
                var guiaEntidad = GuiaAlmacen.Guias
                    .FirstOrDefault(g => g.NumeroGuia == guiaVM.NumeroGuia);

                if (guiaEntidad != null)
                {
                    guiaEntidad.Estado = nuevoEstado;
                    CrearEgresoPorGuia(guiaEntidad);
                }
            }

            GuiaAlmacen.Grabar();
            CAIGrupoG.Almacenes.EgresosAlmacen.Grabar();
        }

        private void CrearEgresoPorGuia(CAIGrupoG.Almacenes.GuiaEntidad guia)
        {
            var agencia = CAIGrupoG.Almacenes.AgenciaAlmacen.AgenciaActual;
            if (agencia == null) return;

            decimal monto = 0;
            if (agencia.Comisiones != null && agencia.Comisiones.TryGetValue(guia.TipoPaquete, out var comision))
                monto = comision;

            var egreso = new CAIGrupoG.Almacenes.EgresosEntidad
            {
                MontoPago = monto,
                NumeroGuia = guia.NumeroGuia,
                FechaPago = default,
                NumeroFactura = 0,
                TipoEgreso = CAIGrupoG.Almacenes.TipoEgresoEnum.ComisionAgencia,
                AgenciaID = agencia.AgenciaID,
                FleteroDNI = string.Empty,
                CUITEmpresaTransporte = string.Empty
            };
            CAIGrupoG.Almacenes.EgresosAlmacen.Nuevo(egreso);
        }
    }
}

