using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignmentEAP.Models
{
    public class DisciplineDefault
    {
        [Key]
        public int DisciplineDefaultId { get; set; }
        [DisplayName("Money Value")]
        public double Money { get; set; }
        [DisplayName("Squat Value")]
        public int Squat_Amout { get; set; }
        [DisplayName("Push Up Value")]
        public int Push_Up { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
    }
}