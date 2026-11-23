using System;
using System.Collections.Generic;

namespace PortalGpfa.Web.Dominio
{
    public enum NivelEstudante
    {
        Graduacao,
        PosGraduacao
    }

    public sealed class Estudante : Membro
    {
        public NivelEstudante Nivel { get; }

        public Estudante(
            int id,
            string nome,
            string formacao,
            string biografia,
            string fotoUrl,
            string lattesUrl,
            string orcidUrl,
            string scholarUrl,
            string pretensoesPesquisa,
            NivelEstudante nivel,
            IEnumerable<Artigo> artigos)
            : base(id, nome, formacao, biografia, fotoUrl, lattesUrl, orcidUrl, scholarUrl, pretensoesPesquisa, artigos)
        {
            if (nivel != NivelEstudante.Graduacao && nivel != NivelEstudante.PosGraduacao)
            {
                throw new ArgumentOutOfRangeException(nameof(nivel), "O nível do estudante é inválido.");
            }

            Nivel = nivel;
        }
    }
}
