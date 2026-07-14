using API.ProjetoCursosIdioma.Validations;
using Microsoft.AspNetCore.Mvc;
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
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail Inválido")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
