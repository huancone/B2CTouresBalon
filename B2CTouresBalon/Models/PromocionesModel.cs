using B2CTouresBalon.ServicioProductos;
namespace B2CTouresBalon.Models
{
    public class PromocionesModel
    {
        public string SearchString { get; set; }

        public int Page { get; set; }
    
        public Producto[] Promociones { get; set; }
    }
}