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
    public class FaseProyectosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FaseProyectos
        public ActionResult Index()
        {
            return View(db.FaseProyectos.ToList());
        }

        // GET: FaseProyectos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseProyecto faseProyecto = db.FaseProyectos.Find(id);
            if (faseProyecto == null)
            {
                return HttpNotFound();
            }
            return View(faseProyecto);
        }

        // GET: FaseProyectos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FaseProyectos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NombreFase,DescripcionFase")] FaseProyecto faseProyecto)
        {
            if (ModelState.IsValid)
            {
                db.FaseProyectos.Add(faseProyecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(faseProyecto);
        }

        // GET: FaseProyectos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseProyecto faseProyecto = db.FaseProyectos.Find(id);
            if (faseProyecto == null)
            {
                return HttpNotFound();
            }
            return View(faseProyecto);
        }

        // POST: FaseProyectos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NombreFase,DescripcionFase")] FaseProyecto faseProyecto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faseProyecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faseProyecto);
        }

        // GET: FaseProyectos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseProyecto faseProyecto = db.FaseProyectos.Find(id);
            if (faseProyecto == null)
            {
                return HttpNotFound();
            }
            return View(faseProyecto);
        }

        // POST: FaseProyectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FaseProyecto faseProyecto = db.FaseProyectos.Find(id);
            db.FaseProyectos.Remove(faseProyecto);
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
