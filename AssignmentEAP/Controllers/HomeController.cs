using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentEAP.Models;

namespace AssignmentEAP.Controllers
{
    public class HomeController : Controller
    {
        MyDbContext db = new MyDbContext();
        public ActionResult Index()
        {
            ViewBag.TotalClass = db.Classes.Count();
            ViewBag.TotalStudent = db.Students.Count();
            ViewBag.TotalDiscipline = db.DisciplineStudents.Count();
            var totalMoney = db.DisciplineStudents.Where(d => d.Discipline.Discipline_name == "Money")
                .Sum(d => d.Discipline_Value);
            ViewBag.TotalMoney = String.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:c}", totalMoney);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}