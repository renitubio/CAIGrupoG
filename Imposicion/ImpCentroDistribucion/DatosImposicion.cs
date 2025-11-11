using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Imposicion.ImpCentroDistribucion
{
    public class DatosImposicion
    {
        public Dictionary<string, int> Items { get; set; }
        public string DNIAutorizadoRetirar { get; set; }
        public bool EntregaDomicilio { get; set; }
        public string DomicilioDestino { get; set; }
        public int AgenciaDestinoID { get; set; }
        public int CDDestinoID { get; set; }
    }
}
