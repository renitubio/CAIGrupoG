using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    internal class HojaDeRutaEntidad
    {

        public int HDR_ID { get; set; }

        public int ServicioID { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string FleteroDNI { get; set; }

        //FALTA ENUM

        public bool Completada { get; set; }

        public List<GuiaEntidad> guias { get; set; }
    }
}
