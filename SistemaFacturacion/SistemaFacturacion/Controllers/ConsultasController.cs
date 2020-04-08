using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaFacturacion.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;

namespace SistemaFacturacion.Controllers
{
    public class ConsultasController : Controller
    {
        private ModelSF db = new ModelSF();

        // GET: Consultas
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Productos(string query)
        {
            if(query != "" && query != null)
                return View(db.Productos.Where(p => p.Nombre == query));
            else
                return View(db.Productos.ToList());
        }
        public ActionResult Clientes()
        {
            return View();
        }
        public ActionResult Proveedores()
        {
            return View();
        }
        public ActionResult Entradas()
        {
            return View();
        }
        public ActionResult Facturaciones()
        {
            return View();
        }
    }
}