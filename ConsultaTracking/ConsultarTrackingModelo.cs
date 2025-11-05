using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.ConsultaTracking
{
    public class ConsultarTrackingModelo
    {


        public ConsultarTrackingModelo()
        {

        }

        public GuiaEntidad BuscarGuia(string numeroGuia)
        {
            return GuiaAlmacen.Buscar(numeroGuia);
        }

        public EstadoEncomiendaEnum? ObtenerEstadoGuia(string numeroGuia)
        {
            var guia = BuscarGuia(numeroGuia);
            return guia?.Estado;
        }
    }
}
