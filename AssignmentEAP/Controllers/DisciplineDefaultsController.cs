using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentEAP.Models;

namespace AssignmentEAP.Controllers
{
    public class DisciplineDefaultsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: DisciplineDefaults
        public ActionResult Index()
        {
            return View(db.DisciplineDefaults.ToList());
        }

        
    
        // GET: DisciplineDefaults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineDefault disciplineDefault = db.DisciplineDefaults.Find(id);
            if (disciplineDefault == null)
            {
                return HttpNotFound();
            }
            return View(disciplineDefault);
        }

        // POST: DisciplineDefaults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisciplineDefaultId,Money,Squat_Amout,Push_Up")] DisciplineDefault disciplineDefault)
        {
            if (ModelState.IsValid)
            {
                var valueDefault = db.DisciplineDefaults.Where(c => c.DisciplineDefaultId == disciplineDefault.DisciplineDefaultId).FirstOrDefault();
                valueDefault.Money = disciplineDefault.Money;
                valueDefault.Push_Up = disciplineDefault.Push_Up;
                valueDefault.Squat_Amout = disciplineDefault.Squat_Amout;
                valueDefault.Updated_at = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(disciplineDefault);
        }

        // GET: DisciplineDefaults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisciplineDefault disciplineDefault = db.DisciplineDefaults.Find(id);
            if (disciplineDefault == null)
            {
                return HttpNotFound();
            }
            return View(disciplineDefault);
        }

        // POST: DisciplineDefaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DisciplineDefault disciplineDefault = db.DisciplineDefaults.Find(id);
            db.DisciplineDefaults.Remove(disciplineDefault);
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
