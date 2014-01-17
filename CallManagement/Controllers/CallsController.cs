using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallManagement.Models;
using System.IO;
using Excel;
using System.Globalization;

namespace CallManagement.Controllers
{
    public class CallsController : Controller
    {
        private PhoneDBContext db = new PhoneDBContext();

        //
        // GET: /Calls/

        public ActionResult Index()
        {
            var calls = db.Calls.Include(p => p.From).Include(p => p.To);
            return View(calls.ToList());
        }

        //
        // GET: /Calls/Details/5

        public ActionResult Details(int id = 0)
        {
            Call call = db.Calls.Find(id);
            if (call == null)
            {
                return HttpNotFound();
            }
            return View(call);
        }

        //
        // GET: /Calls/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Calls/Create

        [HttpPost]
        public ActionResult Create(Call call)
        {
            if (ModelState.IsValid)
            {
                db.Calls.Add(call);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(call);
        }

        //
        // GET: /Calls/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Call call = db.Calls.Find(id);
            if (call == null)
            {
                return HttpNotFound();
            }
            return View(call);
        }

        //
        // POST: /Calls/Edit/5

        [HttpPost]
        public ActionResult Edit(Call call)
        {
            if (ModelState.IsValid)
            {
                db.Entry(call).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(call);
        }

        [HttpPost]
        public ActionResult ProcessFile(HttpPostedFileBase file)
        {
            string path = System.IO.Path.Combine(Server.MapPath("~/Content"), System.IO.Path.GetFileName(file.FileName));
            file.SaveAs(path);
            FileStream stream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();
            String fr,to,st,en;
            foreach (DataRow row in result.Tables[0].Rows)
            {
                fr = row.ItemArray[0].ToString();
                to = row.ItemArray[1].ToString();
                st = row.ItemArray[2].ToString();
                en = row.ItemArray[3].ToString();
                Phone PhoneFrom = db.Phones.FirstOrDefault(p => (p.Number == fr));
                Phone PhoneTo = db.Phones.FirstOrDefault(p => (p.Number == to));
                DateTime start = DateTime.ParseExact(st, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
                DateTime end = DateTime.ParseExact(en, "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);
                Call call = new Call(PhoneFrom, PhoneTo, start, end);
                db.Calls.Add(call);
                db.SaveChanges();
            }
            excelReader.Close();
            ViewBag.Message = "File uploaded successfully";
            return RedirectToAction("Index");
        }

        //
        // GET: /Calls/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Call call = db.Calls.Find(id);
            if (call == null)
            {
                return HttpNotFound();
            }
            return View(call);
        }

        //
        // POST: /Calls/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Call call = db.Calls.Find(id);
            db.Calls.Remove(call);
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