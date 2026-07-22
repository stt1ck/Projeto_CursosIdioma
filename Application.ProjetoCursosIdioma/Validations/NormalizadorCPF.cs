namespace Application.ProjetoCursosIdioma.Validations
{
    public static class NormalizadorCPF
    {
        public static string Normalize(string cpf)
        {
            return new string(cpf.Where(char.IsDigit).ToArray());
        }
    }
}
