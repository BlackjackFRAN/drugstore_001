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
    public class DetalleLiquidacionsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: DetalleLiquidacions
        public ActionResult Index()
        {
            var detalleLiquidacions = db.DetalleLiquidacions.Include(d => d.Concepto).Include(d => d.Liquidacion);
            return View(detalleLiquidacions.ToList());
        }

        // GET: DetalleLiquidacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleLiquidacion detalleLiquidacion = db.DetalleLiquidacions.Find(id);
            if (detalleLiquidacion == null)
            {
                return HttpNotFound();
            }
            return View(detalleLiquidacion);
        }

        // GET: DetalleLiquidacions/Create
        public ActionResult Create()
        {
            ViewBag.idConcepto = new SelectList(db.Conceptoes, "idConcepto", "descripcion");
            ViewBag.idLiquidacion = new SelectList(db.Liquidacions, "idLiquidacion", "idLiquidacion");
            return View();
        }

        // POST: DetalleLiquidacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetalle,idLiquidacion,idConcepto,monto")] DetalleLiquidacion detalleLiquidacion)
        {
            if (ModelState.IsValid)
            {
                db.DetalleLiquidacions.Add(detalleLiquidacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idConcepto = new SelectList(db.Conceptoes, "idConcepto", "descripcion", detalleLiquidacion.idConcepto);
            ViewBag.idLiquidacion = new SelectList(db.Liquidacions, "idLiquidacion", "idLiquidacion", detalleLiquidacion.idLiquidacion);
            return View(detalleLiquidacion);
        }

        // GET: DetalleLiquidacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleLiquidacion detalleLiquidacion = db.DetalleLiquidacions.Find(id);
            if (detalleLiquidacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idConcepto = new SelectList(db.Conceptoes, "idConcepto", "descripcion", detalleLiquidacion.idConcepto);
            ViewBag.idLiquidacion = new SelectList(db.Liquidacions, "idLiquidacion", "idLiquidacion", detalleLiquidacion.idLiquidacion);
            return View(detalleLiquidacion);
        }

        // POST: DetalleLiquidacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetalle,idLiquidacion,idConcepto,monto")] DetalleLiquidacion detalleLiquidacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleLiquidacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idConcepto = new SelectList(db.Conceptoes, "idConcepto", "descripcion", detalleLiquidacion.idConcepto);
            ViewBag.idLiquidacion = new SelectList(db.Liquidacions, "idLiquidacion", "idLiquidacion", detalleLiquidacion.idLiquidacion);
            return View(detalleLiquidacion);
        }

        // GET: DetalleLiquidacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleLiquidacion detalleLiquidacion = db.DetalleLiquidacions.Find(id);
            if (detalleLiquidacion == null)
            {
                return HttpNotFound();
            }
            return View(detalleLiquidacion);
        }

        // POST: DetalleLiquidacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleLiquidacion detalleLiquidacion = db.DetalleLiquidacions.Find(id);
            db.DetalleLiquidacions.Remove(detalleLiquidacion);
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
