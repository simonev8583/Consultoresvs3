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
    public class ReporteProyectosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReporteProyectos
        public ActionResult Index()
        {
            var reporteProyectos = db.ReporteProyectos.Include(r => r.Proyecto);
            return View(reporteProyectos.ToList());
        }

        // GET: ReporteProyectos/Details/5
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

        // GET: ReporteProyectos/Create
        public ActionResult Create()
        {
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre");
            return View();
        }

        // POST: ReporteProyectos/Create
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

        // GET: ReporteProyectos/Edit/5
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

        // POST: ReporteProyectos/Edit/5
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
        // GET: ReporteProyectos/Delete/5
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

        // POST: ReporteProyectos/Delete/5
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
        // Se crea el reporte de proyecto 
        public ActionResult ReporteProyecto(int id)
        {
            int horasTrabajadas = 0;
            var listamatriculas = db.UsuarioProyectos.Where(t => t.IdProyecto == id).ToList();
            for (int i = 0; i < listamatriculas.Count; i++)
            {
                var l = listamatriculas[i];
                var reportesproyectos = db.ReporteUsuarios.Where(t => t.IdUsuarioProyecto == l.Id).ToList();
                for (int j = 0; j < reportesproyectos.Count; j++)
                {
                    horasTrabajadas += reportesproyectos[j].HTrabajadas;
                }
            }
            ReporteProyecto reporte = new ReporteProyecto();
            reporte.HorasInvertidas = horasTrabajadas;
            Proyecto proyecto = db.Proyectos.Find(id);
            reporte.IdProyecto = proyecto.Id;
            reporte.Proyecto = proyecto;
            reporte.Utilidad = proyecto.TiempoEstipulado - horasTrabajadas;
            db.ReporteProyectos.Add(reporte);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
