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

        public static void Nuevo(CentroDistribucionEntidad centroDistribucion)
        {
            if (centroDistribucion.CD_ID == null)
                throw new ArgumentException("El CUIT no puede ser nulo");
            centrosDistribucion.Add(centroDistribucion);
        }

        public static void Borrar(int CD_ID)
        {
            centrosDistribucion.RemoveAll(c => c.CD_ID == CD_ID);
        }

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
