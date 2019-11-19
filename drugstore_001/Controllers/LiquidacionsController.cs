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
    public class LiquidacionsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Liquidacions
        public ActionResult Index()
        {
            var liquidacions = db.Liquidacions.Include(l => l.Empleado);
            return View(liquidacions.ToList());
        }

        // GET: Liquidacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidacion liquidacion = db.Liquidacions.Find(id);
            if (liquidacion == null)
            {
                return HttpNotFound();
            }
            return View(liquidacion);
        }

        // GET: Liquidacions/Create
        public ActionResult Create()
        {
            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre");
            return View();
        }

        // POST: Liquidacions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLiquidacion,idEmpleado,anio,mes,fechaDeposito,totalNeto,bruto")] Liquidacion liquidacion)
        {
            if (ModelState.IsValid)
            {
                db.Liquidacions.Add(liquidacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre", liquidacion.idEmpleado);
            return View(liquidacion);
        }

        // GET: Liquidacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidacion liquidacion = db.Liquidacions.Find(id);
            if (liquidacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre", liquidacion.idEmpleado);
            return View(liquidacion);
        }

        // POST: Liquidacions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLiquidacion,idEmpleado,anio,mes,fechaDeposito,totalNeto,bruto")] Liquidacion liquidacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liquidacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEmpleado = new SelectList(db.Empleadoes, "idEmpleado", "nombre", liquidacion.idEmpleado);
            return View(liquidacion);
        }

        // GET: Liquidacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidacion liquidacion = db.Liquidacions.Find(id);
            if (liquidacion == null)
            {
                return HttpNotFound();
            }
            return View(liquidacion);
        }

        // POST: Liquidacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Liquidacion liquidacion = db.Liquidacions.Find(id);
            db.Liquidacions.Remove(liquidacion);
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
