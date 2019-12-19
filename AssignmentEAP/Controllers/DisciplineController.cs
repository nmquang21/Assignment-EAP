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
            var listDisciplines = db.Disciplines.Select(c => new { id = c.Discipline_id, name = c.Discipline_name}).ToList();
            return Json(listDisciplines, JsonRequestBehavior.AllowGet);
        }
    }
}