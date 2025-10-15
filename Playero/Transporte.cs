using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Playero
{
    public class Vehiculo
    {
        public string Patente { get; set; }
        public List<Guia> GuiasAsignadas { get; set; }

        public Vehiculo()
        {
            GuiasAsignadas = new List<Guia>();
        }
    }
}
