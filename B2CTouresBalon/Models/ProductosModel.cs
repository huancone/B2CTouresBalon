using B2CTouresBalon.ServicioProductos;

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