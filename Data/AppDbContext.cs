using Microsoft.EntityFrameworkCore;
using TrabalhoApi.Models;

namespace TrabalhoApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Estas propriedades definem as tabelas que serão criadas no MySQL
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração para garantir que o salário não cause erro de precisão
            modelBuilder.Entity<Funcionario>()
                .Property(f => f.Salario)
                .HasPrecision(18, 2);

            // Configuração explícita do relacionamento (Chave Estrangeira)
            modelBuilder.Entity<Funcionario>()
                .HasOne<Empresa>()
                .WithMany()
                .HasForeignKey(f => f.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
