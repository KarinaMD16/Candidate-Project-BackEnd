
using CandidateAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandidateAPI.JWTDataBase
{
    //Esta clase es la que se encarga de crear la base de datos en memoria
    public class JWTDbContext : DbContext
    {
        //Constructor de la clase
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "JWTDataBase");
        }
        //Aqui se define la tabla de productos de la base de datos
        public DbSet<Candidato> Candidatos { get; set; }

    }
}
