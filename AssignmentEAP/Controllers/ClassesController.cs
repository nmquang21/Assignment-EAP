using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using System.Data.Entity.Migrations;

using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentEAP.Models;
using LinqKit;
using PagedList;
using WebGrease;

namespace AssignmentEAP.Controllers
{
    public class ClassesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Classes
        public ActionResult Index(string search, int? page, string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TotalSort = sortOrder == "total" ? "total_desc" : "total";
            //ViewBag.DisSort = sortOrder == "dis" ? "dis_desc" : "dis";
            var predicate = PredicateBuilder.New<Class>(true);
            predicate = predicate.And(s =>s.Deleted_at == null);
            if (search != null)
            {
                page = 1;
            }
            ViewBag.Search = search;
            ViewBag.SortOrder = sortOrder;
            var listClass = from s in db.Classes select s;

            if (!String.IsNullOrEmpty(search))
            {
                predicate = predicate.Or(s => s.Class_name.Contains(search));
            }
            listClass = listClass.Where(predicate);
            switch (sortOrder)
            {
                case "name_desc":
                    listClass = listClass.OrderByDescending(s => s.Class_name);
                    break;
                case "total_desc":
                    listClass = listClass.OrderByDescending(s => s.Students.Count);
                    break;
                case "total":
                    listClass = listClass.OrderBy(s => s.Students.Count);
                    break;
                default:
                    listClass = listClass.OrderBy(s => s.Class_name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(listClass.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult GetClassAjax()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Class> classes = db.Classes.ToList();
            return Json(new { data = classes }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChartData(int? classId, string start, string end)
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
            var data = db.Classes
                .Join(db.Students, c => c.Class_Id, s => s.Class.Class_Id, (c, s) => new { c, s})
                .Join(db.DisciplineStudents, cs => cs.s.RollNumber, ds => ds.Student.RollNumber, (cs, ds) => new { cs, ds })
                .Where(csds => csds.cs.c.Class_Id == classId &&(csds.cs.c.Deleted_at == null) 
                        && (csds.cs.s.Deleted_at == null) &&(csds.ds.Deleted_at == null) 
                        && (csds.ds.Created_at >= startTime && csds.ds.Created_at <= endTime))
                .GroupBy(
                    csds => new
                    {
                        Year = csds.ds.Created_at.Year,
                        Month = csds.ds.Created_at.Month,
                        //Week = GetWeekNumberOfMonth(s.Created_at)
                        Day = csds.ds.Created_at.Day
                    }
                ).Select(g => new
                {
                    Date = g.FirstOrDefault().ds.Created_at,
                    Money = g.Where(s => s.ds.Discipline.Discipline_name== "Money").Sum(s => (double?)s.ds.Discipline_Value) ?? 0,
                    PushUp = g.Where(s => s.ds.Discipline.Discipline_name != "Money").Sum(s => (double?)s.ds.Discipline_Value) ?? 0,
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

        // GET: Classes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentClass = @class;

            //List student of class:
            var listStudent = db.Students.Where(s => s.Class.Class_Id == @class.Class_Id && (s.Deleted_at == null)).ToList();
            return View(listStudent);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Class_Id,Class_name")] Class @class)
        {
            if (ModelState.IsValid)
            {
                @class.Created_at = DateTime.Now;
                db.Classes.Add(@class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@class);
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Class_Id,Class_name")] Class @class)
        {
            if (ModelState.IsValid)
            {
                var currentClass = db.Classes.Find(@class.Class_Id);
                @class.Updated_at = DateTime.Now;
                @class.Created_at = currentClass.Created_at;
                db.Classes.AddOrUpdate(@class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@class);
        }

        // GET: Classes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
            db.Classes.Remove(@class);
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
