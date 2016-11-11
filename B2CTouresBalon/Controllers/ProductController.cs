using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using B2CTouresBalon.Models;
using B2CTouresBalon.ServicioProductos;

namespace B2CTouresBalon.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [OutputCache(Duration = 30000, VaryByParam = "searchString;page")]
        public ActionResult Index(string searchString, int page)
        {
            var proxy = new ProductosTouresBalonClient();
            var productos = new ProductosModel
            {
                Productos = proxy.ConsultarProducto(TipoConsultaProducto.DESCRIPCION, page + "@" + searchString),
                Page = page,
                SearchString = searchString
            };
            return View(productos);
        }

        // GET: Product/Details/5
        [OutputCache(Duration = 30000, VaryByParam = "idProducto")]
        public ActionResult Details(int idProducto)
        {
            var proxy = new ProductosTouresBalonClient();
            var productos = new ProductosModel { Productos = proxy.ConsultarProducto(TipoConsultaProducto.ID, idProducto.ToString()) };
            return View(productos.Productos.First());
        }
    }
}
