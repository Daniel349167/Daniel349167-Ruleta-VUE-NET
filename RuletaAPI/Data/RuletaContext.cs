using Microsoft.EntityFrameworkCore;
using RuletaAPI.Models;

namespace RuletaAPI.Data
{
    public class RuletaContext : DbContext
    {
        public RuletaContext(DbContextOptions<RuletaContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Apuesta> Apuestas { get; set; }
        public DbSet<ApuestaTemporal> ApuestasTemporales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.ApuestasTemporales)
                .WithOne(a => a.Usuario)
                .HasForeignKey(a => a.UsuarioId);

            modelBuilder.Entity<Apuesta>()
                .ToTable("Apuestas");

            modelBuilder.Entity<ApuestaTemporal>()
                .ToTable("ApuestasTemporales");
        }
    }
}
