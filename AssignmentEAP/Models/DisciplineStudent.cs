﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssignmentEAP.Models
{
    public class DisciplineStudent
    {
        [Key]
        public int DisciplineStudent_id { get; set; }
        [DisplayName("Discipline Value")]
        public double Discipline_Value { get; set; }
        [DisplayName("Discipline Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
        public virtual Student Student { get; set; }
        public virtual Discipline Discipline { get; set; }
    }
}