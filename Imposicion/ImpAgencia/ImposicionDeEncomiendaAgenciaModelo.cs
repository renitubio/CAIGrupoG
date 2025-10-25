using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Imposicion.ImpAgencia
{
    public class ImposicionDeEncomiendaAgenciaModelo
    {
        //DATOS ESTÁTICOS FICTICIOS INTEGRADOS DIRECTAMENTE EN EL MODELO
        private static List<Cliente> _clientes = new List<Cliente>
        {
            new Cliente { CUIT = "30123456789", RazonSocial = "Empresa Ejemplo SA" },
            new Cliente { CUIT = "20987654321", RazonSocial = "Consultora Ficticia SRL" }
        };

        private static List<Ciudad> _ciudades = new List<Ciudad>
        {
            new Ciudad { Id = 1, Nombre = "CABA" },
            new Ciudad { Id = 2, Nombre = "Rosario" },
            new Ciudad { Id = 3, Nombre = "Córdoba" }
        };

        private static List<AgenciaCD> _agenciasCD = new List<AgenciaCD>
        {
            new AgenciaCD { Id = 101, Nombre = "CD Terminal Dellepiane", CiudadId = 1 },
            new AgenciaCD { Id = 102, Nombre = "Agencia Microcentro", CiudadId = 1 },
            new AgenciaCD { Id = 201, Nombre = "CD Terminal Rosario", CiudadId = 2 },
            new AgenciaCD { Id = 301, Nombre = "CD Terminal Córdoba", CiudadId = 3 },
            new AgenciaCD { Id = 302, Nombre = "Agencia Cerro", CiudadId = 3 }
        };

        private static List<TipoCaja> _tiposCaja = new List<TipoCaja>
        {
            new TipoCaja { Nombre = "S" },
            new TipoCaja { Nombre = "M" },
            new TipoCaja { Nombre = "L" },
            new TipoCaja { Nombre = "XL" }
        };

        private static int _proximoNumeroGuia = 1;

        // Métodos de acceso a datos
        public List<Ciudad> ObtenerCiudades() => _ciudades;
        public List<TipoCaja> ObtenerTiposCaja() => _tiposCaja;
        public List<AgenciaCD> ObtenerAgenciasPorCiudad(int ciudadId) => _agenciasCD.Where(a => a.CiudadId == ciudadId).ToList();

        /// <summary>
        /// Simula la generación de guías, una por cada caja.
        /// </summary>
        public List<string> ConfirmarImposicion(int cantidadTotalCajas, string codigoDestino)
        {
            var guias = new List<string>();
            for (int i = 0; i < cantidadTotalCajas; i++)
            {
                guias.Add($"{codigoDestino}-{_proximoNumeroGuia++:D6}");
            }
            return guias;
        }

        /// <summary>
        /// Simula la búsqueda de un cliente y devuelve el objeto si lo encuentra.
        /// </summary>
        public Cliente BuscarCliente(string cuit)
        {
            return _clientes.FirstOrDefault(c => c.CUIT == cuit);
        }
    }
}
