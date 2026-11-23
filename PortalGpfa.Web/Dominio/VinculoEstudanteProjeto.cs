namespace PortalGpfa.Web.Dominio
{
    public sealed class VinculoEstudanteProjeto
    {
        public Estudante Estudante { get; }
        public Projeto Projeto { get; }

        public VinculoEstudanteProjeto(Estudante estudante, Projeto projeto)
        {
            Estudante = Guarda.NaoNulo(estudante, nameof(estudante));
            Projeto = Guarda.NaoNulo(projeto, nameof(projeto));
        }
    }
}
