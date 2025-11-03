using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class AgenciaAlmacen
    {

        private static List<AgenciaEntidad> agencias = new List<AgenciaEntidad>();

        public static IReadOnlyCollection<AgenciaEntidad> Agencias => agencias.AsReadOnly();

        //en verdad es useless porque no creamos agencias nuevas.
        public static void Nuevo(AgenciaEntidad agencia)
        {
            if (agencia.AgenciaID == null)
                throw new ArgumentException("El CUIT no puede ser nulo");
            agencias.Add(agencia);
        }

        public static void Borrar(int AgenciaID)
        {
            agencias.RemoveAll(a => a.AgenciaID == AgenciaID);
        }

        static AgenciaAlmacen()
        {
            if (File.Exists(@"Datos\Agencias.json"))
            {
                var agenciaJson = File.ReadAllText(@"Datos\Agencias.json");
                agencias = System.Text.Json.JsonSerializer.Deserialize<List<AgenciaEntidad>>(agenciaJson) ?? new List<AgenciaEntidad>();
            }
        }

        public static void Grabar()
        {

            var agenciaJson = System.Text.Json.JsonSerializer.Serialize(agencias);
            File.WriteAllText(@"Datos\Agencias.json", agenciaJson);
        }
    }
}
