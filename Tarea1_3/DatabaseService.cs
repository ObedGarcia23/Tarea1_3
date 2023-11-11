using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Tarea1_3.Models;

namespace Tarea1_3
{
    public class DatabaseService : DbContext
    {
        public DbSet<Personas> Personas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databasePath = "personas.db3"; // Puedes cambiar el nombre del archivo según tus preferencias
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }

   
}
