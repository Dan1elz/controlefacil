using ControleFacil.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618
namespace ControleFacil.src.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Ambientes> Ambientes { get; set; }
        public DbSet<Armarios> Armarios { get; set; }
        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<Itens> Itens { get; set; }
        public DbSet<Movimentacoes> Movimentacoes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Itens>()
            .HasIndex(e => e.Ni)
            .IsUnique();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}