using System.Collections.Generic;

namespace PortalGpfa.Web.Dominio
{
    public sealed class Pesquisador : Membro
    {
        public Pesquisador(
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
            : base(id, nome, formacao, biografia, fotoUrl, lattesUrl, orcidUrl, scholarUrl, pretensoesPesquisa, artigos)
        {
        }
    }
}
