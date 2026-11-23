using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PortalGpfa.Web.Dominio
{
    public abstract class Membro
    {
        private readonly ReadOnlyCollection<Artigo> _artigos;

        public int Id { get; }
        public string Nome { get; }
        public string Formacao { get; }
        public string Biografia { get; }
        public string FotoUrl { get; }
        public string LattesUrl { get; }
        public string OrcidUrl { get; }
        public string ScholarUrl { get; }
        public string PretensoesPesquisa { get; }
        public IReadOnlyList<Artigo> Artigos { get { return _artigos; } }

        protected Membro(
            int id,
            string nome,
            string formacao,
            string biografia,
            string fotoUrl,
            string lattesUrl,
            string orcidUrl,
            string scholarUrl,
            string pretensoesPesquisa,
            IEnumerable<Artigo> artigos)
        {
            Id = Guarda.Positivo(id, nameof(id));
            Nome = Guarda.Texto(nome, nameof(nome));
            Formacao = Guarda.Texto(formacao, nameof(formacao));
            Biografia = Guarda.Texto(biografia, nameof(biografia));
            FotoUrl = Guarda.TextoOpcional(fotoUrl);
            LattesUrl = Guarda.UrlHttpOpcional(lattesUrl, nameof(lattesUrl));
            OrcidUrl = Guarda.UrlHttpOpcional(orcidUrl, nameof(orcidUrl));
            ScholarUrl = Guarda.UrlHttpOpcional(scholarUrl, nameof(scholarUrl));
            PretensoesPesquisa = Guarda.Texto(pretensoesPesquisa, nameof(pretensoesPesquisa));

            if (artigos == null)
            {
                throw new ArgumentNullException(nameof(artigos));
            }

            List<Artigo> lista = artigos.ToList();
            if (lista.Count != 5)
            {
                throw new ArgumentException("Cada membro deve ter exatamente cinco artigos de destaque.", nameof(artigos));
            }

            if (lista.Any(a => a == null))
            {
                throw new ArgumentException("A lista de artigos não pode conter valores nulos.", nameof(artigos));
            }

            if (lista.Distinct().Count() != lista.Count)
            {
                throw new ArgumentException("Um membro não pode destacar o mesmo artigo mais de uma vez.", nameof(artigos));
            }

            _artigos = lista.AsReadOnly();
        }
    }
}
