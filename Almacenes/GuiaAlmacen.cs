using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class GuiaAlmacen
    {

        private static List<GuiaEntidad> guias = new List<GuiaEntidad>();

        public static IReadOnlyCollection<GuiaEntidad> Guias => guias.AsReadOnly();

        public static void Nuevo(GuiaEntidad guia)
        {
            if (guia.NumeroGuia== null)
                throw new ArgumentException("El ID no puede ser nulo");
            guias.Add(guia);
        }

        public static void Borrar(string NumeroGuia)
        {
            guias.RemoveAll(g => g.NumeroGuia== NumeroGuia);
        }

        static GuiaAlmacen()
        {
            if (File.Exists("guia.json"))
            {
                var guiajson = File.ReadAllText("guia.json");
                guias = System.Text.Json.JsonSerializer.Deserialize<List<GuiaEntidad>>(guiajson) ?? new List<GuiaEntidad>();
            }
        }

        public static void Grabar()
        {

            var guiajson = System.Text.Json.JsonSerializer.Serialize(guias);
            File.WriteAllText("guia.json", guiajson);
        }
    }
}
