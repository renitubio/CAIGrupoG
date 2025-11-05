using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class CentroDistribucionAlmacen
    {

        private static List<CentroDistribucionEntidad> centrosDistribucion = new List<CentroDistribucionEntidad>();

        public static IReadOnlyCollection<CentroDistribucionEntidad> CentrosDistribucion => centrosDistribucion.AsReadOnly();


        static CentroDistribucionAlmacen()
        {
            if (File.Exists(@"Datos\CentrosDeDistribucion.json"))
            {
                var centroDistribucionJson = File.ReadAllText(@"Datos\CentrosDeDistribucion.json");
                centrosDistribucion = System.Text.Json.JsonSerializer.Deserialize<List<CentroDistribucionEntidad>>(centroDistribucionJson) ?? new List<CentroDistribucionEntidad>();
            }
        }

        public static void Grabar()
        {

            var centroDistribucionJson = System.Text.Json.JsonSerializer.Serialize(centrosDistribucion);
            File.WriteAllText(@"Datos\CentrosDeDistribucion.json", centroDistribucionJson);
        }
    }
}
