using Consultoresvs3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consultoresvs3.Controllers
{
    public class AccionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Acciones
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReporteProyecto(int proyectoId)
        {
            int horasTrabajadas = 0;
            List<UsuarioProyecto> listamatriculas = db.UsuarioProyectos.Where(t => t.IdProyecto == proyectoId).ToList();
            for (int i = 0; i < listamatriculas.Count; i++)
            {
                var reportesproyectos = db.ReporteUsuarios.Where(t => t.Id == listamatriculas[0].Id).ToList();
                for (int j = 0; j < reportesproyectos.Count; j++)
                {
                    horasTrabajadas = reportesproyectos[j].HTrabajadas;
                }
            }
            ReporteProyecto reporte = new ReporteProyecto();
            reporte.HorasInvertidas = horasTrabajadas;
            reporte.IdProyecto = proyectoId;
            Proyecto proyecto = db.Proyectos.Find(proyectoId);
            reporte.Proyecto = proyecto;
            reporte.Utilidad = proyecto.TiempoEstipulado - horasTrabajadas;
            db.ReporteProyectos.Add(reporte);
            db.SaveChanges();
            return Index();
        }
    }
}