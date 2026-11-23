using System;

namespace PortalGpfa.Web.Dominio
{
    public sealed class Artigo
    {
        public string Titulo { get; }
        public string Veiculo { get; }
        public int Ano { get; }
        public string Link { get; }

        public Artigo(string titulo, string veiculo, int ano, string link = null)
        {
            Titulo = Guarda.Texto(titulo, nameof(titulo));
            Veiculo = Guarda.Texto(veiculo, nameof(veiculo));

            if (ano <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ano), "O ano do artigo deve ser positivo.");
            }

            Ano = ano;
            Link = Guarda.UrlHttpOpcional(link, nameof(link));
        }
    }
}
