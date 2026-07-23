namespace Domain.ProjetoCursosIdioma.Entities
{
    public class Aluno
    {
        //Properties
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        //Nav Properties
        public ICollection<AlunoTurma> AlunoTurmas { get; set; } = new List<AlunoTurma>();

        //Methods
        public bool AlreadySignedIn(Guid turmaId)
        {
            return AlunoTurmas.Any(at => at.TurmaId == turmaId);
        }
        public bool IsSignedIn()
        {
            return AlunoTurmas.Any();
        }
    }
}
