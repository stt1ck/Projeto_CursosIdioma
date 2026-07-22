namespace API.ProjetoCursosIdioma.Models.Dto.AlunoDto_s
{
    public class TurmaDoAlunoDto
    {
        public string Name { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
        public int AnoLetivo { get; set; }
        public string NumeroTurma { get; set; } = string.Empty;
        public Guid TurmaId {  get; set; }
    }
}
