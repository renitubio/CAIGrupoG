using CAIGrupoG.Almacenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAIGrupoG.Imposicion.ImpAgencia
{
    public class ImposicionDeEncomiendaAgenciaModelo
    {
        private ClienteEntidad ?_clienteActual;

        private static int _proximoNumeroGuia = 1;

        public Cliente BuscarCliente(string cuit)
        {
            var clienteEntidad = ClienteAlmacen.Clientes
                                     .FirstOrDefault(c => c.ClienteCUIT == cuit);

            if (clienteEntidad != null)
            {
                _clienteActual = clienteEntidad;
                return new Cliente
                {
                    CUIT = clienteEntidad.ClienteCUIT,
                    RazonSocial = clienteEntidad.RazonSocial
                };
            }

            _clienteActual = null;
            return null;
        }

        public List<Ciudad> ObtenerCiudades()
        {
            return CentroDistribucionAlmacen.CentrosDistribucion
                .Select(cd => new Ciudad
                {
                    Id = cd.CD_ID,
                    Nombre = cd.Nombre,
                    CD_ID = cd.CD_ID
                }).ToList();
        }

        public List<TipoCaja> ObtenerTiposCaja()
        {
            string[] nombresEnum = Enum.GetNames(typeof(TipoPaqueteEnum));

            List<TipoCaja> tiposCaja = new List<TipoCaja>();
            foreach (string nombre in nombresEnum)
            {
                tiposCaja.Add(new TipoCaja { Nombre = nombre });
            }
            return tiposCaja;
        }

        public List<AgenciaCD> ObtenerAgenciasPorCiudad(int ciudadId)
        {
            return AgenciaAlmacen.Agencias
                .Where(a => a.CiudadID == ciudadId)
                .Select(a => new AgenciaCD
                {
                    Id = a.AgenciaID,
                    Nombre = a.Nombre,
                    CiudadId = a.CiudadID
                }).ToList();
        }

        public List<AgenciaCD> ObtenerCDPorCiudad(int ciudadId)
        {
            return AgenciaAlmacen.Agencias
                .Where(a => a.CiudadID == ciudadId && a.AgenciaID == ciudadId)
                .Select(a => new AgenciaCD
                {
                    Id = a.AgenciaID,
                    Nombre = a.Nombre,
                    CiudadId = a.CiudadID
                }).ToList();
        }
        public List<string> ConfirmarImposicion(int cantidadTotalCajas, string codigoDestino)
        {
            var guias = new List<string>();
            for (int i = 0; i < cantidadTotalCajas; i++)
            {
                guias.Add($"{codigoDestino}-{_proximoNumeroGuia++:D6}");
            }
            return guias;
        }

    }

}