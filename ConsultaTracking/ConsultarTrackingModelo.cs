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
        
            // Solución: Buscar en la colección Guias de GuiaAlmacen
            return CAIGrupoG.Almacenes.GuiaAlmacen.Guias.FirstOrDefault(g => g.NumeroGuia == numeroGuia);
        }

        public EstadoEncomiendaEnum? ObtenerEstadoGuia(string numeroGuia)
        {
            var guia = BuscarGuia(numeroGuia);
            return guia?.Estado;
        }
    }
}
