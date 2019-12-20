using AssignmentEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentEAP.Controllers
{
    public class DisciplineController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: Discipline
        public ActionResult Index()
        {
            var listDisciplines = db.Students.Select(c => new { id = c.RollNumber, text = c.Student_Name}).ToList();
            return Json(listDisciplines, JsonRequestBehavior.AllowGet);
        }
    }
}