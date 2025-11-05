using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class GuiaAlmacen
    {

        private static List<GuiaEntidad> guias = new List<GuiaEntidad>();

        public static IReadOnlyCollection<GuiaEntidad> Guias => guias.AsReadOnly();

        static GuiaAlmacen()
        {
            string ruta = Path.Combine(Directory.GetCurrentDirectory(), "Datos", "guias.json");

            if (File.Exists(ruta))
            {
                var guiajson = File.ReadAllText(ruta);
                guias = JsonSerializer.Deserialize<List<GuiaEntidad>>(guiajson) ?? new List<GuiaEntidad>();
            }
            else
            {
                guias = new List<GuiaEntidad>();
            }
        }

        public static GuiaEntidad Buscar(string numeroGuia)
        {
            return guias.FirstOrDefault(g => g.NumeroGuia.Equals(numeroGuia, StringComparison.OrdinalIgnoreCase));
        }

        public static void Nuevo(GuiaEntidad guia)
        {
            if (string.IsNullOrEmpty(guia.NumeroGuia))
                throw new ArgumentException("El número de guía no puede ser nulo.");

            guias.Add(guia);
            Grabar();
        }

        public static void Grabar()
        {
            var guiajson = JsonSerializer.Serialize(guias, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(@"Datos\Guias.json", guiajson);
        }
    }
}
