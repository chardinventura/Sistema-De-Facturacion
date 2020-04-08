﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaFacturacion.Models;
using System.Web.Mvc;
using System.Data.Entity;

namespace SistemaFacturacion.Controllers
{
    public class ConsultasController : Controller
    {
        private ModelSF db = new ModelSF();
        private EntradaMercancia entradaMercancia = new EntradaMercancia();
        private Facturacion facturacion = new Facturacion();

        // GET: Consultas
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Productos(string query)
        {
            if(!string.IsNullOrEmpty(query))
                return View(db.Productos.Where(p => p.Nombre == query));

            return View(db.Productos.ToList());
        }
        public ActionResult Clientes(string query, string seleccion, string opcion)
        {
            if (!string.IsNullOrEmpty(query))
            {
                if(opcion.Equals("0"))
                    return View(db.Clientes.Where(p => p.Nombre == query));
                else
                {
                    if (seleccion.Equals("0"))
                        return View(db.Clientes.ToList());
                    else if(seleccion.Equals("1"))
                        return View(db.Clientes.Where(c => c.Categoria == "premium"));
                    else
                        return View(db.Clientes.Where(c => c.Categoria == "regular"));
                }
            }

            if (!string.IsNullOrEmpty(seleccion))
            {
                if (seleccion.Equals("0"))
                    return View(db.Clientes.ToList());
                else if (seleccion.Equals("1"))
                    return View(db.Clientes.Where(c => c.Categoria == "premium"));
                else
                    return View(db.Clientes.Where(c => c.Categoria == "regular"));
            }

            return View(db.Clientes.ToList());
        }
        public ActionResult Proveedores(string query, string seleccion)
        {
            if (!string.IsNullOrEmpty(query))
            {
                if(seleccion.Equals("0"))
                    return View(db.Proveedores.Where(p => p.Nombre == query));
                else
                    return View(db.Proveedores.Where(p => p.Email == query));
            }

            return View(db.Proveedores.ToList());
        }
        public ActionResult Entradas(string query, string seleccion, string opcion)
        {
            var filtroProducto = db.EntradaMercancias.Where(e => e.Producto.Nombre == query);
            var filtroFecha = db.EntradaMercancias.Where(e => e.Fecha.Value.Day == 12);
            var filtroProveedor = db.EntradaMercancias.Where(e => e.Proveedore.Nombre == query);

            if (!string.IsNullOrEmpty(query))
            {
                if (seleccion.Equals("0"))
                {
                    if(opcion.Equals("0"))
                    {
                        ViewBag.sumatoria = entradaMercancia.sumatoria(filtroProducto.ToList());
                        ViewBag.promedio = entradaMercancia.promedio(filtroProducto.ToList());
                        ViewBag.conteo = entradaMercancia.conteo(filtroProducto.ToList());
                    }
                    
                    return View(filtroProducto);
                }
                else if (seleccion.Equals("1"))
                {
                    if (opcion.Equals("0"))
                    {
                        ViewBag.sumatoria = entradaMercancia.sumatoria(filtroFecha.ToList());
                        ViewBag.promedio = entradaMercancia.promedio(filtroFecha.ToList());
                        ViewBag.conteo = entradaMercancia.conteo(filtroFecha.ToList());
                    }

                    return View(filtroFecha);
                }
                else
                {
                    if (opcion.Equals("0"))
                    {
                        ViewBag.sumatoria = entradaMercancia.sumatoria(filtroProveedor.ToList());
                        ViewBag.promedio = entradaMercancia.promedio(filtroProveedor.ToList());
                        ViewBag.conteo = entradaMercancia.conteo(filtroProveedor.ToList());
                    }
                    
                    return View(filtroProveedor);
                }
            }

            return View(db.EntradaMercancias.ToList());
        }
        public ActionResult Facturaciones(string query, string seleccion, string opcion)
        {
            var filtroFecha = db.Facturacions.Where(f => f.Fecha.Value.Day == 12);
            var filtroCliente = db.Facturacions.Where(f => f.Cliente.Nombre == query);

            if (!string.IsNullOrEmpty(query))
            {
                if (seleccion.Equals("0"))
                {
                    if (opcion.Equals("0"))
                    {
                        ViewBag.sumatoria = facturacion.sumatoria(filtroCliente.ToList());
                        ViewBag.promedio = facturacion.promedio(filtroCliente.ToList());
                        ViewBag.conteo = facturacion.conteo(filtroCliente.ToList());
                        ViewBag.min = facturacion.valorMinimo(filtroCliente.ToList());
                        ViewBag.max = facturacion.valorMaximo(filtroCliente.ToList());
                    }

                    return View(filtroCliente);
                }
                else if (seleccion.Equals("1"))
                {
                    if (opcion.Equals("0"))
                    {
                        ViewBag.sumatoria = facturacion.sumatoria(filtroFecha.ToList());
                        ViewBag.promedio = facturacion.promedio(filtroFecha.ToList());
                        ViewBag.conteo = facturacion.conteo(filtroFecha.ToList());
                        ViewBag.min = facturacion.valorMinimo(filtroFecha.ToList());
                        ViewBag.max = facturacion.valorMaximo(filtroFecha.ToList());
                    }

                    return View(filtroFecha);
                }
            }

            return View(db.Facturacions.ToList());
        }
    }
}