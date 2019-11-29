using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace drugstore_001.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HttpGet]
        public ActionResult UnauthorizedOperation(String tipo, String msjeErrorExcepcion)
        {

            ViewBag.msjeErrorExcepcion = msjeErrorExcepcion;
            return View();
        }
    }
}