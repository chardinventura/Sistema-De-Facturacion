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

            return View(db.Productos.ToList());
        }
        public ActionResult Clientes()
        {
            return View();
        }
        public ActionResult Proveedores(string query, string seleccion)
        {
            if (query != "" && query != null)
            {
                if(seleccion.Equals("0"))
                    return View(db.Proveedores.Where(p => p.Nombre == query));
                else
                    return View(db.Proveedores.Where(p => p.Email == query));
            }

            return View(db.Proveedores.ToList());
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