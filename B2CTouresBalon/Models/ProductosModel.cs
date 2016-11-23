using B2CTouresBalon.ProxyServiceB2C;

namespace B2CTouresBalon.Models
{
    public class ProductosModel
    {
        public string SearchString { get; set; }
        public int Page { get; set; }

        public Producto[] Productos { get; set; }

        public Producto[] ProductosRelacionados { get; set; }
    }
}