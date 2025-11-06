using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    internal class FleteroEntidad
    {
        public string FleteroDNI { get; set; }

        public string Nombre { get; set; }

        public int CD_ID { get; set; }

        public Dictionary<string, decimal> comisiones { get; set; }
    }
}
