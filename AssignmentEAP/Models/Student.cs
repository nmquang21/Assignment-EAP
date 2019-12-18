using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignmentEAP.Models
{
    public class Student
    {
        [Key]
        public string RollNumber { get; set; }

        [DisplayName(" Student Name")]
        public string Student_Name { get; set; }
        public string Avatar { get; set; }

        [DisplayName("Birth Day")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Phone { get; set; }
        [DisplayName("Class")]
        public int Class_Id { get; set; }
        public virtual Class Class { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Deleted_at { get; set; }
    }
}