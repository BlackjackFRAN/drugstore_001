using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace drugstore_001.Filters
{
    public class AutorizarUsuario
    {
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
        public class AuthorizeUser : AuthorizeAttribute
        {
            private Usuario usuario;
            private Database1Entities db = new Database1Entities();
            private int idTipo;

            public AuthorizeUser(int idTipo = 0)
            {
                this.idTipo = idTipo;
            }


            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                String tipo = "";
                String nombreModulo = "";
                try
                {
                    usuario = (Usuario)HttpContext.Current.Session["User"];
                    var lstMisOperaciones = from m in db.Usuarios
                                            where m.tipo == usuario.tipo
                                                
                                            select m;


                    if (lstMisOperaciones.ToList().Count() == 0)
                    {
                      

                        filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?tipo=" + tipo +"&msjeErrorExcepcion=");
                    }
                }
                catch (Exception ex)
                {
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?tipo=" + tipo +"&msjeErrorExcepcion=" + ex.Message);
                }
            }


        }
    }
}