using API.ProjetoCursosIdioma.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.ProjetoCursosIdioma.Data
{
    public class PCI_DbContext : DbContext
    {
        public PCI_DbContext(DbContextOptions<PCI_DbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<NivelTurma> NivelTurmas { get; set; }

        //Seedar os Alunos e Turmas


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

            /*var turmas = new List<Turma>() seed this later
            {
                new Turma
                {
                    Id = Guid.Parse("DE0544DB-A409-41F8-A7DB-4150F54000A1"),
                    Name = "Português"
                }
            };*/
        }
    }
}
