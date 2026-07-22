using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjetoCursosIdioma.Common
{
    public enum errorType
    {
        validation,
        notFound,
        conflict,
        Interno
    }

    public class Resultado<T>
    {
        public bool success {  get; private set; }
        public T? Dados { get; private set; }
        public string? Mensagem { get; private set; }
        public errorType? errorType { get; private set; }

        public static Resultado<T> Ok(T dados)
        {
            return new Resultado<T>
            {
                success = true,
                Dados = dados
            };
        }

        public static Resultado<T> Falha(string mensagem, errorType tipoErro)
        {
            return new Resultado<T>
            {
                success = false,
                Mensagem = mensagem,
                errorType = tipoErro
            };
        }
    }
}
