using Consultoresvs3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult ActualizarUsuario(int? identidad,double salario,double valorhorasp,double valorhorasnp)
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

    }
}