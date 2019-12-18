using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignmentEAP.Models
{
    public class Discipline
    {
        [Key]
        public int Discipline_id { get; set; }
        public string Discipline_name { get; set; }
        public string Description { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public DateTime Deleted_at { get; set; }
        public virtual ICollection<DisciplineStudent> DisciplineStudents { get; set; }
    }
}