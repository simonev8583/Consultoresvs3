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
    public class UsuarioProyectosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UsuarioProyectos
        public ActionResult Index()
        {
            var usuarioProyectos = db.UsuarioProyectos.Include(u => u.Proyecto).Include(u => u.Usuario);
            return View(usuarioProyectos.ToList());
        }

        // GET: UsuarioProyectos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioProyecto usuarioProyecto = db.UsuarioProyectos.Find(id);
            if (usuarioProyecto == null)
            {
                return HttpNotFound();
            }
            return View(usuarioProyecto);
        }

        // GET: UsuarioProyectos/Create
        public ActionResult Create()
        {
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.ApplicationUsers, "Id", "Nombre");
            return View();
        }

        // POST: UsuarioProyectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdProyecto,IdUsuario")] UsuarioProyecto usuarioProyecto)
        {
            if (ModelState.IsValid)
            {
                db.UsuarioProyectos.Add(usuarioProyecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", usuarioProyecto.IdProyecto);
            ViewBag.IdUsuario = new SelectList(db.ApplicationUsers, "Id", "Nombre", usuarioProyecto.IdUsuario);
            return View(usuarioProyecto);
        }

        // GET: UsuarioProyectos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioProyecto usuarioProyecto = db.UsuarioProyectos.Find(id);
            if (usuarioProyecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", usuarioProyecto.IdProyecto);
            ViewBag.IdUsuario = new SelectList(db.ApplicationUsers, "Id", "Nombre", usuarioProyecto.IdUsuario);
            return View(usuarioProyecto);
        }

        // POST: UsuarioProyectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdProyecto,IdUsuario")] UsuarioProyecto usuarioProyecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioProyecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", usuarioProyecto.IdProyecto);
            ViewBag.IdUsuario = new SelectList(db.ApplicationUsers, "Id", "Nombre", usuarioProyecto.IdUsuario);
            return View(usuarioProyecto);
        }

        // GET: UsuarioProyectos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioProyecto usuarioProyecto = db.UsuarioProyectos.Find(id);
            if (usuarioProyecto == null)
            {
                return HttpNotFound();
            }
            return View(usuarioProyecto);
        }

        // POST: UsuarioProyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioProyecto usuarioProyecto = db.UsuarioProyectos.Find(id);
            db.UsuarioProyectos.Remove(usuarioProyecto);
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
