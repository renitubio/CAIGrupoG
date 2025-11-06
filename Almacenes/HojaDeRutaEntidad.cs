using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    public class HojaDeRutaEntidad
    {

        public int HDR_ID { get; set; }

        public int ServicioID { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string FleteroDNI { get; set; }

        public TipoHDREnum Tipo { get; set; }

        public bool Completada { get; set; }

        //cambio aqui de List<GuiaEntidad> a List<string>, esta bien??? ivo
        public List<GuiaEntidad> Guias { get; set; }
    }
}
