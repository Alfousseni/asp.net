using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M1GL2023.Models
{
    public class Utilisateur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "le nom"), Required(ErrorMessage = "*")]
        public string Nom { get; set; }

        [Display(Name = "le prenom"), Required(ErrorMessage = "*")]
        public string Prenom { get; set; }

        [Display(Name = "l'email"), Required(ErrorMessage = "*")]
        public string Email { get; set; }
        [Display(Name = "nomUser", ResourceType = typeof(ResourceImmo)), Required(ErrorMessage = "*")]

        public string Username { get; set; }
        [Display(Name = "password"), Required(ErrorMessage = "*")]

        public string Password { get; set; }

        
    }
}