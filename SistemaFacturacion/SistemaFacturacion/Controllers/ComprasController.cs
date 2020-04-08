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
    public class ComprasController : Controller
    {
        private ModelSF db = new ModelSF();

        // GET: Compras
        public ActionResult Index()
        {
            var compras = db.Compras.Include(c => c.Cliente).Include(c => c.Producto);
            return View(compras.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Nombre");
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,producto_id,cliente_id,Cantidad,Fecha")] Compra compra)
        {
            var existenciaStock = db.Stocks.SingleOrDefault(s => s.producto_id == compra.producto_id);
            var existenciaCompra = db.Compras.Where(c => c.producto_id == compra.producto_id && c.cliente_id == compra.cliente_id).Count();
            var cantidadValida = db.Stocks.SingleOrDefault(s => s.producto_id == compra.producto_id).Cantidad;

            if (ModelState.IsValid &&  existenciaCompra != 0 && cantidadValida >= compra.Cantidad)
            {
                if (existenciaStock != null)
                    existenciaStock.Cantidad -= compra.Cantidad;

                compra.Fecha = DateTime.Now;
                db.Compras.Add(compra);
                db.SaveChanges();
                return RedirectToAction("Index", "Facturacions");
            }

            if(cantidadValida < compra.Cantidad)
            {
                ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Nombre", compra.cliente_id);
                ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", compra.producto_id);

                ViewBag.error = "Error, cantidad elevada, cantidad disponible es " + cantidadValida;

                return View(compra);
            }

            if (db.Compras.Where(c => c.producto_id == compra.producto_id && c.cliente_id == compra.cliente_id).Count() != 0)
            {
                ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Nombre", compra.cliente_id);
                ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", compra.producto_id);

                ViewBag.error = "Error, ya habia comprado el producto " + db.Productos.SingleOrDefault(p => p.Id == compra.producto_id).Nombre;

                return View(compra);
            }
            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Nombre", compra.cliente_id);
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", compra.producto_id);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Cedula", compra.cliente_id);
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", compra.producto_id);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,producto_id,cliente_id,Cantidad,Fecha")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                compra.Fecha = DateTime.Now;
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Cedula", compra.cliente_id);
            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", compra.producto_id);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compras.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compra compra = db.Compras.Find(id);
            db.Compras.Remove(compra);
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
