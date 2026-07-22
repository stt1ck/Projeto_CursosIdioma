namespace API.ProjetoCursosIdioma.Models.Domain
{
    public class Aluno
    {
        //Properties
        public Guid Id { get; set; }

        public string Cpf { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public ICollection<AlunoTurma> AlunoTurmas { get; set; } = new List<AlunoTurma>();
    }
}
