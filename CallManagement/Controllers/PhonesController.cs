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
    public class PhonesController : Controller
    {
        private PhoneDBContext db = new PhoneDBContext();

        //
        // GET: /Phones/

        public ActionResult Index()
        {
            var phones = db.Phones.Include(p => p.Division).Include(p => p.User).Include(p => p.Tarif);
            return View(phones.ToList());
        }

        //
        // GET: /Phones/Details/5

        public ActionResult Details(int id = 0)
        {
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        //
        // GET: /Phones/Create

        public ActionResult Create()
        {
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            ViewBag.TarifId = new SelectList(db.Tarifs, "TarifId", "FullName");
            return View();
        }

        //
        // POST: /Phones/Create

        [HttpPost]
        public ActionResult Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phones.Add(phone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", phone.DivisionId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", phone.UserId);
            ViewBag.TarifId = new SelectList(db.Tarifs, "TarifId", "FullName", phone.TarifId);
            return View(phone);
        }

        //
        // GET: /Phones/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", phone.DivisionId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", phone.UserId);
            ViewBag.TarifId = new SelectList(db.Tarifs, "TarifId", "FullName", phone.TarifId);
            return View(phone);
        }

        //
        // POST: /Phones/Edit/5

        [HttpPost]
        public ActionResult Edit(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DivisionId = new SelectList(db.Divisions, "DivisionId", "Name", phone.DivisionId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", phone.UserId);
            ViewBag.TarifId = new SelectList(db.Tarifs, "TarifId", "FullName", phone.TarifId);
            return View(phone);
        }

       

        public ActionResult GetStatistics(int id = 0)
        {
            Phone phone = db.Phones.Include(p => p.Tarif).FirstOrDefault(p => p.PhoneId == id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            var InCalls = db.Calls.Where(p => (p.FromId == phone.PhoneId)).Include(p => p.To).Include(p => p.To.Tarif);
            var OutCalls = db.Calls.Where(p => (p.ToId == phone.PhoneId)).Include(p => p.From).Include(p => p.From.Tarif);
            List<Call> calls = InCalls.Concat(OutCalls).ToList<Call>();
            ViewBag.calls = calls;
            ViewBag.phone = phone;
            return View(phone);
        }

        //
        // GET: /Phones/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Phone phone = db.Phones.Find(id);
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        //
        // POST: /Phones/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Phone phone = db.Phones.Find(id);
            db.Phones.Remove(phone);
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