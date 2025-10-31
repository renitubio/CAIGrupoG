﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIGrupoG.Almacenes
{
    internal class GuiaEntidad
    {
        public string NumeroGuia { get; set; }

        //FALTAN LOS 2 ENUM
        public int CDDestinoID { get; set; }
        
        public string ClienteCUIT { get; set; }

        public int CDOrigenID { get; set; }

        public decimal Importe { get; set; }

        public string DNIAutorizadoRetirar { get; set; }

        public string DomicilioDestino { get; set; }

        public bool EntregaDomicilio { get; set; }

        public DateTime FechaAdmision { get; set; }

        public bool RetiroDomicilio { get; set; }

        public bool EntregaGuíaAgencia { get; set; }

        public int NumeroFactura { get; set; }

        public int AgenciaDestinoID { get; set; }

    }
}
