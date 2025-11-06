using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    internal class AgenciaEntidad
    {
        public int AgenciaID { get; set; }
        public string Nombre { get; set; }
        public int CiudadID { get; set; }
        // debe referenciar a la entidad CiudadEntidad

        public Dictionary<string, decimal> Comisiones { get; set; }

    }
}
