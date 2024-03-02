using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M1GL2023.Models
{
    public class Appartement : Bien
    {
        [Display(Name = "numero d'etage"), Required(ErrorMessage = "*")]

        public int Etage { get; set; }
        [Display(Name = "nombre de piece"), Required(ErrorMessage = "*")]

        public int NombreDePieces { get; set; }

        
    }
}