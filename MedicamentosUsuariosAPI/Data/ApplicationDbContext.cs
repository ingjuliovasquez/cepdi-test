using Microsoft.EntityFrameworkCore;
using MedicamentosUsuariosAPI.Models;

namespace MedicamentosUsuariosAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<FormaFarmaceutica> FormasFarmaceuticas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones
            modelBuilder.Entity<Medicamento>()
                .HasOne(m => m.FormaFarmaceutica)
                .WithMany()
                .HasForeignKey(m => m.IdFormaFarmaceutica);
        }
    }
}
