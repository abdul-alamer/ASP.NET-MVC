﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class PatientModel
    {
      //  public int Id { get; set; }
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Dosage { get; set; }
    }
}
