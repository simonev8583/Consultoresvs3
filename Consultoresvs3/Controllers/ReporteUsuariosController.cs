using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Consultoresvs3.Models;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

namespace Consultoresvs3.Controllers
{
    [Authorize]
    public class ReporteUsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReporteUsuarios
        [Authorize]
        public ActionResult Index()
        {
            string idusuario = User.Identity.GetUserId();

            // Es para el dropdown del empleado actual (debe traer los proyectos en los que se encuentra).... lo hace pero...
            // Los esta trayendo con reportes asi que si genera un reporte 20 veces de la misma empresa va a poner 20 de esos
            // cuadrar si se le ocurre algo....
           ViewBag.idProyecto = new SelectList(db.ReporteUsuarios.Where(r => r.IdUsuario.Equals(idusuario)), "Proyecto.Id", "Proyecto.Nombre");
            var reporte_Empleados = db.ReporteUsuarios.Where(r => r.IdUsuario.Equals(idusuario)).Include(r => r.Usuario).Include(r => r.Proyecto).Include(r => r.Servicio);
            return View(reporte_Empleados.ToList().OrderByDescending(r => r.FechaReporte));
            
        }

        // GET: ReporteUsuarios/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteUsuario reporteUsuario = db.ReporteUsuarios.Find(id);
            if (reporteUsuario == null)
            {
                return HttpNotFound();
            }
            return View(reporteUsuario);
        }

        // GET: ReporteUsuarios/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre");
            ViewBag.IdServicio = new SelectList(db.Servicios, "Id", "Nombre");
            return View();
        }

        // POST: ReporteUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaReporte,HTrabajadas,IdServicio,IdProyecto,IdUsuario")] ReporteUsuario reporteUsuario)
        {
            if (ModelState.IsValid)
            {
                reporteUsuario.IdUsuario=User.Identity.GetUserId();
                db.ReporteUsuarios.Add(reporteUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", reporteUsuario.IdProyecto);
            ViewBag.IdServicio = new SelectList(db.Servicios, "Id", "Nombre", reporteUsuario.IdServicio);
            return View(reporteUsuario);
        }

        // GET: ReporteUsuarios/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteUsuario reporteUsuario = db.ReporteUsuarios.Find(id);
            if (reporteUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", reporteUsuario.IdProyecto);
            ViewBag.IdServicio = new SelectList(db.Servicios, "Id", "Nombre", reporteUsuario.IdServicio);
            ViewBag.IdUsuario = new SelectList(db.Users, "Id", "Nombre", reporteUsuario.IdUsuario);
            return View(reporteUsuario);
        }

        // POST: ReporteUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaReporte,HTrabajadas,IdServicio,IdProyecto,IdUsuario")] ReporteUsuario reporteUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reporteUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", reporteUsuario.IdProyecto);
            ViewBag.IdServicio = new SelectList(db.Servicios, "Id", "Nombre", reporteUsuario.IdServicio);
            ViewBag.IdUsuario = new SelectList(db.Users, "Id", "Nombre", reporteUsuario.IdUsuario);
            return View(reporteUsuario);
        }

        // GET: ReporteUsuarios/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteUsuario reporteUsuario = db.ReporteUsuarios.Find(id);
            if (reporteUsuario == null)
            {
                return HttpNotFound();
            }
            return View(reporteUsuario);
        }

        // POST: ReporteUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReporteUsuario reporteUsuario = db.ReporteUsuarios.Find(id);
            db.ReporteUsuarios.Remove(reporteUsuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Queremos mostrar la informacion que tiene cada empleado según un proyecto en especial.
        [HttpPost]
        public ActionResult FiltroProyectoEmp(int? idProyecto)
        {
            string idusuario = User.Identity.GetUserId();
            //var Reporte = db.ReporteUsuarios.Where(r=> r.IdUsuario.Equals(idusuario) && r.IdProyecto.Equals(idProyecto)).Include(r=>r.Servicio);
            var Reporte = db.ReporteUsuarios.Where(r => r.IdUsuario.Equals(idusuario) && r.IdProyecto == idProyecto).Include(r=>r.Servicio);
            //ViewBag.ProyectoId = db.Proyectos.Find(idProyecto);
            ViewBag.ProyectoId = db.Proyectos.Find(idProyecto);
            return PartialView("_FiltroProyectoemp", Reporte);
        }
        public ActionResult FiltroProyectoAdm(int ? idProyecto)
        {
            var Reporte =db.ReporteUsuarios.Where(r=> r.IdProyecto ==1).Include(r => r.Usuario).Include(r => r.Proyecto).Include(r => r.Servicio);
            //var Reporte = db.ReporteUsuarios.Where(r => r.IdProyecto == idProyecto).Include(r => r.Usuario).Include(r => r.Proyecto).Include(r => r.Servicio);
            //ViewBag.ProyectoId = db.Proyectos.Find(idProyecto);
            ViewBag.ProyectoId = db.Proyectos.Find(1);
            return PartialView("_FiltroProyectoadm", Reporte);
        }
        public ActionResult FiltroReporteUFechaEmp(int? mes,int? año)
        {
            string idusuario = User.Identity.GetUserId();
            // var Reporte = db.ReporteUsuarios.Where(r => r.FechaReporte.Month.Equals(mes) && r.FechaReporte.Year.Equals(año) && r.IdUsuario.Equals(idusuario));
            var Reporte = db.ReporteUsuarios.Where(r => r.FechaReporte.Month == 08 && r.FechaReporte.Year ==2017 && r.IdUsuario.Equals(idusuario))
                .Include(r => r.Proyecto).Include(r => r.Servicio);
            return PartialView("_FiltroReporteUFechaemp", Reporte);
        }
        public ActionResult FiltroReporteUFechaAdm(int? mes, int? año)
        {
            // var Reporte = db.ReporteUsuarios.Where(r => r.FechaReporte.Month.Equals(mes) && r.FechaReporte.Year.Equals(año));
            var Reporte = db.ReporteUsuarios.Where(r => r.FechaReporte.Month.Equals(08) && r.FechaReporte.Year.Equals(2017)).Include(r => r.Usuario)
                .Include(r => r.Proyecto).Include(r => r.Servicio);
            return PartialView("_FiltroReporteUFechaadm", Reporte);
        }
        public ActionResult FiltroReporteServicioEmp(int ? idservicio)
        {
            string idusuario = User.Identity.GetUserId();
            //var Reporte = db.ReporteUsuarios.Where(r => r.IdServicio == idservicio && r.IdUsuario.Equals(idusuario)).Include(r => r.Proyecto);
            var Reporte = db.ReporteUsuarios.Where(r => r.IdServicio == 1 && r.IdUsuario.Equals(idusuario)).Include(r => r.Proyecto.Empresa);
            return PartialView("_FiltroReporteServicioemp", Reporte);
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
