namespace PortalGpfa.Web.Dominio
{
    public sealed class ParticipacaoPesquisador
    {
        public Pesquisador Pesquisador { get; }
        public Projeto Projeto { get; }

        public ParticipacaoPesquisador(Pesquisador pesquisador, Projeto projeto)
        {
            Pesquisador = Guarda.NaoNulo(pesquisador, nameof(pesquisador));
            Projeto = Guarda.NaoNulo(projeto, nameof(projeto));
        }
    }
}
