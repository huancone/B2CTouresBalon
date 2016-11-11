using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Web.Mvc;
using B2CTouresBalon.Models;
using Enyim.Caching.Configuration;
using System.Net;
using Enyim.Caching.Memcached;
using Enyim.Caching;
using B2CTouresBalon.DAL.Security;
using System.Linq;
using B2CTouresBalon.ServicioOrdenes;
using B2CTouresBalon.ServicioClientes;
using RespuestaGenerica = B2CTouresBalon.ServicioOrdenes.RespuestaGenerica;

namespace B2CTouresBalon.Controllers
{
    public class OrdenController : Controller
    {
        // GET: Orden
        public ActionResult Index()
        {
            try
            {
                var currentUser = System.Web.HttpContext.Current.User as CustomPrincipal;
                if (currentUser == null) return RedirectToAction("Index", "Account");

                var proxy = new OrdenesTouresBalonClient();
            
                var lstOrden = proxy.ConsultarClientesOrdenes((int)currentUser.CustId);

                return View(lstOrden);
            }
            catch (FaultException<ConsultarClientesOrdenesFault> exf )
            {

                throw new Exception("Error al traer las ordenes " + exf.Detail.ConsultarClientesOrdenesFault1.mensaje);
            }
            catch (CommunicationException cex)
            {

                throw new CommunicationException("Error de conmunicacion " + cex.ToString());
            }

        }

        public ActionResult  Cancelar(int idOrden)
        {
            var cancela = new OrdenesTouresBalonClient(); 
            RespuestaGenerica rpta;
            var valorcancela = new RespuestaCancelacionModel();
            var idOrdenes = new int[1] { idOrden } ;

            try
            {
                rpta = cancela.CancelarOrdenes(idOrdenes);
            }
            catch ( FaultException<CancelarOrdenesFault> exf )
            {
                valorcancela.Respuesta = "Se presento un error al cancelar la orden " + idOrden + ". Intentelo más tarde (" + exf.Message + ")"   ;
                return View("_Cancelar", valorcancela);
            }
            catch (Exception)
            {
                valorcancela.Respuesta = "Se presento un error al cancelar la orden " + idOrden + ". Intentelo más tarde";
                return View("_Cancelar", valorcancela);
            }
            if (rpta == RespuestaGenerica.OK)
                valorcancela.Respuesta  = "Se cancelo la orden " + idOrden + " de forma exitosa";
            else
                valorcancela.Respuesta  = "Se presento un problema al cancelar la orden " + idOrden + ". Intentelo más tarde";

            return View("_Cancelar", valorcancela);
        }


        public ActionResult Detalle (int idOrden )
        {
            var proxy = new OrdenesTouresBalonClient();
            var criterio = new CriterioConsultaOrden
            {
                codigo = idOrden,
                tipo_consulta = TipoConsultaOrden.ORDEN
            };

            var orden = proxy.ConsultarOrdenes(criterio).First();

            return View("_Detalle", orden); 
        }
    }
}