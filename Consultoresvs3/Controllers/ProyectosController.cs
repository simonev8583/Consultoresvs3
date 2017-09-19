using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Consultoresvs3.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace Consultoresvs3.Controllers
{
    public class ProyectosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Proyectos
        [Authorize]
        public ActionResult Index()
        {
            var proyectos = db.Proyectos.Include(p => p.Empresa).Include(p => p.Estado);
            return View(proyectos.ToList());
        }

        // GET: Proyectos/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyectos.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // GET: Proyectos/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEmpresa");
            ViewBag.IdEstado = new SelectList(db.EstadoProyectos, "Id", "Nombre");
            return View();
        }

        // POST: Proyectos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Precio,TiempoEstipulado,IdEmpresa,Fecha,FechaFin")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                proyecto.IdEstado = 1;
                db.Proyectos.Add(proyecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEmpresa", proyecto.IdEmpresa);
            ViewBag.IdEstado = new SelectList(db.EstadoProyectos, "Id", "Nombre", proyecto.IdEstado);
            return View(proyecto);
        }

        // GET: Proyectos/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyectos.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEmpresa", proyecto.IdEmpresa);
            ViewBag.IdEstado = new SelectList(db.EstadoProyectos, "Id", "Nombre", proyecto.IdEstado);
            return View(proyecto);
        }

        // POST: Proyectos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Precio,TiempoEstipulado,IdEmpresa,Fecha,IdEstado")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proyecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpresa = new SelectList(db.Empresas, "Id", "NombreEmpresa", proyecto.IdEmpresa);
            ViewBag.IdEstado = new SelectList(db.EstadoProyectos, "Id", "Nombre", proyecto.IdEstado);
            return View(proyecto);
        }

        // GET: Proyectos/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = db.Proyectos.Find(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }



        // POST: Proyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proyecto proyecto = db.Proyectos.Find(id);
            db.Proyectos.Remove(proyecto);
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
        [HttpGet]
        public ActionResult EditarEstado(int id)
        {
            var estado = db.EstadoProyectos.Where(t => t.Nombre == "Finalizado").First();
            db.Proyectos.Find(id).Estado = estado;
            db.Proyectos.Find(id).IdEstado = estado.Id;
            db.SaveChanges();
            CrearReporte(id);
            return RedirectToAction("Index");
        }
        public ActionResult CrearReporte(int id)
        {
            var reporte = db.ReporteUsuarios.Where(t => t.Proyecto.Id == id).ToList();
            int horastrabajadas = 0;
            decimal utilidad = 0;
            for (int i = 0; i < reporte.Count; i++)
            {
                horastrabajadas += reporte[i].HTrabajadas;
                utilidad += (reporte[i].HTrabajadas * reporte[i].Usuario.ValorHoraPrestacionesSociales);
            }
            ReporteProyecto nuevoreporte = new ReporteProyecto();
            nuevoreporte.HorasInvertidas = horastrabajadas;
            Proyecto proyecto = db.Proyectos.Find(id);
            nuevoreporte.IdProyecto = proyecto.Id;
            nuevoreporte.Proyecto = proyecto;
            nuevoreporte.Utilidad = (proyecto.Precio - utilidad);
            db.ReporteProyectos.Add(nuevoreporte);
            db.SaveChanges();
            return RedirectToAction("~/ReporteProyectoes/Index");
        }
        public void ExportExcelProyectos()
        {
            var grid = new GridView();
            var reporte = db.Proyectos.ToList();
            grid.DataSource = from data in reporte
                              select new
                              {
                                  Nombre = data.Nombre,
                                  Estado = data.Estado.Nombre,
                                  Precio = data.Precio,
                                  Empresa = data.Empresa.NombreEmpresa
                              };
            grid.DataBind();
            Response.ClearContent();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=excelTest.xls");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htmlwriter = new HtmlTextWriter(sw);
            grid.RenderControl(htmlwriter);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
