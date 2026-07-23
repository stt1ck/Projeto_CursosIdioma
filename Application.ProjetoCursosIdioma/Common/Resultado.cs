using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjetoCursosIdioma.Common
{
    public enum ErrorType
    {
        validation,
        notFound,
        conflict,
        Interno
    }

    public class Resultado<T>
    {
        public bool Success {  get; private set; }
        public T? Dados { get; private set; }
        public string? Mensagem { get; private set; }
        public ErrorType? ErrorType { get; private set; }

        public static Resultado<T> Ok(T dados)
        {
            return new Resultado<T>
            {
                Success = true,
                Dados = dados
            };
        }

        public static Resultado<T> Falha(string mensagem, ErrorType tipoErro)
        {
            return new Resultado<T>
            {
                Success = false,
                Mensagem = mensagem,
                ErrorType = tipoErro
            };
        }
    }
}
