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
        public string formatMessage(string studentName, string n, int quantity, string discipline)
        {
            string message = String.Format("Student {0} is late for school for the {1} time, Discipline-x{2} {3}", studentName, n, quantity, discipline );
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
                var disciplineDefault = db.DisciplineDefaults.Find(1);
                disciplineStudent.Student = db.Students.Find(rollnumber);
                disciplineStudent.Discipline = db.Disciplines.Find(id);
                var _DBdisciplineStudents = db.DisciplineStudents;
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
                                disciplineStudent.Discipline_Value = 3 * disciplineDefault.Squat_Amout;
                                message = formatMessage(student.Student_Name, "third time", 3, "Squat");
                                break;
                            case 2:
                                disciplineStudent.Discipline_Value = 3 * disciplineDefault.Money;
                                message = formatMessage(student.Student_Name, "third time", 3, "Money");
                                break;
                            case 3:
                                disciplineStudent.Discipline_Value = 3* disciplineDefault.Push_Up;
                                message = formatMessage(student.Student_Name, "third time", 3, "Push-Up");
                                break;
                        }
                    }
                    else
                    {                      
                        switch (id)
                        {
                           
                        case 1:
                            disciplineStudent.Discipline_Value = 2 * disciplineDefault.Squat_Amout;
                            message = formatMessage(student.Student_Name, "second time", 2, "Squat");
                            break;
                        case 2:
                            disciplineStudent.Discipline_Value = 2 * disciplineDefault.Money;
                            message = formatMessage(student.Student_Name, "second time", 2, "Money");
                            break;
                        case 3:
                            disciplineStudent.Discipline_Value = 2 * disciplineDefault.Push_Up;
                            message = formatMessage(student.Student_Name, "second time", 2, "Push-Up");
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
                            disciplineStudent.Discipline_Value = disciplineDefault.Squat_Amout;
                            message = formatMessage(student.Student_Name, "first time", 1, "Squat");
                            break;
                        case 2:
                            disciplineStudent.Discipline_Value = disciplineDefault.Money;
                            message = formatMessage(student.Student_Name, "first time", 1, "Money");
                            break;
                        case 3:
                            disciplineStudent.Discipline_Value = disciplineDefault.Push_Up;
                            message = formatMessage(student.Student_Name, "first time", 1, "Push-Up");
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