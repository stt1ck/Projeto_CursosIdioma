using System.ComponentModel.DataAnnotations;

namespace API.ProjetoCursosIdioma.Models.Dto.TurmaDto
{
    public class TurmaAddRequestDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Número mínimo de 3 caractéres")]
        [MaxLength(3, ErrorMessage = "Número máximo de 3 caractéres")]
        public string NumeroTurma { get; set; }

        [Required]
        public int AnoLetivo { get; set; }

        [Required]
        public Guid NivelTurmaId { get; set; }
    }
}
