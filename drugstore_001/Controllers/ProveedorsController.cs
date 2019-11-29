using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using drugstore_001;
using drugstore_001.Models;

namespace drugstore_001.Controllers
{
    public class ProveedorsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Proveedors
        public ActionResult Index()
        {
            var proveedors = db.Proveedors.Include(p => p.Direccion);
            return View(proveedors.ToList());
        }

        // GET: Proveedors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedors.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: Proveedors/Create
        public ActionResult Create()
        {
            List<Provincia> lista = db.Provincias.ToList();
            ViewBag.ListaProvincia = new SelectList(lista, "idProvincia", "nombre");
            return View();
        }

        public JsonResult GetLocalidades(int idProvincia)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Localidad> lista = db.Localidads.Where(x => x.idProvincia == idProvincia).ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProveedorCLS proveedor)
        {
            if (!ModelState.IsValid)
            {
                List<Provincia> lista = db.Provincias.ToList();
                ViewBag.ListaProvincia = new SelectList(lista, "idProvincia", "nombre");
                return View(proveedor);
            }



            Direccion dir = new Direccion();
            dir.calle = proveedor.calle;
            dir.numero = proveedor.numero;
            dir.codigoPostal = proveedor.codigoPostal;
            dir.idDireccion = (int)proveedor.idDireccion;
            db.Direccions.Add(dir);
            db.SaveChanges();


            Proveedor prov = new Proveedor();
            prov.idProveedor = proveedor.idProveedor;
            prov.nombre = proveedor.nombre;
            prov.apellido = proveedor.apellido;
            prov.cuit = proveedor.cuit;
            Direccion direccion = db.Direccions.Where(x => x.calle == proveedor.calle && x.numero == proveedor.numero).FirstOrDefault();
            prov.idDireccion = direccion.idDireccion;
            db.Proveedors.Add(prov);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Proveedors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProveedor,idDireccion,nombre,apellido,cuit")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Proveedors.Add(proveedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", proveedor.idDireccion);
            return View(proveedor);
        }
        */

        // GET: Proveedors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedors.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", proveedor.idDireccion);
            return View(proveedor);
        }

        // POST: Proveedors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProveedor,idDireccion,nombre,apellido,cuit")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", proveedor.idDireccion);
            return View(proveedor);
        }

        // GET: Proveedors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedors.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedor proveedor = db.Proveedors.Find(id);
            db.Proveedors.Remove(proveedor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
