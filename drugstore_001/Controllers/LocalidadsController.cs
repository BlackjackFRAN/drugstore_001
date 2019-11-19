using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using drugstore_001;

namespace drugstore_001.Controllers
{
    public class LocalidadsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Localidads
        public ActionResult Index()
        {
            var localidads = db.Localidads.Include(l => l.Provincia);
            return View(localidads.ToList());
        }

        // GET: Localidads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidads.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

        // GET: Localidads/Create
        public ActionResult Create()
        {
            ViewBag.idProvincia = new SelectList(db.Provincias, "idProvincia", "nombre");
            return View();
        }

        // POST: Localidads/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoPostal,nombre,idProvincia")] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                db.Localidads.Add(localidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProvincia = new SelectList(db.Provincias, "idProvincia", "nombre", localidad.idProvincia);
            return View(localidad);
        }

        // GET: Localidads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidads.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProvincia = new SelectList(db.Provincias, "idProvincia", "nombre", localidad.idProvincia);
            return View(localidad);
        }

        // POST: Localidads/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoPostal,nombre,idProvincia")] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProvincia = new SelectList(db.Provincias, "idProvincia", "nombre", localidad.idProvincia);
            return View(localidad);
        }

        // GET: Localidads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidads.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

        // POST: Localidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Localidad localidad = db.Localidads.Find(id);
            db.Localidads.Remove(localidad);
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
