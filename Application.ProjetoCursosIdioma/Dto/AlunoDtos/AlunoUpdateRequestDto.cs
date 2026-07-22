using System.ComponentModel.DataAnnotations;

namespace Application.ProjetoCursosIdioma.Dto.AlunoDtos
{
    public class AlunoUpdateRequestDto
    {
        //Properties
        [Required]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
