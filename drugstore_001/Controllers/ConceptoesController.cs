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
    public class ConceptoesController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Conceptoes
        public ActionResult Index()
        {
            return View(db.Conceptoes.ToList());
        }

        // GET: Conceptoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concepto concepto = db.Conceptoes.Find(id);
            if (concepto == null)
            {
                return HttpNotFound();
            }
            return View(concepto);
        }

        // GET: Conceptoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Conceptoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idConcepto,descripcion,porcentaje,total")] Concepto concepto)
        {
            if (ModelState.IsValid)
            {
                db.Conceptoes.Add(concepto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(concepto);
        }

        // GET: Conceptoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concepto concepto = db.Conceptoes.Find(id);
            if (concepto == null)
            {
                return HttpNotFound();
            }
            return View(concepto);
        }

        // POST: Conceptoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idConcepto,descripcion,porcentaje,total")] Concepto concepto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(concepto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(concepto);
        }

        // GET: Conceptoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Concepto concepto = db.Conceptoes.Find(id);
            if (concepto == null)
            {
                return HttpNotFound();
            }
            return View(concepto);
        }

        // POST: Conceptoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Concepto concepto = db.Conceptoes.Find(id);
            db.Conceptoes.Remove(concepto);
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
