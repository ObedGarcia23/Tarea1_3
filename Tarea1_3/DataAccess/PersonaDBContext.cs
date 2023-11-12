using Tarea1_3.Models;
using Tarea1_3.Utilidades;
using Microsoft.EntityFrameworkCore;

namespace Tarea1_3.DataAccess
{
    public class PersonaDBContext : DbContext
    {
        public DbSet<Persona> Personas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String conexionDB = $"Filename={ConexionDB.DevolverRuta("personas.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(col => col.Id);
                entity.Property(col => col.Id).IsRequired().ValueGeneratedOnAdd();

            });
        }
    }
}
