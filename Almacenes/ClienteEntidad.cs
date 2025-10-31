using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    internal class ClienteEntidad
    {
        public string ClienteCUIT { get; set; }
        public string Direccion { get; set; }
        public string RazonSocial { get; set; }
        public int CDOrigen { get; set; }
        public List<Tarifario> Tarifas { get; set; }
    }
}
