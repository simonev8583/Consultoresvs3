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
    public class EmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Empresas
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Empresas.ToList());
        }

        // GET: Empresas/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NIT,NombreEmpresa,Direccion,CorreoEmpresa,ActividadEconomica,Telefono,NombreRepLegal,IdentificacionRepLegal,NombreRepSuplente,IdentificacionRepSuplente,NombreJuntaDir1,IdentificacionJuntaDir1,NombreJuntaDir2,IdentificacionJuntaDir2,NombreJuntaDir3,IdentificacionJuntaDir3,NombreJuntaDir4,IdentificacionJuntaDir4,NombreJuntaDir5,IdentificacionJuntaDir5,NombreJuntaDir6,IdentificacionJuntaDir6,NombreJuntaDir7,IdentificacionJuntaDir7,NombreJuntaDir8,IdentificacionJuntaDir8,NombreJuntaDir9,IdentificacionJuntaDir9,NombreJuntaDir10,IdentificacionJuntaDir10")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Empresas.Add(empresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empresa);
        }

        // GET: Empresas/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NIT,NombreEmpresa,Direccion,CorreoEmpresa,ActividadEconomica,Telefono,NombreRepLegal,IdentificacionRepLegal,NombreRepSuplente,IdentificacionRepSuplente,NombreJuntaDir1,IdentificacionJuntaDir1,NombreJuntaDir2,IdentificacionJuntaDir2,NombreJuntaDir3,IdentificacionJuntaDir3,NombreJuntaDir4,IdentificacionJuntaDir4,NombreJuntaDir5,IdentificacionJuntaDir5,NombreJuntaDir6,IdentificacionJuntaDir6,NombreJuntaDir7,IdentificacionJuntaDir7,NombreJuntaDir8,IdentificacionJuntaDir8,NombreJuntaDir9,IdentificacionJuntaDir9,NombreJuntaDir10,IdentificacionJuntaDir10")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = db.Empresas.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empresa empresa = db.Empresas.Find(id);
            db.Empresas.Remove(empresa);
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
