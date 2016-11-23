using System;
using System.Linq;
using System.ServiceModel;
using System.Web.Mvc;
using B2CTouresBalon.DAL.Security;
using B2CTouresBalon.Models;
using B2CTouresBalon.ProxyServiceB2C;
using B2CTouresBalon.ServiceOrdenes;
using CancelarOrdenesFault = B2CTouresBalon.ProxyServiceB2C.CancelarOrdenesFault;
using ConsultarClientesOrdenesFault = B2CTouresBalon.ProxyServiceB2C.ConsultarClientesOrdenesFault;
using RespuestaGenerica = B2CTouresBalon.ProxyServiceB2C.RespuestaGenerica;

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

                var proxy = new ServiceProxyB2CClient();
            
                var lstOrden = proxy.ConsultarClientesOrdenes((int)currentUser.CustId);

                return View(lstOrden);
            }
            catch (FaultException<ConsultarClientesOrdenesFault> exf )
            {

                throw new Exception("Error al traer las ordenes " + exf.Detail.ConsultarClientesOrdenesFault1.mensaje);
            }
            catch (CommunicationException cex)
            {

                throw new CommunicationException("Error de conmunicacion " + cex);
            }

        }

        public ActionResult  Cancelar(int idOrden)
        {
            var cancela = new ServiceProxyB2CClient(); 
            RespuestaGenerica rpta;
            var valorcancela = new RespuestaCancelacionModel();
            var idOrdenes = new[] { idOrden } ;

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