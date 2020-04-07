using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaFacturacion.Models;

namespace SistemaFacturacion.Controllers
{
    public class EntradaMercanciasController : Controller
    {
        private ModelSF db = new ModelSF();

        // GET: EntradaMercancias
        public ActionResult Index()
        {
            var entradaMercancias = db.EntradaMercancias.Include(e => e.Producto).Include(e => e.Proveedore);
            return View(entradaMercancias.ToList());
        }

        // GET: EntradaMercancias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntradaMercancia entradaMercancia = db.EntradaMercancias.Find(id);
            if (entradaMercancia == null)
            {
                return HttpNotFound();
            }
            return View(entradaMercancia);
        }

        // GET: EntradaMercancias/Create
        public ActionResult Create()
        {
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre");
            ViewBag.proveedor_id = new SelectList(db.Proveedores, "Id", "Nombre");
            return View();
        }

        // POST: EntradaMercancias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,producto_id,proveedor_id,Cantidad,Fecha")] EntradaMercancia entradaMercancia)
        {
            if (ModelState.IsValid)
            {
                db.EntradaMercancias.Add(entradaMercancia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", entradaMercancia.producto_id);
            ViewBag.proveedor_id = new SelectList(db.Proveedores, "Id", "Nombre", entradaMercancia.proveedor_id);
            return View(entradaMercancia);
        }

        // GET: EntradaMercancias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntradaMercancia entradaMercancia = db.EntradaMercancias.Find(id);
            if (entradaMercancia == null)
            {
                return HttpNotFound();
            }
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", entradaMercancia.producto_id);
            ViewBag.proveedor_id = new SelectList(db.Proveedores, "Id", "Nombre", entradaMercancia.proveedor_id);
            return View(entradaMercancia);
        }

        // POST: EntradaMercancias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,producto_id,proveedor_id,Cantidad,Fecha")] EntradaMercancia entradaMercancia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entradaMercancia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", entradaMercancia.producto_id);
            ViewBag.proveedor_id = new SelectList(db.Proveedores, "Id", "Nombre", entradaMercancia.proveedor_id);
            return View(entradaMercancia);
        }

        // GET: EntradaMercancias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntradaMercancia entradaMercancia = db.EntradaMercancias.Find(id);
            if (entradaMercancia == null)
            {
                return HttpNotFound();
            }
            return View(entradaMercancia);
        }

        // POST: EntradaMercancias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntradaMercancia entradaMercancia = db.EntradaMercancias.Find(id);
            db.EntradaMercancias.Remove(entradaMercancia);
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
