using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace M1GL2023.Models
{
    public class Maison : Bien
    {
        [Display(Name = "nombre de chambre"), Required(ErrorMessage = "*")]
        public int NbreChambre { get; set; }

        [Display(Name = "nombre de cuisine"), Required(ErrorMessage = "*")]
        public int NbreCuisine { get; set; }

        [Display(Name = "nombre de salle d'eau"), Required(ErrorMessage = "*")]
        public int NbreSalleEau { get; set; }

        [Display(Name = "nombre de toilette"), Required(ErrorMessage = "*")]
        public int NbreToilete { get; set; }

       
    }
}