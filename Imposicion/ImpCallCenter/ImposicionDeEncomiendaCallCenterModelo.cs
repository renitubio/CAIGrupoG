using CAIGrupoG.Imposicion.ImpCentroDistribucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Imposicion.ImpCallCenter
{
    public class ImposicionDeEncomiendaCallCenterModelo
    {
        // DATOS ESTÁTICOS FICTICIOS
        private static List<Cliente> _clientes = new List<Cliente>
        {
            new Cliente { CUIT = "30123456789", RazonSocial = "Empresa Ejemplo SA" }
        };

        private static List<Ciudad> _ciudades = new List<Ciudad>
        {
            new Ciudad { Id = 1, Nombre = "CABA", CD_ID = 1 },
            new Ciudad { Id = 2, Nombre = "Rosario", CD_ID = 2 },
            new Ciudad { Id = 3, Nombre = "Córdoba", CD_ID = 3 }
        };

        private static List<AgenciaCD> _agenciasCD = new List<AgenciaCD>
        {
            new AgenciaCD { Id = 1, Nombre = "CD Terminal Dellepiane", CiudadId = 1 },
            new AgenciaCD { Id = 101, Nombre = "Agencia Microcentro", CiudadId = 1 },
            new AgenciaCD { Id = 102, Nombre = "Agencia Palermo", CiudadId = 1 },
            new AgenciaCD { Id = 2, Nombre = "CD Terminal Rosario", CiudadId = 2 },
            new AgenciaCD { Id = 301, Nombre = "CD Terminal Córdoba", CiudadId = 3 },
            new AgenciaCD { Id = 3, Nombre = "Agencia Cerro", CiudadId = 3 }
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

        // agregar un obtener CD por ciudad id
        public List<AgenciaCD> ObtenerCDPorCiudad(int ciudadId)
        {
            //retornar solo los cd con el mismo id que la ciudad
            return _agenciasCD.Where(a => a.CiudadId == ciudadId && a.Id == ciudadId).ToList();
        }


        /// Simula la generación de guías, una por cada caja.

        public List<string> ConfirmarImposicion(int cantidadTotalCajas, string codigoDestino)
        {
            var guias = new List<string>();
            for (int i = 0; i < cantidadTotalCajas; i++)
            {
                guias.Add($"{codigoDestino}-{_proximoNumeroGuia++:D6}");
            }
            return guias;
        }

        /// Simula la búsqueda de un cliente y devuelve su objeto si lo encuentra.
        public Cliente BuscarCliente(string cuit)
        {
            return _clientes.FirstOrDefault(c => c.CUIT == cuit);
        }
    }
}
