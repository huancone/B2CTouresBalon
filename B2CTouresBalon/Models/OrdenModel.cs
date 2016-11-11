using System;
using System.Collections.Generic;
using B2CTouresBalon.ServicioOrdenes;

namespace B2CTouresBalon.Models
{
    public class OrdenModel
    {
        public Orden[] Orden { get; set; }
    }

    public class RespuestaCancelacionModel
    {
        public string Respuesta { get; set; }
    }
}