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

            // mostrar a los empleados los proyectos en ejecucion, NO FINALIZADOS
            if (User.IsInRole("ADMIN"))
            {
                var rolAdmin = db.Roles.Where(r => r.Name.Equals("ADMIN"));
                ViewBag.idEmpresa = new SelectList(db.Empresas, "Id", "NombreEmpresa");

                // traer a todos los usuarios, menos al administrador

                //var usuarios= 

                ViewBag.UsuarioId = new SelectList(db.Users, "Id", "Nombre");
                ViewBag.idProyecto = new SelectList(db.Proyectos, "Id", "Nombre");
                var reporte_Empleados= db.ReporteUsuarios.Include(r => r.Usuario).Include(r => r.Proyecto).Include(r => r.Servicio);
                return View(reporte_Empleados.ToList().OrderByDescending(r => r.FechaReporte));
            }
            else
            {
                // En este ViewBag van los proyectos en los que se encuentra el empleado logeado actualmente
                if (db.ReporteUsuarios.Where(r=>r.IdUsuario.Equals(idusuario)).Count()<=0)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    ViewBag.idProyecto = new SelectList(from p in db.Proyectos
                                                        join up in db.ReporteUsuarios
                                                        on p.Id equals up.IdProyecto
                                                        where p.Id == up.IdProyecto
                                                        where up.IdUsuario == idusuario
                                                        group p by new { p.Id, p.Nombre } into proyectos
                                                        select new { proyectos.Key.Id, proyectos.Key.Nombre }, "Id", "Nombre");
                    var reporte_Empleados = db.ReporteUsuarios.Where(r => r.IdUsuario.Equals(idusuario)).Include(r => r.Usuario).Include(r => r.Proyecto).Include(r => r.Servicio);
                    return View(reporte_Empleados.ToList().OrderByDescending(r => r.FechaReporte));
                }
                
            }
           
            
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
            ViewBag.IdProyecto = new SelectList(db.Proyectos.Where(r => r.Estado.Nombre.ToUpper() != "FINALIZADO"), "Id", "Nombre");
            //ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre");
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

            /////OJO
            /// OJO PROBRAR IdProyecto (CREATE)
            ViewBag.IdProyecto = new SelectList(db.Proyectos.Where(r => r.Estado.Nombre.ToUpper() != "FINALIZADO"), "Id", "Nombre");
           // ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", reporteUsuario.IdProyecto);
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
            var Reporte = db.ReporteUsuarios.Where(r => r.IdUsuario.Equals(idusuario) && r.IdProyecto == idProyecto).Include(r=>r.Servicio);
            ViewBag.ProyectoId = db.Proyectos.Find(idProyecto);
            return PartialView("_FiltroProyectoemp", Reporte.ToList().OrderByDescending(r => r.FechaReporte));
        }
        public ActionResult FiltroProyectoAdm(int ? idProyecto)
        {
            var Reporte =db.ReporteUsuarios.Where(r=> r.IdProyecto == idProyecto).Include(r => r.Usuario).Include(r => r.Proyecto).Include(r => r.Servicio);
            ViewBag.ProyectoId = db.Proyectos.Find(idProyecto);
            return PartialView("_FiltroProyectoadm", Reporte.ToList().OrderByDescending(r => r.FechaReporte));
        }
        public ActionResult FiltroReporteUFechaEmp(int? mes,int? año)
        {
            string idusuario = User.Identity.GetUserId();
            var Reporte = db.ReporteUsuarios.Where(r => r.FechaReporte.Month == mes && r.FechaReporte.Year == año && r.IdUsuario.Equals(idusuario))
                .Include(r => r.Proyecto).Include(r => r.Servicio);
            return PartialView("_FiltroReporteUFechaemp", Reporte.ToList().OrderByDescending(r => r.FechaReporte));
        }
        public ActionResult FiltroReporteUFechaAdm(int? mes, int? año)
        {
            var Reporte = db.ReporteUsuarios.Where(r => r.FechaReporte.Month == mes && r.FechaReporte.Year == año).Include(r => r.Usuario)
                .Include(r => r.Proyecto).Include(r => r.Servicio);
            return PartialView("_FiltroReporteUFechaadm", Reporte.ToList().OrderByDescending(r => r.FechaReporte));
        }     
        public ActionResult FiltroEmpresaAdm(int? idempresa)
        {            
            var Reporte = db.ReporteUsuarios.Where(r => r.Proyecto.IdEmpresa == idempresa).Include(r => r.Proyecto).Include(r => r.Usuario).Include(r => r.Proyecto.Empresa).Include(r => r.Servicio);
            return PartialView("_FiltroEmpresaadm", Reporte.ToList().OrderByDescending(r => r.FechaReporte));
        }
        public ActionResult FiltroEmpleadoAdm(string idusuario)
        {
            ViewBag.UsuarioId = db.Users.Find(idusuario);
            var Reporte = db.ReporteUsuarios.Where(r => r.IdUsuario.Equals(idusuario)).Include(r => r.Proyecto).Include(r => r.Servicio).Include(r => r.Proyecto.Empresa);
            return PartialView("_FiltroReporteEmpleadoeadm", Reporte.ToList().OrderByDescending(r => r.FechaReporte));
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
