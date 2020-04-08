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
    public class FacturacionsController : Controller
    {
        private ModelSF db = new ModelSF();

        // GET: Facturacions/Create
        public ActionResult Create()
        {
            var compras = db.Compras.Include(c => c.Cliente).Include(c => c.Producto);

            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Nombre");

            return View(compras.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,cliente_id,Descuento,Itbis,Monto,Fecha")] Facturacion facturacion)
        {
            decimal monto = 0, descuento = 0, sumatoria = 0;

            foreach (var item in db.Compras.Where(c => c.cliente_id == facturacion.cliente_id))
                sumatoria += item.Cantidad * db.Productos.SingleOrDefault(p => p.Id == item.producto_id).Precio;

            descuento = facturacion.descuento(sumatoria, db.Clientes.SingleOrDefault(c => c.Id == facturacion.cliente_id).Categoria);
            monto = facturacion.itbis(descuento);

            if (ModelState.IsValid)
            {
                facturacion.Descuento = descuento;
                facturacion.Monto = monto;
                facturacion.Fecha = DateTime.Now;
                db.Compras.RemoveRange(db.Compras.ToList());
                db.Facturacions.Add(facturacion);
                db.SaveChanges();
                return RedirectToAction("Facturaciones", "Consultas");
            }

            ViewBag.cliente_id = new SelectList(db.Clientes, "Id", "Cedula", facturacion.cliente_id);
            return View(facturacion);
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
