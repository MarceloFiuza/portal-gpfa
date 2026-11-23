namespace PortalGpfa.Web.Dominio
{
    public sealed class VinculoOrientacao
    {
        public Pesquisador Pesquisador { get; }
        public Estudante Estudante { get; }
        public bool Atual { get; }

        public VinculoOrientacao(Pesquisador pesquisador, Estudante estudante, bool atual)
        {
            Pesquisador = Guarda.NaoNulo(pesquisador, nameof(pesquisador));
            Estudante = Guarda.NaoNulo(estudante, nameof(estudante));
            Atual = atual;
        }
    }
}
