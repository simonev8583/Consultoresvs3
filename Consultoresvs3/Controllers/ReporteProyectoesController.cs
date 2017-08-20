using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Consultoresvs3.Models;

namespace Consultoresvs3.Controllers
{
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

        public ActionResult CrearReporte(int id)
        {
            var reporte = db.ReporteUsuarios.Where(t => t.Proyecto.Id == id).ToList();
            int horastrabajadas = 0;
            for (int i = 0; i < reporte.Count; i++)
            {
                horastrabajadas += reporte[i].HTrabajadas;
            }
            ReporteProyecto nuevoreporte = new ReporteProyecto();
            nuevoreporte.HorasInvertidas = horastrabajadas;
            Proyecto proyecto = db.Proyectos.Find(id);
            nuevoreporte.IdProyecto = proyecto.Id;
            nuevoreporte.Proyecto = proyecto;
            nuevoreporte.Utilidad = (proyecto.TiempoEstipulado - horastrabajadas);
            db.ReporteProyectos.Add(nuevoreporte);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
