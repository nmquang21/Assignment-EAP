using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentEAP.Models;
using LinqKit;
using PagedList;

namespace AssignmentEAP.Controllers
{
    public class StudentsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Students
        public ActionResult Index(string search, int? page, string sortOrder, string className)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSort = sortOrder == "id"? "id_desc":"id"; 
            ViewBag.ClassSort = sortOrder == "class"? "class_desc":"class"; 
            var predicate = PredicateBuilder.New<Student>(true);
            if (search != null)
            {
                page = 1;
            }
            ViewBag.Search = search;
            ViewBag.SortOrder = sortOrder;
            var listStudent = from s in db.Students select s;

            if (!String.IsNullOrEmpty(search))
            {
                predicate = predicate.Or(s => s.Student_Name.Contains(search));
                predicate = predicate.Or(s => s.Email.Contains(search));
                predicate = predicate.Or(s => s.RollNumber.Contains(search));
                predicate = predicate.Or(s => s.Phone.Contains(search));
            }
            if (!String.IsNullOrEmpty(className) && className != "All")
            {
                predicate = predicate.And(s => s.Class.Class_name == className);
            }
            ViewBag.Class = className;
            ViewBag.listClass = db.Classes.ToList();
            listStudent = listStudent.Where(predicate);
            switch (sortOrder)
            {
                case "name_desc":
                    listStudent = listStudent.OrderByDescending(s => s.Student_Name);
                    break;
                case "id":
                    listStudent = listStudent.OrderBy(s => s.RollNumber);
                    break;
                case "id_desc":
                    listStudent = listStudent.OrderByDescending(s => s.RollNumber);
                    break;
                case "class":
                    listStudent = listStudent.OrderBy(s => s.Class.Class_name);
                    break;
                case "class_desc":
                    listStudent = listStudent.OrderByDescending(s => s.Class.Class_name);
                    break;
                default:
                    listStudent = listStudent.OrderBy(s => s.Student_Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(listStudent.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult GetStudentAjax()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Student> listStudent = db.Students.ToList();
            return Json(new { data = listStudent }, JsonRequestBehavior.AllowGet);
        }
        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_name");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RollNumber,Student_Name,Avatar,Birthday,Email,Phone,Class_Id,Created_at,Updated_at,Deleted_at")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_name", student.Class_Id);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_name", student.Class_Id);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RollNumber,Student_Name,Avatar,Birthday,Email,Phone,Class_Id,Created_at,Updated_at,Deleted_at")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_name", student.Class_Id);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
