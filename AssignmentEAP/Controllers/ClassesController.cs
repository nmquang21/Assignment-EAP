using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentEAP.Models;
using LinqKit;
using PagedList;

namespace AssignmentEAP.Controllers
{
    public class ClassesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Classes
        public ActionResult Index(string search, int? page, string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            var predicate = PredicateBuilder.New<Class>(true);
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
            return View(@class);
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
        public ActionResult Create([Bind(Include = "Class_Id,Class_name,Created_at,Updated_at,Deleted_at")] Class @class)
        {
            if (ModelState.IsValid)
            {
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
        public ActionResult Edit([Bind(Include = "Class_Id,Class_name,Created_at,Updated_at,Deleted_at")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
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
