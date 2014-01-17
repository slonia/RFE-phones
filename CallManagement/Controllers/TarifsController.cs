﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallManagement.Models;

namespace CallManagement.Controllers
{
    public class TarifsController : Controller
    {
        private PhoneDBContext db = new PhoneDBContext();

        //
        // GET: /Tarifs/

        public ActionResult Index()
        {
            return View(db.Tarifs.ToList());
        }

        //
        // GET: /Tarifs/Details/5

        public ActionResult Details(int id = 0)
        {
            Tarif tarif = db.Tarifs.Find(id);
            if (tarif == null)
            {
                return HttpNotFound();
            }
            return View(tarif);
        }

        //
        // GET: /Tarifs/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tarifs/Create

        [HttpPost]
        public ActionResult Create(Tarif tarif)
        {
            if (ModelState.IsValid)
            {
                db.Tarifs.Add(tarif);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tarif);
        }

        //
        // GET: /Tarifs/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Tarif tarif = db.Tarifs.Find(id);
            if (tarif == null)
            {
                return HttpNotFound();
            }
            return View(tarif);
        }

        //
        // POST: /Tarifs/Edit/5

        [HttpPost]
        public ActionResult Edit(Tarif tarif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tarif);
        }

        //
        // GET: /Tarifs/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Tarif tarif = db.Tarifs.Find(id);
            if (tarif == null)
            {
                return HttpNotFound();
            }
            return View(tarif);
        }

        //
        // POST: /Tarifs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tarif tarif = db.Tarifs.Find(id);
            db.Tarifs.Remove(tarif);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}