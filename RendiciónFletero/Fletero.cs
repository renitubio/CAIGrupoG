using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.RendiciónFletero
{
    public class Fletero
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public List<Guia> GuiasAsignadas { get; set; }

        public Fletero()
        {
            GuiasAsignadas = new List<Guia>();
        }
    }
}
