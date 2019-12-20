using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentEAP.Models;
using AssignmentEAP.Service;

namespace AssignmentEAP.Controllers
{
    public class DisciplineStudentsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: DisciplineStudents
        public ActionResult Index()
        {
            return View(db.DisciplineStudents.ToList());
        }

        // GET: DisciplineStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineStudent disciplineStudent = db.DisciplineStudents.Find(id);
            if (disciplineStudent == null)
            {
                return HttpNotFound();
            }
            return View(disciplineStudent);
        }

        // GET: DisciplineStudents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DisciplineStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(string studentID, int disciplineID)
        {
            DisciplineStudentService disciplineStudentService = new DisciplineStudentService();
            return disciplineStudentService.CheckDisciplineStudent(studentID, disciplineID);
        }

        // GET: DisciplineStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineStudent disciplineStudent = db.DisciplineStudents.Find(id);
            if (disciplineStudent == null)
            {
                return HttpNotFound();
            }
            return View(disciplineStudent);
        }

        // POST: DisciplineStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisciplineStudent_id,Discipline_Value,Created_at,Updated_at,Deleted_at")] DisciplineStudent disciplineStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disciplineStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disciplineStudent);
        }

        // GET: DisciplineStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineStudent disciplineStudent = db.DisciplineStudents.Find(id);
            if (disciplineStudent == null)
            {
                return HttpNotFound();
            }
            return View(disciplineStudent);
        }

        // POST: DisciplineStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DisciplineStudent disciplineStudent = db.DisciplineStudents.Find(id);
            db.DisciplineStudents.Remove(disciplineStudent);
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
