using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Consultoresvs3.Models;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Consultoresvs3.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ReporteProyectoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReporteProyectoes
        [Authorize]
        public ActionResult Index()
        {
            var reporteProyectos = db.ReporteProyectos.Include(r => r.Proyecto);
            return View(reporteProyectos.ToList());
        }

        // GET: ReporteProyectoes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteProyecto reporteProyecto = db.ReporteProyectos.Find(id);
            if (reporteProyecto == null)
            {
                return HttpNotFound();
            }
            return View(reporteProyecto);
        }

        // GET: ReporteProyectoes/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre");
            return View();
        }

        // POST: ReporteProyectoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdProyecto,HorasInvertidas,Utilidad")] ReporteProyecto reporteProyecto)
        {
            if (ModelState.IsValid)
            {
                db.ReporteProyectos.Add(reporteProyecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", reporteProyecto.IdProyecto);
            return View(reporteProyecto);
        }

        // GET: ReporteProyectoes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteProyecto reporteProyecto = db.ReporteProyectos.Find(id);
            if (reporteProyecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", reporteProyecto.IdProyecto);
            return View(reporteProyecto);
        }

        // POST: ReporteProyectoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdProyecto,HorasInvertidas,Utilidad")] ReporteProyecto reporteProyecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reporteProyecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", reporteProyecto.IdProyecto);
            return View(reporteProyecto);
        }

        // GET: ReporteProyectoes/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteProyecto reporteProyecto = db.ReporteProyectos.Find(id);
            if (reporteProyecto == null)
            {
                return HttpNotFound();
            }
            return View(reporteProyecto);
        }

        // POST: ReporteProyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReporteProyecto reporteProyecto = db.ReporteProyectos.Find(id);
            db.ReporteProyectos.Remove(reporteProyecto);
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
        public void ExportExcel()
        {
            var grid = new GridView();
            var reporte = db.ReporteProyectos.ToList();
            grid.DataSource = from data in reporte
                              select new
                              {
                                  NITEmpresa = data.Proyecto.Empresa.NIT,
                                  Empresa = data.Proyecto.Empresa.NombreEmpresa,
                                  Proyecto = data.Proyecto.Nombre,
                                  FechaI = data.Proyecto.Fecha,
                                  FechaFin = data.Proyecto.FechaFin,
                                  TiempoEstipulado = data.Proyecto.TiempoEstipulado,
                                  HorasInvertidas = data.HorasInvertidas,
                                  Utilidad = data.Utilidad
                              };
            grid.DataBind();
            Response.ClearContent();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=ReporteProyectos.xls");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htmlwriter = new HtmlTextWriter(sw);
            grid.RenderControl(htmlwriter);
            Response.Write(sw.ToString());
            Response.End();
        }
    }
}
