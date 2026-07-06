using API.ProjetoCursosIdioma.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.ProjetoCursosIdioma.Data
{
    public class PCI_DbContext : DbContext
    {
        public PCI_DbContext(DbContextOptions<PCI_DbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }

        //Seedar os Alunos e Turmas

    }
}
