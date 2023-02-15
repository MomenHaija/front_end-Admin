using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using University_Registration.Models;

namespace University_Registration.Controllers
{
    public class SubjectRegistrationsAdminController : Controller
    {
        private ERP_SystemEntities db = new ERP_SystemEntities();

        // GET: SubjectRegistrationsAdmin
        public ActionResult Index()
        {
            var subjectRegistrations = db.SubjectRegistrations.Include(s => s.Section).Include(s => s.Student).Include(s => s.Subject);
            return View(subjectRegistrations.ToList());
        }

        // GET: SubjectRegistrationsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectRegistration subjectRegistration = db.SubjectRegistrations.Find(id);
            if (subjectRegistration == null)
            {
                return HttpNotFound();
            }
            return View(subjectRegistration);
        }

        // GET: SubjectRegistrationsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.Section_ID = new SelectList(db.Sections, "Section_ID", "SectionDay");
            ViewBag.Student_ID = new SelectList(db.Students, "Student_ID", "Id");
            ViewBag.Subject_ID = new SelectList(db.Subjects, "Subject_ID", "Name");
            return View();
        }

        // POST: SubjectRegistrationsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectRegistrations_ID,Student_ID,Subject_ID,PaymentStatus,Section_ID,Price")] SubjectRegistration subjectRegistration)
        {
            if (ModelState.IsValid)
            {
                db.SubjectRegistrations.Add(subjectRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Section_ID = new SelectList(db.Sections, "Section_ID", "SectionDay", subjectRegistration.Section_ID);
            ViewBag.Student_ID = new SelectList(db.Students, "Student_ID", "Id", subjectRegistration.Student_ID);
            ViewBag.Subject_ID = new SelectList(db.Subjects, "Subject_ID", "Name", subjectRegistration.Subject_ID);
            return View(subjectRegistration);
        }

        // GET: SubjectRegistrationsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectRegistration subjectRegistration = db.SubjectRegistrations.Find(id);
            if (subjectRegistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.Section_ID = new SelectList(db.Sections, "Section_ID", "SectionDay", subjectRegistration.Section_ID);
            ViewBag.Student_ID = new SelectList(db.Students, "Student_ID", "Id", subjectRegistration.Student_ID);
            ViewBag.Subject_ID = new SelectList(db.Subjects, "Subject_ID", "Name", subjectRegistration.Subject_ID);
            return View(subjectRegistration);
        }

        // POST: SubjectRegistrationsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectRegistrations_ID,Student_ID,Subject_ID,PaymentStatus,Section_ID,Price")] SubjectRegistration subjectRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Section_ID = new SelectList(db.Sections, "Section_ID", "SectionDay", subjectRegistration.Section_ID);
            ViewBag.Student_ID = new SelectList(db.Students, "Student_ID", "Id", subjectRegistration.Student_ID);
            ViewBag.Subject_ID = new SelectList(db.Subjects, "Subject_ID", "Name", subjectRegistration.Subject_ID);
            return View(subjectRegistration);
        }

        // GET: SubjectRegistrationsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubjectRegistration subjectRegistration = db.SubjectRegistrations.Find(id);
            if (subjectRegistration == null)
            {
                return HttpNotFound();
            }
            return View(subjectRegistration);
        }

        // POST: SubjectRegistrationsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubjectRegistration subjectRegistration = db.SubjectRegistrations.Find(id);
            db.SubjectRegistrations.Remove(subjectRegistration);
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
