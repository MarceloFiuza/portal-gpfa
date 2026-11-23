using PortalGpfa.Web.Dominio;

namespace PortalGpfa.Web
{
    /// <summary>
    /// Rótulos de apresentação derivados dos tipos do domínio.
    /// </summary>
    public static class Rotulos
    {
        public static string TipoMembro(Membro membro)
        {
            if (membro is Pesquisador)
            {
                return "Pesquisador";
            }

            Estudante estudante = membro as Estudante;
            if (estudante != null)
            {
                return estudante.Nivel == NivelEstudante.Graduacao
                    ? "Estudante de graduação"
                    : "Estudante de pós-graduação";
            }

            if (membro is Tecnico)
            {
                return "Técnico";
            }

            return "Membro";
        }

        public static string SituacaoProjeto(SituacaoProjeto situacao)
        {
            return situacao == SituacaoProjeto.EmAndamento ? "Em andamento" : "Concluído";
        }

        public static string SituacaoOrientacao(bool atual)
        {
            return atual ? "Atual" : "Passado";
        }

        public static string OrigemCoorientador(bool externo)
        {
            return externo ? "Externo" : "Pesquisador do grupo";
        }
    }
}
