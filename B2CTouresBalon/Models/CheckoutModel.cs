using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B2CTouresBalon.ServicioClientes;

namespace B2CTouresBalon.Models
{
    public class CheckoutModel
    {
        public Cart Cart { get; set; }
        public Cliente Cliente { get; set; }
    }
}