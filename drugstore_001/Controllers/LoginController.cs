using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace drugstore_001.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(drugstore_001.Usuario usuario)
        {
            using (Database1Entities db = new Database1Entities())
            {
                var userDetails = db.Usuarios
                    .Where(x => x.usuario1 == usuario.usuario1 && x.contrasenia == usuario.contrasenia)
                    .FirstOrDefault();
                if (userDetails==null)
                {
                    usuario.LoginErrorMsj = "Error nombre de usuario o contraseña.";
                    return View("Index", usuario);
                }
                else
                {
                    Session["userId"] = userDetails.idEmpleado;
                    Session["userName"] = userDetails.usuario1;
                    Session["user"] = userDetails;
                    return RedirectToAction("Index", "Home");
                }
            }
            
            
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}