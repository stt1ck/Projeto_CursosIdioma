using System.ComponentModel.DataAnnotations;

namespace API.ProjetoCursosIdioma.Models.Domain
{
    public class Turma
    {
        //Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NumeroTurma { get; set; }

        public int AnoLetivo { get; set; }

        public ICollection<AlunoTurma> AlunoTurmas { get; set; } = new List<AlunoTurma>();

        //Nav Properties
        public Guid NivelTurmaId { get; set; }

        public NivelTurma NivelTurma { get; set; }

    }
}
