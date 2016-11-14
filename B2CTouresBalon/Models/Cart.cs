using System;
using System.Collections.Generic;
using B2CTouresBalon.ServicioProductos;

namespace B2CTouresBalon.Models
{
    [Serializable]
    public class Cart
    {
        public int UserId { get; set; }
        public List<Item> Items { get; set; }
    }

    [Serializable]
    public class Item
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
    }
}