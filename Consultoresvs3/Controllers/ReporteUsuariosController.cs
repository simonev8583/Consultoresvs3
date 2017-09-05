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
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

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

            if (User.IsInRole("ADMIN"))
            {
                ViewBag.idEmpresa = new SelectList(db.Empresas, "Id", "NombreEmpresa");            
                // traer a todos los usuarios, menos al administrador
                ViewBag.UsuarioId = new SelectList(from u in db.Users
                                                   where u.Id != idusuario
                                                   select new { Id = u.Id, Nombre = u.Nombre + " " + u.Apellido }, "Id", "Nombre");
                ViewBag.idProyecto = new SelectList(db.Proyectos, "Id", "Nombre");
                var reporte_Empleados= db.ReporteUsuarios.Include(r => r.Usuario).Include(r => r.Proyecto).Include(r => r.Servicio);
                return View(reporte_Empleados.ToList().OrderByDescending(r => r.FechaReporte).OrderByDescending(r => r.IdProyecto));
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
                    return View(reporte_Empleados.ToList().OrderByDescending(r => r.FechaReporte).OrderByDescending(r => r.IdProyecto));
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

            ViewBag.IdProyecto = new SelectList(db.Proyectos.Where(r => r.Estado.Nombre.ToUpper() != "FINALIZADO"), "Id", "Nombre");
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
        // DESACOPLE FILTROS 
        //DF => Desacople Filtro ...

        // Consulta en la base de datos los reportes generados por el empleado logeado
        // actualmente y trae los reportes de un proyecto especifico
        public dynamic DFProyectoEmp(int? idProyecto)
        {
            string idusuario = User.Identity.GetUserId();
            var Reporte = db.ReporteUsuarios.Where(r => r.IdUsuario.Equals(idusuario) && r.IdProyecto == idProyecto).Include(r => r.Servicio)
                .ToList().OrderByDescending(r => r.FechaReporte).OrderByDescending(r => r.IdUsuario);
            return Reporte;
        }
        // Consulta en la base de datos los reportes generados 
        // trae  todos los reportes de un proyecto especifico
        public dynamic DFProyectoAdm(int? idProyecto) {
            var Reporte = db.ReporteUsuarios.Where(r => r.IdProyecto == idProyecto).Include(r => r.Usuario).Include(r => r.Proyecto).Include(r => r.Servicio)
                .ToList().OrderByDescending(r => r.FechaReporte).OrderByDescending(r => r.IdUsuario);
            return Reporte;
        }
        // Consulta en la base de datos los reportes generados en un mes especifico
        //  del empleado logeado actualmente
        public dynamic DFReporteUFechaEmp(int? mes, int?año)
        {
            string idusuario = User.Identity.GetUserId();
            var Reporte = db.ReporteUsuarios.Where(r => r.FechaReporte.Month == mes && r.FechaReporte.Year == año && r.IdUsuario.Equals(idusuario))
                .Include(r => r.Proyecto).Include(r => r.Servicio)
                .ToList().OrderByDescending(r => r.FechaReporte).OrderByDescending(r => r.Proyecto.Empresa.Id);
            return Reporte;
        }
        // Consulta en la base de datos los reportes generados en un mes especifico
        public dynamic DFReporteUFechaAdm(int? mes, int? año)
        {
            var Reporte = db.ReporteUsuarios.Where(r => r.FechaReporte.Month == mes && r.FechaReporte.Year == año).Include(r => r.Usuario)
                .Include(r => r.Proyecto).Include(r => r.Servicio)
                .ToList().OrderByDescending(r => r.FechaReporte).OrderByDescending(r => r.Proyecto.Empresa.Id);
            return Reporte;
        }
        // Consulta en la base de datos los reportes de todos los proyectos que perteneces 
        // a una empresa en especifico
        public dynamic DFiltroEmpresaAdm(int? idEmpresa)
        {
            var Reporte = db.ReporteUsuarios.Where(r => r.Proyecto.IdEmpresa == idEmpresa).Include(r => r.Proyecto).Include(r => r.Usuario).Include(r => r.Proyecto.Empresa).Include(r => r.Servicio)
               .ToList().OrderByDescending(r => r.FechaReporte).OrderByDescending(r => r.Proyecto.Id);
            return Reporte;
        }
        // Consulta en la base de datos los reportes generados por 
        // un empleado en especifico 
        public dynamic DFiltroEmpleadoAdm(string UsuarioId)
        {
            var Reporte = db.ReporteUsuarios.Where(r => r.IdUsuario.Equals(UsuarioId)).Include(r => r.Proyecto).Include(r => r.Servicio).Include(r => r.Proyecto.Empresa)
                .ToList().OrderByDescending(r => r.FechaReporte).OrderByDescending(r => r.IdProyecto);
            return Reporte;
        }
        // FILTROS DE BUSQUEDA 
        // Queremos mostrar la informacion que tiene cada empleado según un proyecto en especial.

         // Retorna los resultados de cada consulta en la vista, y muestra los reportes 
        [HttpPost]
        public ActionResult FiltroProyectoEmp(int? idProyecto)
        {
            var Reporte = DFProyectoEmp(idProyecto);
            ViewBag.ProyectoId = db.Proyectos.Find(idProyecto);
            return PartialView("_FiltroProyectoemp", Reporte);
        }
        [HttpPost]
        public ActionResult FiltroProyectoAdm(int ? idProyecto)
        {
            var Reporte = DFProyectoAdm(idProyecto);
            ViewBag.ProyectoId = db.Proyectos.Find(idProyecto);
            return PartialView("_FiltroProyectoadm", Reporte);
        }
        [HttpPost]
        public ActionResult FiltroReporteUFechaEmp(int? mes,int? año)
        {
            var Reporte = DFReporteUFechaEmp(mes, año);
            return PartialView("_FiltroReporteUFechaemp", Reporte);
        }
        [HttpPost]
        public ActionResult FiltroReporteUFechaAdm(int? mes, int? año)
        {
            var Reporte = DFReporteUFechaAdm(mes, año);
            return PartialView("_FiltroReporteUFechaadm", Reporte);
        }
        [HttpPost]
        public ActionResult FiltroEmpresaAdm(int? idEmpresa)
        {            
            var Reporte = DFiltroEmpresaAdm(idEmpresa);
            return PartialView("_FiltroEmpresaadm", Reporte);
        }
        [HttpPost]
        public ActionResult FiltroEmpleadoAdm(string UsuarioId)
        {
            ViewBag.UsuarioId =db.Users.Find(UsuarioId);
            var Reporte = DFiltroEmpleadoAdm(UsuarioId);
            return PartialView("_FiltroReporteEmpleadoeadm", Reporte);
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
            double utilidad = 0;
            int horastrabajadas = 0;
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
            nuevoreporte.Utilidad = proyecto.Precio - utilidad;
            db.ReporteProyectos.Add(nuevoreporte);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void ExportExcel()
        {
            var grid = new GridView();
            var reporte = db.ReporteUsuarios.ToList();
            grid.DataSource = from data in reporte
                              select new
                              {
                                  Usuario = data.Usuario.Nombre,
                                  Identidad = data.Usuario.Identificacion,
                                  Proyecto = data.Proyecto.Nombre,
                                  HorasTrabajdas = data.HTrabajadas
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
