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
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter roll number")]
        public string RollNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter student name")]
        [DisplayName(" Student Name")]
        public string Student_Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter avatar")]
        public string Avatar { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter birth day")]
        [DisplayName("Birth Day")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
            ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }
        [DisplayName("Class")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter class")]
        public int Class_Id { get; set; }
        public virtual Class Class { get; set; }
        public virtual ICollection<DisciplineStudent> DisciplineStudents { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
    }
}