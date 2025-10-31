using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    internal class Tarifario
    {
        public TipoPaqueteEnum TipoPaquete { get; set; }
        public int CDOrigen { get; set; }
        public int CDDestino { get; set; }
        public decimal Precio { get; set; }
    }
}
