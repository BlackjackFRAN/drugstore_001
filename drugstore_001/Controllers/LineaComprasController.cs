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
    public class LineaComprasController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: LineaCompras
        public ActionResult Index()
        {
            var lineaCompras = db.LineaCompras.Include(l => l.Compra).Include(l => l.Producto);
            return View(lineaCompras.ToList());
        }

        // GET: LineaCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaCompra lineaCompra = db.LineaCompras.Find(id);
            if (lineaCompra == null)
            {
                return HttpNotFound();
            }
            return View(lineaCompra);
        }

        // GET: LineaCompras/Create
        public ActionResult Create()
        {
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra");
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion");
            return View();
        }

        // POST: LineaCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idLineaCompra,idCompra,idProducto,cantidad,subtotal,precioCompra")] LineaCompra lineaCompra)
        {
            if (ModelState.IsValid)
            {
                db.LineaCompras.Add(lineaCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra", lineaCompra.idCompra);
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaCompra.idProducto);
            return View(lineaCompra);
        }

        // GET: LineaCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaCompra lineaCompra = db.LineaCompras.Find(id);
            if (lineaCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra", lineaCompra.idCompra);
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaCompra.idProducto);
            return View(lineaCompra);
        }

        // POST: LineaCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idLineaCompra,idCompra,idProducto,cantidad,subtotal,precioCompra")] LineaCompra lineaCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineaCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCompra = new SelectList(db.Compras, "idCompra", "idCompra", lineaCompra.idCompra);
            ViewBag.idProducto = new SelectList(db.Productoes, "idProducto", "descripcion", lineaCompra.idProducto);
            return View(lineaCompra);
        }

        // GET: LineaCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineaCompra lineaCompra = db.LineaCompras.Find(id);
            if (lineaCompra == null)
            {
                return HttpNotFound();
            }
            return View(lineaCompra);
        }

        // POST: LineaCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LineaCompra lineaCompra = db.LineaCompras.Find(id);
            db.LineaCompras.Remove(lineaCompra);
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
