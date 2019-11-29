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
    public class EmpleadoesController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Empleadoes
        public ActionResult Index()
        {
            var empleadoes = db.Empleadoes.Include(e => e.Direccion);
            return View(empleadoes.ToList());
        }

        // GET: Empleadoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleadoes/Create
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
        public ActionResult Create(EmpleadoCLS empleado)
        {
            if (!ModelState.IsValid)
            {
            List<Provincia> lista = db.Provincias.ToList();
            ViewBag.ListaProvincia = new SelectList(lista, "idProvincia", "nombre");
            return View(empleado);
            }



            Direccion dir = new Direccion();
            dir.calle = empleado.calle;
            dir.numero = empleado.numero;
            dir.codigoPostal = empleado.codigoPostal;
            dir.idDireccion = 53;
            db.Direccions.Add(dir);
            db.SaveChanges();

            Empleado emp = new Empleado();
            emp.tipo = empleado.tipo;
            emp.nombre = empleado.nombre;
            emp.apellido = empleado.apellido;
            emp.fechaNacimiento = empleado.fechaNacimiento;
            emp.sueldoBase = empleado.sueldoBase;
            emp.estadoCivil = empleado.estadoCivil;
            emp.dni = empleado.dni;
            Direccion direccion = db.Direccions.Where(x=>x.calle == empleado.calle && x.numero == empleado.numero).FirstOrDefault();
            emp.idDireccion = direccion.idDireccion;
            db.Empleadoes.Add(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       

        // POST: Empleadoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEmpleado,idDireccion,tipo,nombre,apellido,fechaNacimiento,sueldoBase,estadoCivil,dni")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleadoes.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", empleado.idDireccion);
            return View(empleado);
        }
        */


        // GET: Empleadoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", empleado.idDireccion);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEmpleado,idDireccion,tipo,nombre,apellido,fechaNacimiento,sueldoBase,estadoCivil,dni")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDireccion = new SelectList(db.Direccions, "idDireccion", "calle", empleado.idDireccion);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleadoes.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleado empleado = db.Empleadoes.Find(id);
            db.Empleadoes.Remove(empleado);
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
