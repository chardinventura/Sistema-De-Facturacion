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
            var existenciaStock = db.Stocks.SingleOrDefault(s => s.producto_id == entradaMercancia.producto_id);

            if (ModelState.IsValid)
            {
                if (existenciaStock != null)
                    db.Stocks.SingleOrDefault(s => s.Id == existenciaStock.Id).Cantidad += entradaMercancia.Cantidad;
                else
                    db.Stocks.Add(new Stock()
                    {
                        Cantidad = entradaMercancia.Cantidad,
                        producto_id = entradaMercancia.producto_id
                    });

                entradaMercancia.Fecha = DateTime.Now;
                db.EntradaMercancias.Add(entradaMercancia);
                db.SaveChanges();

                return RedirectToAction("Index", "Clientes");
            }

            ViewBag.producto_id = new SelectList(db.Productos, "Id", "Nombre", entradaMercancia.producto_id);
            ViewBag.proveedor_id = new SelectList(db.Proveedores, "Id", "Nombre", entradaMercancia.proveedor_id);
            return View(entradaMercancia);
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
