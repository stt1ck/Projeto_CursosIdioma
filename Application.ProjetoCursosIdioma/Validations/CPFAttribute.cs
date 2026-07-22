using System.ComponentModel.DataAnnotations;
using Caelum.Stella.CSharp.Validation;

namespace Application.ProjetoCursosIdioma.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class CPFAttribute : ValidationAttribute
    {
        public CPFAttribute() { ErrorMessage = "CPF inválido."; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;
            if (value is not string cpf || string.IsNullOrWhiteSpace(cpf)) { return ValidationResult.Success; }

            string normalizedCpf = NormalizadorCPF.Normalize(cpf);

            var validation = new CPFValidator();

            return validation.IsValid(normalizedCpf) ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
