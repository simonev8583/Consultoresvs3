﻿using System;
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
    public class ReporteUsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReporteUsuarios
        public ActionResult Index()
        {
            var reporteUsuarios = db.ReporteUsuarios.Include(r => r.Proyecto).Include(r => r.Servicio).Include(r => r.Usuario);
            return View(reporteUsuarios.ToList());
        }

        // GET: ReporteUsuarios/Details/5
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
        public ActionResult Create()
        {
            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre");
            ViewBag.IdServicio = new SelectList(db.Servicios, "Id", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.Users, "Id", "Nombre");
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
                db.ReporteUsuarios.Add(reporteUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProyecto = new SelectList(db.Proyectos, "Id", "Nombre", reporteUsuario.IdProyecto);
            ViewBag.IdServicio = new SelectList(db.Servicios, "Id", "Nombre", reporteUsuario.IdServicio);
            ViewBag.IdUsuario = new SelectList(db.Users, "Id", "Nombre", reporteUsuario.IdUsuario);
            return View(reporteUsuario);
        }

        // GET: ReporteUsuarios/Edit/5
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
