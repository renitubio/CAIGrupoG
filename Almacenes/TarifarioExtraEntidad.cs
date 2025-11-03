using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    internal class TarifarioExtraEntidad
    {
        public int Id { get; set; }
        public TipoExtraEnum Tipo { get; set; }
        public decimal Precio { get; set; }
    }
}
