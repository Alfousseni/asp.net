using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M1GL2023.Models
{
    public class Proprietaire : Utilisateur
    {
        public virtual ICollection<Bien> Biens { get; set; }

        
    }
}