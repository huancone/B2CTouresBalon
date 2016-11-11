using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using B2CTouresBalon.DAL.Security;
using B2CTouresBalon.Models;
using B2CTouresBalon.ServicioClientes;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace B2CTouresBalon.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout/Create
        public ActionResult Create()
        {
            var checkout = new CheckoutModel();

            var currentUser = System.Web.HttpContext.Current.User as CustomPrincipal;
            if (currentUser == null) return RedirectToAction("Index", "Account");

            using (var proxy = new ClientesTouresBalonClient())
            {
                var c = proxy.ConsultarPorIdentificacionCliente((int)currentUser.CustId);
                checkout.Cliente = c;
                if (checkout.Cliente == null) return RedirectToAction("Index", "Account");
            }

            var clientConfiguration = new MemcachedClientConfiguration { Protocol = MemcachedProtocol.Binary };
            clientConfiguration.Servers.Add(new IPEndPoint(IPAddress.Parse("memcached"), 32768));

            using (var cartCacheClient = new MemcachedClient(clientConfiguration))
            {
                checkout.Cart = cartCacheClient.Get<Cart>("Cart-" + currentUser.UserName);
                if (checkout.Cart == null) return RedirectToAction("Index", "Product");
            }

            return View();
        }

        // POST: Checkout/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CheckoutModel checkout)
        {
            try
            {

                return RedirectToAction("Index", "Orden");
            }
            catch
            {
                return View();
            }
        }
    }
}
