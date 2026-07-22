using System.ComponentModel.DataAnnotations;

namespace Domain.ProjetoCursosIdioma.Entities
{
    public class Turma
    {
        //Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NumeroTurma { get; set; }
        public int AnoLetivo { get; set; }

        //Nav Properties
        public Guid NivelTurmaId { get; set; }
        public NivelTurma NivelTurma { get; set; }
        public ICollection<AlunoTurma> AlunoTurmas { get; set; } = new List<AlunoTurma>();

        //Methods
        public const int maxCount = 5; 
        public bool hasAvailableSpace(int count)
        {
            return count < maxCount;
        }

    }
}
