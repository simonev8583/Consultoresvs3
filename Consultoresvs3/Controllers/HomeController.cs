using Consultoresvs3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Consultoresvs3.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult ActualizarUsuario()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ActualizarUsuario(int? identidad, decimal salario, decimal valorhorasp, decimal valorhorasnp)
        {
            if (identidad == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var users = db.Users.Where(t => t.Identificacion == identidad).ToList();
            if (users.Count() == 0)
            {
                return PartialView("_UsuarioNoActualizado");
            }
            else
            {
                var user = db.Users.Where(t => t.Identificacion == identidad).First();
                if (user == null)
                {
                    return HttpNotFound();
                }
                db.Users.Where(t => t.Identificacion == identidad).First().Salario = salario;

                db.Users.Where(t => t.Identificacion == identidad).First().ValorHoraPrestacionesSociales = valorhorasp;

                db.Users.Where(t => t.Identificacion == identidad).First().ValorHoraNoPrestacionSociales = valorhorasnp;

                db.SaveChanges();


                return PartialView("_UsuarioActualizado");
            }
        }
        [HttpGet]
        [Authorize]
        public ActionResult ReporteUsuario()
        {
            return View();
        }

        public List<ReporteUsuario> reportesusuario(int identidad)
        {
            return db.ReporteUsuarios.Where(t => t.Usuario.Identificacion == identidad).ToList();
        }

        [HttpPost]
        public ActionResult ReporteExcel(int identidad)
        {
            var grid = new GridView();
            var reporte = reportesusuario(identidad);
            grid.DataSource = from data in reporte
                              select new
                              {
                                  Identidad = data.Usuario.Identificacion,
                                  Usuario = data.Usuario.Nombre,
                                  UsuarioApellido = data.Usuario.Apellido,
                                  UsuarioCargo = data.Usuario.Cargo,
                                  Proyecto = data.Proyecto.Nombre,
                                  Proyectofechai = data.Proyecto.Fecha,
                                  Proyectofechafin = data.Proyecto.FechaFin,
                                  TiempoEstipulado = data.Proyecto.TiempoEstipulado,
                                  HorasTrabajdas = data.HTrabajadas
                              };
            grid.DataBind();
            Response.ClearContent();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=ReporteUsuario.xls");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htmlwriter = new HtmlTextWriter(sw);
            grid.RenderControl(htmlwriter);
            Response.Write(sw.ToString());
            Response.End();
            return PartialView("_Exportado");
        }
    }
}