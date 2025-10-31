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
        int CDOrigen { get; set; }
        int CDDestino { get; set; }
        decimal Precio { get; set; }
    }
}
