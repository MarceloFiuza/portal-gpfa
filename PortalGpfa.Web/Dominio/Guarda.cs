using System;

namespace PortalGpfa.Web.Dominio
{
    internal static class Guarda
    {
        public static string Texto(string valor, string nome)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new ArgumentException("O campo " + nome + " é obrigatório.", nome);
            }

            return valor.Trim();
        }

        public static string TextoOpcional(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
        }

        public static string UrlHttpOpcional(string valor, string nome)
        {
            string url = TextoOpcional(valor);
            if (url == null)
            {
                return null;
            }

            Uri endereco;
            bool absoluto = Uri.TryCreate(url, UriKind.Absolute, out endereco);
            if (!absoluto ||
                (endereco.Scheme != Uri.UriSchemeHttp && endereco.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException("O campo " + nome + " deve ser uma URL http ou https.", nome);
            }

            return url;
        }

        public static int Positivo(int valor, string nome)
        {
            if (valor <= 0)
            {
                throw new ArgumentOutOfRangeException(nome, "O valor deve ser positivo.");
            }

            return valor;
        }

        public static T NaoNulo<T>(T valor, string nome) where T : class
        {
            if (valor == null)
            {
                throw new ArgumentNullException(nome);
            }

            return valor;
        }
    }
}
