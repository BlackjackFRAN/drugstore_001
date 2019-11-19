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
    public class FamiliarsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Familiars
        public ActionResult Index()
        {
            var familiars = db.Familiars.Include(f => f.Empleado);
            return View(familiars.ToList());
        }

        // GET: Familiars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familiar familiar = db.Familiars.Find(id);
            if (familiar == null)
            {
                return HttpNotFound();
            }
            return View(familiar);
        }

        // GET: Familiars/Create
        public ActionResult Create()
        {
            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre");
            return View();
        }

        // POST: Familiars/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idFamiliar,idEmpleado,Parentezco,nombre,apellido,fechaNacimiento")] Familiar familiar)
        {
            if (ModelState.IsValid)
            {
                db.Familiars.Add(familiar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre", familiar.idEmpleado);
            return View(familiar);
        }

        // GET: Familiars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familiar familiar = db.Familiars.Find(id);
            if (familiar == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre", familiar.idEmpleado);
            return View(familiar);
        }

        // POST: Familiars/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFamiliar,idEmpleado,Parentezco,nombre,apellido,fechaNacimiento")] Familiar familiar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(familiar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre", familiar.idEmpleado);
            return View(familiar);
        }

        // GET: Familiars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Familiar familiar = db.Familiars.Find(id);
            if (familiar == null)
            {
                return HttpNotFound();
            }
            return View(familiar);
        }

        // POST: Familiars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Familiar familiar = db.Familiars.Find(id);
            db.Familiars.Remove(familiar);
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
