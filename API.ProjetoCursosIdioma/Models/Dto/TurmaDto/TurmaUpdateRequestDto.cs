using System.ComponentModel.DataAnnotations;

namespace API.ProjetoCursosIdioma.Models.Dto.TurmaDto
{
    public class TurmaUpdateRequestDto
    {
        public string Name { get; set; }

        public string NumeroTurma { get; set; }

        public int AnoLetivo { get; set; }

        public Guid NivelTurmaId { get; set; }
    }
}
