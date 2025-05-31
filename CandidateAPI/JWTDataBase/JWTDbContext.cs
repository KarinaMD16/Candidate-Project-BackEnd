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
                new Habilidad { Id = 1, Nombre = "C#", icono = "https://img1.daumcdn.net/thumb/R800x0/?scode=mtistory2&fname=https%3A%2F%2Fblog.kakaocdn.net%2Fdn%2Fok2TI%2FbtsNMUd1Iil%2F1zSQf6HULGEqFfHHiY77Q1%2Fimg.png" },
                new Habilidad { Id = 2, Nombre = ".NET", icono = "https://miro.medium.com/v2/resize:fit:1200/1*oc0z3w_7TKgE0oSyiqpnRw.png" },
                new Habilidad { Id = 3, Nombre = "React", icono = "https://cdn4.iconfinder.com/data/icons/logos-3/600/React.js_logo-512.png" },
                new Habilidad { Id = 4, Nombre = "SQL", icono = "https://gimgs2.nohat.cc/thumb/f/640/sql-logo-illustration-microsoft-azure-sql-database-microsoft-sql-server-database-blue-text-logo-png--compngwingzoupl.jpg" },
                new Habilidad { Id = 5, Nombre = "Java", icono = "https://www.citypng.com/public/uploads/preview/hd-java-logo-transparent-background-701751694771845zainlxmlfo.png" },
                new Habilidad { Id = 6, Nombre = "Python", icono = "https://c0.klipartz.com/pngpicture/620/835/gratis-png-logo-azul-y-amarillo-logo-de-python-thumbnail.png" },
                new Habilidad { Id = 7, Nombre = "JavaScript", icono = "https://icon2.cleanpng.com/20180810/ekz/11448a7a96ee808a3cdbaf0df9570976.webp" },
                new Habilidad { Id = 8, Nombre = "NestJS", icono = "https://image.pngaaa.com/758/7692758-middle.png" },
                new Habilidad { Id = 9, Nombre = "Angular", icono = "https://c0.klipartz.com/pngpicture/497/691/sticker-png-angularjs-data-binding-web-application-angular-angle-triangle-logo-web-application-line-thumbnail.png" },
                new Habilidad { Id = 10, Nombre = "Docker", icono = "https://cdn4.iconfinder.com/data/icons/logos-and-brands/512/97_Docker_logo_logos-512.png" }
            );

            modelBuilder.Entity<Empresa>().HasData(
                new Empresa { Id = 1, Nombre = "Google", SitioWeb = "google.com", Correo = "google.com", icono= "https://th.bing.com/th/id/R.0fa3fe04edf6c0202970f2088edea9e7?rik=joOK76LOMJlBPw&riu=http%3a%2f%2fpluspng.com%2fimg-png%2fgoogle-logo-png-open-2000.png&ehk=0PJJlqaIxYmJ9eOIp9mYVPA4KwkGo5Zob552JPltDMw%3d&risl=&pid=ImgRaw&r=0" },
                new Empresa { Id = 2, Nombre = "Facebook", SitioWeb = "facebook.com", Correo = "facebook@gmail.com", icono= "https://pngimg.com/uploads/facebook_logos/facebook_logos_PNG19757.png" },
                new Empresa { Id = 3, Nombre = "Intelec", SitioWeb = "intelec.com", Correo = "intelec@gmail.com", icono= "https://www.intelec.co.cr/wp-content/uploads/2024/10/logonew.png" },
                new Empresa { Id = 4, Nombre = "Intel", SitioWeb = "intel.com", Correo = "intel@gmail.com", icono= "https://th.bing.com/th/id/OIP.jo5dPgs47NBogIJiW78VkQHaE8?rs=1&pid=ImgDetMain" },
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