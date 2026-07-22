using Domain.ProjetoCursosIdioma.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ProjetoCursosIdioma.Data
{
    public class PCI_DbContext : DbContext
    {
        public PCI_DbContext(DbContextOptions<PCI_DbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<NivelTurma> NivelTurmas { get; set; }
        public DbSet<AlunoTurma> AlunoTurmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);    

            modelBuilder.Entity<Turma>()
                .HasIndex(t => new
                {
                    t.Name,
                    t.NivelTurmaId,
                    t.AnoLetivo,
                    t.NumeroTurma
                })
                .IsUnique();

            modelBuilder.Entity<Aluno>()
                .HasIndex(a => new { a.Email })
                .IsUnique();

            modelBuilder.Entity<Aluno>()
                .Property(aluno => aluno.Cpf)
                .IsRequired()
                .HasMaxLength(11)
                .IsUnicode(false);

            modelBuilder.Entity<Aluno>().HasIndex(aluno => aluno.Cpf)
                .IsUnique();

            modelBuilder.Entity<AlunoTurma>(entity =>
            {
                entity.HasKey(at => new
                {
                    at.AlunoId,
                    at.TurmaId
                });

                entity.HasOne(at => at.Aluno)
                    .WithMany(a => a.AlunoTurmas)
                    .HasForeignKey(at => at.AlunoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(at => at.Turma)
                    .WithMany(t => t.AlunoTurmas)
                    .HasForeignKey(at => at.TurmaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //Seeding Níveis
            var nivelTurmas = new List<NivelTurma>()
            {
                new NivelTurma
                {
                    Id = Guid.Parse("20ef9ed7-a2de-4b12-afbc-d7e95a2bdf1e"),
                    Name = "Básico"
                },
                new NivelTurma
                {
                    Id = Guid.Parse("ab10d5c3-0dac-4087-a6ec-50871b732e96"),
                    Name = "Intermediário"
                },
                new NivelTurma
                {
                    Id = Guid.Parse("9fd3436d-c6e3-4c54-b2ae-6769659c1644"),
                    Name = "Avançado"
                }
            };

            modelBuilder.Entity<NivelTurma>().HasData(nivelTurmas);
        }
    }
}
