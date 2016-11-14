using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using B2CTouresBalon.DAL.Security;
using B2CTouresBalon.Models;
using B2CTouresBalon.ServicioClientes;
using B2CTouresBalon.ServicioOrdenes;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Item = B2CTouresBalon.ServicioOrdenes.Item;

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
                checkout.Firstname = c.nombres;
                checkout.LastName = c.apellidos;
                checkout.CreditCardType = c.tipo_tarjeta;
                checkout.Creditcard = c.numero_tarjeta;
                checkout.CustomerId = c.id_cliente;
                if (c.email == null) return RedirectToAction("Index", "Account");
            }

            var clientConfiguration = new MemcachedClientConfiguration { Protocol = MemcachedProtocol.Binary };
            clientConfiguration.Servers.Add(new IPEndPoint(IPAddress.Parse("192.168.99.100"), 32768));

            using (var cartCacheClient = new MemcachedClient(clientConfiguration))
            {
                checkout.Cart = cartCacheClient.Get<Cart>("Cart-" + currentUser.UserName);
                foreach (var item in checkout.Cart.Items)
                {
                    checkout.Total = checkout.Total + item.Cantidad * (item.Producto.tipo_espectaculo.precio + item.Producto.tipo_hospedaje.precio + item.Producto.tipo_transporte.precio);
                }
                if (checkout.Cart == null) return RedirectToAction("Index", "Home");
            }

            return View("Create", checkout);
        }

        // POST: Checkout/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CheckoutModel checkout)
        {
            var items = new List<ServicioOrdenes.Item>();
            try
            {
                var proxy = new OrdenesTouresBalonClient();

                var currentUser = System.Web.HttpContext.Current.User as CustomPrincipal;
                if (currentUser == null) return RedirectToAction("Index", "Account");

                var clientConfiguration = new MemcachedClientConfiguration { Protocol = MemcachedProtocol.Binary };
                clientConfiguration.Servers.Add(new IPEndPoint(IPAddress.Parse("192.168.99.100"), 32768));
                using (var cartCacheClient = new MemcachedClient(clientConfiguration))
                {
                    checkout.Cart = cartCacheClient.Get<Cart>("Cart-" + currentUser.UserName);
                    foreach (var item in checkout.Cart.Items)
                    {
                        checkout.Total = checkout.Total + item.Cantidad * (item.Producto.tipo_espectaculo.precio + item.Producto.tipo_hospedaje.precio + item.Producto.tipo_transporte.precio);
                    }
                    if (checkout.Cart == null) return RedirectToAction("Index", "Home");
                }

                items.AddRange(checkout.Cart.Items.Select(i => new Item
                {
                    precio = i.Producto.tipo_espectaculo.precio + i.Producto.tipo_hospedaje.precio + i.Producto.tipo_transporte.precio,
                    cantidad = i.Cantidad,
                    id_prod = i.Producto.id_producto,
                    nombre_prod = i.Producto.descripcion
                }));
                var orden = new Orden
                {
                    precio = checkout.Total,
                    estatus = EstatusOrden.VALIDACION,
                    fecha_orden = DateTime.Now,
                    id_cliente = checkout.CustomerId,
                    item = items.ToArray()
                };

                var orderId = proxy.CrearOrdenes(orden);
                if (orderId == null)
                {
                    return View();
                }
                else
                {
                    using (var cartCacheClient = new MemcachedClient(clientConfiguration))
                    {
                        cartCacheClient.Remove("Cart-" + currentUser.UserName);
                    }
                    return RedirectToAction("Index", "Orden", new { OrderId = orderId });
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
