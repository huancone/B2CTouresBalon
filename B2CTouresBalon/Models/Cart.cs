using System;
using System.Collections.Generic;
using B2CTouresBalon.ProxyServiceB2C;

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