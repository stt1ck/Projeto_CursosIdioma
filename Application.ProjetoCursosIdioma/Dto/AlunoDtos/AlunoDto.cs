namespace Application.ProjetoCursosIdioma.Dto.AlunoDtos
{
    public class AlunoDto
    {
        //Properties
        public Guid Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public List<TurmaDoAlunoDto> Turmas { get; set; } = new();
    }
}
