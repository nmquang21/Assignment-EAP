using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentEAP.Models;
using LinqKit;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace AssignmentEAP.Controllers
{
    public class StudentsController : Controller
    {
        private MyDbContext db = new MyDbContext();
        //Search Student
        public ActionResult SearchStudent(string term) {
            var listStudent = from s in db.Students select s;
            //var listStudent = db.Students.Select(s => new { id = s.RollNumber, text = s.Student_Name + "-" + s.RollNumber });
            var predicate = PredicateBuilder.New<Student>(true);
            if (!String.IsNullOrEmpty(term))
            {
                predicate = predicate.Or(s => s.Student_Name.Contains(term));
                predicate = predicate.Or(s => s.Email.Contains(term));
                predicate = predicate.Or(s => s.RollNumber.Contains(term));
                predicate = predicate.Or(s => s.Phone.Contains(term));
            }
            else
            {
            
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            listStudent = listStudent.Where(predicate);
            var result = new List<Student>(listStudent).Select(s => new { id = s.RollNumber, text = s.Student_Name + "-" + s.RollNumber });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

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
            predicate = predicate.And(s => s.Deleted_at == null);
            predicate = predicate.And(s => s.Class.Deleted_at == null);
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
        public ActionResult GetChartData(string id, string start, string end)
        {
            var startTime = DateTime.Now;
            startTime = startTime.AddYears(-1);
            try
            {
                startTime = DateTime.Parse(start);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0, 0);

            var endTime = DateTime.Now;
            try
            {
                endTime = DateTime.Parse(end);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            endTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, 23, 59, 59, 0);

            var data = db.DisciplineStudents.Where(s => s.Student.RollNumber == id && (s.Deleted_at == null) &&(s.Student.Deleted_at == null) && (s.Created_at >= startTime && s.Created_at <= endTime))
                .GroupBy(
                    s => new
                    {
                        Year = s.Created_at.Year,
                        Month = s.Created_at.Month,
                        //Week = GetWeekNumberOfMonth(s.Created_at)
                        //Day = s.Created_at.Day
                    }
                ).Select(g => new
                {
                    Date = g.FirstOrDefault().Created_at,
                    Money = g.Where(s => s.Discipline.Discipline_name == "Money").Sum(s => (double?)s.Discipline_Value) ?? 0,
                    PushUp = g.Where(s => s.Discipline.Discipline_name != "Money").Sum(s => (double?)s.Discipline_Value)?? 0,
                    Count = g.Count()
                }).OrderBy(s => s.Date).ToList();

            return new JsonResult()
            {
                Data = data.Select(s => new
                {
                    Date = s.Date.ToString("d"),
                    Money = s.Money,
                    PushUp = s.PushUp,
                    Count = s.Count
                }),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
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
            var data = db.DisciplineStudents
                .Where(d => d.Student.RollNumber == id)
                .GroupBy(d=>d.Student.RollNumber)
                .Select(s => new
                {
                    TotalMoney= s.Where(d => d.Discipline.Discipline_name == "Money").Sum(d => (double?)d.Discipline_Value)??0,
                    TotalPushUp = s.Where(d => d.Discipline.Discipline_name != "Money").Sum(d => (double?)d.Discipline_Value) ?? 0,
                });
            var totalMoney = data.Sum(d => (double?)d.TotalMoney) ?? 0;
            ViewBag.TotalPushUp = data.Sum(d => (double?)d.TotalPushUp) ?? 0;
            ViewBag.TotalMoney = String.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:c}", totalMoney);
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
                TempData["success"] = "Success!";
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
        public ActionResult Edit([Bind(Include = "RollNumber,Student_Name,Avatar,Birthday,Email,Phone,Class_Id,Deleted_at")] Student student)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(student).State = EntityState.Modified;
                var existStudent = db.Students.Find(student.RollNumber);
                student.Updated_at = DateTime.Now;
                student.Created_at = existStudent.Created_at;
                db.Students.AddOrUpdate(student);
                db.SaveChanges();
                TempData["success"] = "Success!";
                return RedirectToAction("Index");
            }
            ViewBag.Class_Id = new SelectList(db.Classes, "Class_Id", "Class_name", student.Class_Id);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { success = false, message = "Student is not found" }, JsonRequestBehavior.AllowGet);
            }
            student.Deleted_at = DateTime.Now;
            db.Students.AddOrUpdate(student);
            db.SaveChanges();
            return new JsonResult()
            {
                Data = "Done!",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
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
        //static int GetWeekNumberOfMonth(DateTime date)
        //{
        //    date = date.Date;
        //    DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
        //    DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
        //    if (firstMonthMonday > date)
        //    {
        //        firstMonthDay = firstMonthDay.AddMonths(-1);
        //        firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
        //    }
        //    return (date - firstMonthMonday).Days / 7 + 1;
        //}
    }
}
