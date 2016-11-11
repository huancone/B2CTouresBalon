using System.Web.Mvc;
using B2CTouresBalon.Models;
using B2CTouresBalon.ServicioProductos;

namespace B2CTouresBalon.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /Home/
        public ActionResult Index()
        {
            var proxy   = new ProductosTouresBalonClient();
            var productos = new PromocionesModel {Promociones = proxy.ConsultarCampaniaProducto()};

            return View(productos);
        }
    }
}