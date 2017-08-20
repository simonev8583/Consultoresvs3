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
    public class EstadoProyectosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EstadoProyectos
        [Authorize]
        public ActionResult Index()
        {
            return View(db.EstadoProyectos.ToList());
        }

        // GET: EstadoProyectos/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoProyecto estadoProyecto = db.EstadoProyectos.Find(id);
            if (estadoProyecto == null)
            {
                return HttpNotFound();
            }
            return View(estadoProyecto);
        }

        // GET: EstadoProyectos/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoProyectos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre")] EstadoProyecto estadoProyecto)
        {
            if (ModelState.IsValid)
            {
                db.EstadoProyectos.Add(estadoProyecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(estadoProyecto);
        }

        // GET: EstadoProyectos/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoProyecto estadoProyecto = db.EstadoProyectos.Find(id);
            if (estadoProyecto == null)
            {
                return HttpNotFound();
            }
            return View(estadoProyecto);
        }

        // POST: EstadoProyectos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] EstadoProyecto estadoProyecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoProyecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estadoProyecto);
        }

        // GET: EstadoProyectos/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoProyecto estadoProyecto = db.EstadoProyectos.Find(id);
            if (estadoProyecto == null)
            {
                return HttpNotFound();
            }
            return View(estadoProyecto);
        }

        // POST: EstadoProyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoProyecto estadoProyecto = db.EstadoProyectos.Find(id);
            db.EstadoProyectos.Remove(estadoProyecto);
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
    }
}
