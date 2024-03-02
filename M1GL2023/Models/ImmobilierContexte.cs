using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace M1GL2023.Models
{
    public class ImmobilierContexte : DbContext
    {
        public DbSet<Bien> Biens {  get; set; }
        public DbSet<Maison> Maisons { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }

        public DbSet<Appartement> Appartements { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<Proprietaire> Proprietaires { get; set; }
        public DbSet<AgentImmobilier> AgentsImmobiliers { get; set; }
        public ImmobilierContexte() : base("GestImmoConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bien>()
                .HasRequired(b => b.Proprietaire)
                .WithMany(p => p.Biens)
                .HasForeignKey(b => b.ProprietaireId);

            modelBuilder.Entity<Bien>()
                .Map<Maison>(m => m.Requires("TypeBien").HasValue("Maison"))
                .Map<Appartement>(a => a.Requires("TypeBien").HasValue("Appartement"))
                .Map<Studio>(s => s.Requires("TypeBien").HasValue("Studio"))
                .Map<Terrain>(t => t.Requires("TypeBien").HasValue("Terrain"));

            modelBuilder.Entity<Utilisateur>()
                .Map<Proprietaire>(p => p.Requires("TypeUtilisateur").HasValue("Proprietaire"))
                .Map<AgentImmobilier>(a => a.Requires("TypeUtilisateur").HasValue("AgentImmobilier"));
        }

        public System.Data.Entity.DbSet<M1GL2023.Models.StudioViewModel> StudioViewModels { get; set; }
    }
}