﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M1GL2023.Models
{
    public class MaisonViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBien { get; set; }

        [Display(Name = "Description du bien"), Required(ErrorMessage = "*"), MaxLength(1000, ErrorMessage = "La taille maximal est de 1000")]
        public string Description { get; set; }

        [Display(Name = "Superfice")]
        public float SuperficiBien { get; set; }

        [Display(Name = "Localisation du bien"), Required(ErrorMessage = "*"), MaxLength(300, ErrorMessage = "La taille maximal est de 300")]
        public string Localisation { get; set; }
        public int ? ProprietaireId { get; set; } // Clé étrangère
        [ForeignKey("ProprietaireId")]
        public virtual Proprietaire Proprietaire { get; set; }

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