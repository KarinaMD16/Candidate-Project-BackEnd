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
                new Habilidad { Id = 1, Nombre = "C#", icono = "https://cdn3.iconfinder.com/data/icons/teenyicons-solid-vol-1/15/c-sharp-1024.png" },
                new Habilidad { Id = 2, Nombre = ".NET", icono = "https://seekicon.com/free-icon-download/dot-net_1.svg" },
                new Habilidad { Id = 3, Nombre = "React", icono = "https://cdn3.iconfinder.com/data/icons/font-awesome-brands/512/react-1024.png" },
                new Habilidad { Id = 4, Nombre = "SQL", icono = "https://cdn2.iconfinder.com/data/icons/coding-and-development-outline/60/SQL-Database-programming-developer-software-query-language-512.png" },
                new Habilidad { Id = 5, Nombre = "Java", icono = "https://cdn3.iconfinder.com/data/icons/font-awesome-brands/512/java-1024.png" },
                new Habilidad { Id = 6, Nombre = "Python", icono = "https://cdn0.iconfinder.com/data/icons/programming-set/25/phyton-512.png" },
                new Habilidad { Id = 7, Nombre = "JavaScript", icono = "https://cdn4.iconfinder.com/data/icons/scripting-and-programming-languages/512/js-512.png" },
                new Habilidad { Id = 8, Nombre = "NestJS", icono = "https://static-00.iconduck.com/assets.00/nestjs-icon-2048x2038-6bjnpydw.png" },
                new Habilidad { Id = 9, Nombre = "Angular", icono = "https://cdn3.iconfinder.com/data/icons/font-awesome-brands/512/angular-1024.png" },
                new Habilidad { Id = 10, Nombre = "Docker", icono = "https://cdn3.iconfinder.com/data/icons/font-awesome-brands/640/docker-512.png" }
            );

            modelBuilder.Entity<Empresa>().HasData(
                new Empresa { Id = 1, Nombre = "Google", SitioWeb = "google.com", Correo = "google.com", icono= "https://cdn4.iconfinder.com/data/icons/logos-and-brands/512/150_Google_logo_logos-512.png" },
                new Empresa { Id = 2, Nombre = "Facebook", SitioWeb = "facebook.com", Correo = "facebook@gmail.com", icono= "https://cdn4.iconfinder.com/data/icons/logos-and-brands/512/122_Facebook_F_logo_logos-1024.png" },
                new Empresa { Id = 3, Nombre = "Intelec", SitioWeb = "intelec.com", Correo = "intelec@gmail.com", icono= "https://wolksoftcr.com/wp-content/uploads/2023/03/300x300-300x300.png" },
                new Empresa { Id = 4, Nombre = "Intel", SitioWeb = "intel.com", Correo = "intel@gmail.com", icono= "https://cdn4.iconfinder.com/data/icons/flat-brand-logo-2/512/intel-1024.png" },
                new Empresa { Id = 5, Nombre = "Gollo", SitioWeb = "gollo.com", Correo = "gollo@gmail.com", icono= "https://www.caprede.com/wp-content/uploads/2022/11/Gollo-Logo.png" },
                new Empresa { Id = 6, Nombre = "Monge", SitioWeb = "monge.com", Correo = "monge@gmail.com", icono= "https://th.bing.com/th/id/R.4c0c361ef18f9da5113fcbc68dce275b?rik=p8aAv2fPifUstQ&pid=ImgRaw&r=0" }
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