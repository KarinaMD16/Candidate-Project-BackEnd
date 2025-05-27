using CandidateAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandidateAPI.JWTDataBase
{
    public class JWTDbContext : DbContext
    {
        public JWTDbContext(DbContextOptions<JWTDbContext> options) : base(options) {}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Clave compuesta para tabla intermedia
            modelBuilder.Entity<OfertaHabilidad>()
                .HasKey(oh => new { oh.OfertaId, oh.HabilidadId });

            // Habilidades pre-cargadas
            modelBuilder.Entity<Habilidad>().HasData(
                new Habilidad { Id = 1, Nombre = "C#" },
                new Habilidad { Id = 2, Nombre = ".NET" },
                new Habilidad { Id = 3, Nombre = "React" },
                new Habilidad { Id = 4, Nombre = "SQL" },
                new Habilidad { Id = 5, Nombre = "Java" },
                new Habilidad { Id = 6, Nombre = "Python" },
                new Habilidad { Id = 7, Nombre = "JavaScript" },
                new Habilidad { Id = 8, Nombre = "NestJS" },
                new Habilidad { Id = 9, Nombre = "Angular" },
                new Habilidad { Id = 10, Nombre = "Docker" }
            );

            modelBuilder.Entity<Empresa>().HasData(
                new Empresa { Id = 1, Nombre = "Google", SitioWeb = "google.com", Correo = "google.com" },
                new Empresa { Id = 2, Nombre = "Facebook", SitioWeb = "facebook.com", Correo = "facebook@gmail.com" },
                new Empresa { Id = 3, Nombre = "Intelec", SitioWeb = "intelec.com", Correo = "intelec@gmail.com" },
                new Empresa { Id = 4, Nombre = "Intel", SitioWeb = "intel.com", Correo = "intel@gmail.com" },
                new Empresa { Id = 5, Nombre = "Gollo", SitioWeb = "gollo.com", Correo = "gollo@gmail.com" },
                new Empresa { Id = 6, Nombre = "Monge", SitioWeb = "monge.com", Correo = "monge@gmail.com" }
);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidato>()
                .HasMany(c => c.Habilidades)
                .WithMany(h => h.Candidatos);

            modelBuilder.Entity<Oferta>()
                .HasMany(c => c.CandidatosPostulados)
                .WithMany(h => h.OfertasAplicadas);

        }

        // DbSets
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<Habilidad> Habilidades { get; set; }
        public DbSet<OfertaHabilidad> OfertaHabilidades { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
    }
}