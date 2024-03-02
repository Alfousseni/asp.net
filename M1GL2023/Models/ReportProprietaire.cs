using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M1GL2023.Models
{
    public class ReportProprietaire
    {
      

        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}