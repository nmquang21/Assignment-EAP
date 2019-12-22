using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssignmentEAP.Models
{
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Class_Id { get; set; }
        [Required(ErrorMessage = "Class Name is required.")]
        [UniqueClassName(ErrorMessage = "This class name is already registered.")]
        public string Class_name { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public class UniqueClassNameAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                MyDbContext db = new MyDbContext();
                var classWithTheSameName = db.Classes.SingleOrDefault(
                    u => u.Class_name == (string)value);
                return classWithTheSameName == null;
            }
        }
    }
}