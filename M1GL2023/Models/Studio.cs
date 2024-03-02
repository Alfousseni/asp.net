using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M1GL2023.Models
{
    public class Studio: Bien
    {
        [Display(Name = "Meuble"), Required(ErrorMessage = "*")]

        public bool Meuble { get; set; }



    }
}