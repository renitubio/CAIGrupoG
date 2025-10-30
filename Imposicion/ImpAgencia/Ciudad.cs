using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Imposicion.ImpAgencia
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        // Se utiliza para mostrar el nombre en el ComboBox
        public int CD_ID { get; set; }
        public override string ToString() => Nombre;
    }
}
