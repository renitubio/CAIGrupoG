using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Imposicion.ImpCentroDistribucion
{
    public class ImposicionDeEncomiendaCDModelo
    {
        //DATOS ESTÁTICOS FICTICIOS INTEGRADOS DIRECTAMENTE EN EL MODELO
        private static List<Ciudad> _ciudades = new List<Ciudad>
        {
            new Ciudad { Id = 1, Nombre = "CABA" , CD_ID = 1},
            new Ciudad { Id = 2, Nombre = "Rosario" , CD_ID = 2},
            new Ciudad { Id = 3, Nombre = "Córdoba" , CD_ID = 3}
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



        //Simula la generación de guías para múltiples líneas
        public List<string> ConfirmarAdmision(int cantidadEncomiendas)
        {
            var guias = new List<string>();
            var rnd = new Random();

            for (int i = 0; i < cantidadEncomiendas; i++)
            {
                // Genera un número de 9 dígitos aleatorios
                long numAleatorio = (long)(rnd.NextDouble() * 900000000L) + 100000000L;
                guias.Add($"TRK-{numAleatorio}");
            }
            return guias;
        }

        //Simula si el CUIT está "registrado"
        public bool BuscarCliente(string cuit)
        {
            // Solo el CUIT "30123456789" (de 11 dígitos) devuelve true
            return cuit == "30123456789";
        }
    }
}
