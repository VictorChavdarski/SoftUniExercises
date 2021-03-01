﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P01_HospitalDatabase.Data.Models
{
    public class Visitation
    {
        public int VisitationsId { get; set; }

        public virtual DateTime Date{ get; set; }

        [MaxLength(250)]
        public string Comments { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

    }
}
