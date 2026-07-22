using API.ProjetoCursosIdioma.Validations;
using System.ComponentModel.DataAnnotations;

namespace API.ProjetoCursosIdioma.Models.Dto.AlunoDto_s
{
    public class AlunoAddRequestDto
    {
        //Properties
        [Required]
        [CPF]
        public string Cpf { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "E-mail Inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "É necessário informar pelo menos uma turma.")]
        [MinLength(1, ErrorMessage = "O aluno deve ser cadastrado em pelo menos uma turma.")]
        public List<Guid> TurmaIds { get; set; } = new();
    }
}
