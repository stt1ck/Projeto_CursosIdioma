namespace Domain.ProjetoCursosIdioma.Entities
{
    public class AlunoTurma
    {
        public Guid AlunoId { get; set; }

        public Aluno Aluno { get; set; } = null!;

        public Guid TurmaId { get; set; }

        public Turma Turma { get; set; } = null!;
    }
}
