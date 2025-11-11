using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    static class HojaDeRutaAlmacen
    {

        private static List<HojaDeRutaEntidad> hojasDeRuta = new List<HojaDeRutaEntidad>();

        public static IReadOnlyCollection<HojaDeRutaEntidad> HojasDeRuta => hojasDeRuta.AsReadOnly();

        public static void Nuevo(HojaDeRutaEntidad hojaDeRuta)
        {
            if (hojaDeRuta.HDR_ID == null)
                throw new ArgumentException("El ID no puede ser nulo");
            hojasDeRuta.Add(hojaDeRuta);
        }

        public static void Borrar(int HDR_ID)
        {
            hojasDeRuta.RemoveAll(h => h.HDR_ID == HDR_ID);
        }

        static HojaDeRutaAlmacen()
        {
            if (File.Exists(@"Datos\HojaDeRuta.json"))
            {
                var HojaDeRutajson = File.ReadAllText(@"Datos\HojaDeRuta.json");
                hojasDeRuta = System.Text.Json.JsonSerializer.Deserialize<List<HojaDeRutaEntidad>>(HojaDeRutajson) ?? new List<HojaDeRutaEntidad>();
            }
        }

        public static void Grabar()
        {

            var HojaDeRutajson = System.Text.Json.JsonSerializer.Serialize(hojasDeRuta);
            File.WriteAllText(@"Datos\HojaDeRuta.json", HojaDeRutajson);
        }
    }
}
