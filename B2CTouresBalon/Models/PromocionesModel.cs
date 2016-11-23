using B2CTouresBalon.ProxyServiceB2C;

namespace B2CTouresBalon.Models
{
    public class PromocionesModel
    {
        public string SearchString { get; set; }

        public int Page { get; set; }
    
        public Producto[] Promociones { get; set; }
    }
}