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
    public class LineaVentasController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: LineaVentas
        public ActionResult Index()
        {
            var lineaVentas = db.LineaVentas.Include(l => l.Producto).Include(l => l.Venta);
            return View(lineaVentas.ToList());
        }

        // GET: LineaVentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaVenta lineaVenta = db.LineaVentas.Find(id);
            if (lineaVenta == null)
            {
                return HttpNotFound();
            }
            return View(lineaVenta);
        }

        // GET: LineaVentas/Create
        public ActionResult Create()
        {
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion");
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "idVenta");
            return View();
        }

        // POST: LineaVentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLineaVenta,idVenta,idProducto,cantidad,subtotal,precio")] LineaVenta lineaVenta)
        {
            if (ModelState.IsValid)
            {
                db.LineaVentas.Add(lineaVenta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaVenta.idProducto);
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "idVenta", lineaVenta.idVenta);
            return View(lineaVenta);
        }

        // GET: LineaVentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaVenta lineaVenta = db.LineaVentas.Find(id);
            if (lineaVenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaVenta.idProducto);
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "idVenta", lineaVenta.idVenta);
            return View(lineaVenta);
        }

        // POST: LineaVentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLineaVenta,idVenta,idProducto,cantidad,subtotal,precio")] LineaVenta lineaVenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineaVenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaVenta.idProducto);
            ViewBag.idVenta = new SelectList(db.Ventas, "idVenta", "idVenta", lineaVenta.idVenta);
            return View(lineaVenta);
        }

        // GET: LineaVentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaVenta lineaVenta = db.LineaVentas.Find(id);
            if (lineaVenta == null)
            {
                return HttpNotFound();
            }
            return View(lineaVenta);
        }

        // POST: LineaVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LineaVenta lineaVenta = db.LineaVentas.Find(id);
            db.LineaVentas.Remove(lineaVenta);
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
