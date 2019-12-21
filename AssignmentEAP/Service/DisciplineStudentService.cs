using AssignmentEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentEAP.Service
{
    
    public class DisciplineStudentService
    {

        private MyDbContext db = new MyDbContext();
        public string formatMessage(string studentName, string n, int x, string discipline, double quantity)
        {
            string message = String.Format("Student {0} is late for school for the {1} time, Discipline: x{2} {3} - {4}", studentName, n, x, discipline,quantity );
            return message;
        }
        public ActionResult CheckDisciplineStudent(string rollnumber, int? id) {
            var disciplineStudent = new DisciplineStudent();
            var students = db.Students.Where(s => s.RollNumber.Equals(rollnumber));
            if (students.Count() == 0)
            {
                return new JsonResult()
                {
                    Data = new
                    {
                        message = "Student not found!!"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                };
            }
            else
            {
                string message = "";
                var student = students.FirstOrDefault();         
                disciplineStudent.Student = db.Students.Find(rollnumber);
                disciplineStudent.Discipline = db.Disciplines.Find(id);
                var _DBdisciplineStudents = db.DisciplineStudents;
                var disciplineDefault = db.DisciplineDefaults.Find(1);
                var _moneyDefault = disciplineDefault.Money;
                var _pushUpDefault = disciplineDefault.Push_Up;
                var _squatDefault = disciplineDefault.Squat_Amout;
                var today = DateTime.Today;
                DisciplineStudent yesterday = db.DisciplineStudents.Where(c => c.Student.RollNumber.Equals(rollnumber)).OrderByDescending(c=>c.Created_at).Take(1).FirstOrDefault();

                if (yesterday != null&&(today-yesterday.Created_at).TotalDays == 1)
                {
                    DisciplineStudent beforeYesterday = db.DisciplineStudents.Where(c => c.Student.RollNumber.Equals(rollnumber)).OrderByDescending(c => c.Created_at).Skip(1).Take(1).FirstOrDefault();
                    if(beforeYesterday != null && (today - beforeYesterday.Created_at).TotalDays == 2)
                    {                                        
                        switch (id)
                        {
                            case 1:
                                disciplineStudent.Discipline_Value = 3 * _squatDefault;
                                message = formatMessage(student.Student_Name, "third time", 3, "Squat", 3 * _squatDefault);
                                break;
                            case 2:
                                disciplineStudent.Discipline_Value = 3 * _moneyDefault;
                                message = formatMessage(student.Student_Name, "third time", 3, "Money", 3 * _moneyDefault);
                                break;
                            case 3:
                                disciplineStudent.Discipline_Value = 3 * _pushUpDefault;
                                message = formatMessage(student.Student_Name, "third time", 3, "Push-Up", 3 * _pushUpDefault);
                                break;
                        }
                    }
                    else
                    {                      
                        switch (id)
                        {
                           
                        case 1:
                            disciplineStudent.Discipline_Value = 2 * _squatDefault;
                            message = formatMessage(student.Student_Name, "second time", 2, "Squat", 2 * _squatDefault);
                            break;
                        case 2:
                            disciplineStudent.Discipline_Value = 2 * _moneyDefault;
                            message = formatMessage(student.Student_Name, "second time", 2, "Money", 2 * _moneyDefault);
                            break;
                        case 3:
                            disciplineStudent.Discipline_Value = 2 * _pushUpDefault;
                            message = formatMessage(student.Student_Name, "second time", 2, "Push-Up", 2 * _pushUpDefault);
                            break;
                       
                        }
                    }
                }
                else if(yesterday != null && today.Date == yesterday.Created_at.Date)
                {
                    return new JsonResult()
                    {
                        Data = new
                        {
                            message = "Discipline has been saved before!"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    };
                }
                else 
                {                
                    switch (id)
                    {
                        case 1:
                            disciplineStudent.Discipline_Value = _squatDefault;
                            message = formatMessage(student.Student_Name, "first time", 1, "Squat", _squatDefault);
                            break;
                        case 2:
                            disciplineStudent.Discipline_Value = _moneyDefault;
                            message = formatMessage(student.Student_Name, "first time", 1, "Money", _moneyDefault);
                            break;
                        case 3:
                            disciplineStudent.Discipline_Value = _pushUpDefault;
                            message = formatMessage(student.Student_Name, "first time", 1, "Push-Up", _pushUpDefault);
                            break;
                    }
                }
              
                disciplineStudent.Created_at = DateTime.Now;
                db.DisciplineStudents.Add(disciplineStudent);
                db.SaveChanges();
                return new JsonResult()
                {
                    Data = new
                    {
                        message = message
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                };
            }
        }
    }
}