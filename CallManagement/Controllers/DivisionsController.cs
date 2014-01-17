using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallManagement.Models;

namespace CallManagement.Controllers
{
    public class DivisionsController : Controller
    {
        private PhoneDBContext db = new PhoneDBContext();

        //
        // GET: /Divisions/

        public ActionResult Index()
        {
            return View(db.Divisions.Include(p=> p.Parent).ToList());
        }

        //
        // GET: /Divisions/Details/5

        public ActionResult Details(int id = 0)
        {
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        //
        // GET: /Divisions/Create

        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.Divisions, "DivisionId", "Name");
            return View();
        }

        //
        // POST: /Divisions/Create

        [HttpPost]
        public ActionResult Create(Division division)
        {
            if (ModelState.IsValid)
            {
                db.Divisions.Add(division);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Divisions, "DivisionId", "Name", division.ParentId);
            return View(division);
        }

        //
        // GET: /Divisions/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.Divisions, "DivisionId", "Name", division.ParentId);
            return View(division);
        }

        //
        // POST: /Divisions/Edit/5

        [HttpPost]
        public ActionResult Edit(Division division)
        {
            if (ModelState.IsValid)
            {
                db.Entry(division).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Divisions, "DivisionId", "Name", division.ParentId);
            return View(division);
        }

        //
        // GET: /Divisions/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Division division = db.Divisions.Find(id);
            if (division == null)
            {
                return HttpNotFound();
            }
            return View(division);
        }

        //
        // POST: /Divisions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Division division = db.Divisions.Find(id);
            db.Divisions.Remove(division);
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