using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class PatientModel
    {


        [Display(Name = "Name")]
        [Required(ErrorMessage = "You need to give us your name.")]
        public string Name { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "You need to give us your Age.")]
        public int Age { get; set; }

        [Display(Name = "Dosage")]
        [Required(ErrorMessage = "You need to give The Dosage   .")]
        public int Dosage { get; set; }

        [Display(Name = "Patient Id")]
        public int PatientId { get; set; }
    }
}