using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssignmentEAP.Models;

namespace AssignmentEAP.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        private List<Class> classes = new List<Class>()
        {
            new Class()
            {
                Class_name = "T1807E",
            },
            new Class()
            {
                Class_name = "T1805M",
            },
            new Class()
            {
                Class_name = "T1909A",
            },
            new Class()
            {
                Class_name = "T1707E",
            },
        };
        public ActionResult Index()
        {
            return View();
        }
    }
}