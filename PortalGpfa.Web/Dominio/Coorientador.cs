namespace PortalGpfa.Web.Dominio
{
    public sealed class Coorientador
    {
        public Estudante Estudante { get; }
        public Pesquisador PesquisadorInterno { get; }
        public string NomeExterno { get; }
        public bool EhExterno { get { return PesquisadorInterno == null; } }
        public string Nome { get { return PesquisadorInterno == null ? NomeExterno : PesquisadorInterno.Nome; } }

        private Coorientador(Estudante estudante, Pesquisador pesquisadorInterno, string nomeExterno)
        {
            Estudante = Guarda.NaoNulo(estudante, nameof(estudante));
            PesquisadorInterno = pesquisadorInterno;
            NomeExterno = nomeExterno;
        }

        public static Coorientador Interno(Estudante estudante, Pesquisador pesquisador)
        {
            return new Coorientador(
                estudante,
                Guarda.NaoNulo(pesquisador, nameof(pesquisador)),
                null);
        }

        public static Coorientador Externo(Estudante estudante, string nome)
        {
            return new Coorientador(
                estudante,
                null,
                Guarda.Texto(nome, nameof(nome)));
        }
    }
}
