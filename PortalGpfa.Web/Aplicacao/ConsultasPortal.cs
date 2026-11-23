using System.Collections.Generic;
using PortalGpfa.Web.Dominio;

namespace PortalGpfa.Web.Aplicacao
{
    /// <summary>
    /// Consultas de leitura usadas pelas telas, sem dependência de System.Web ou Session.
    /// </summary>
    public sealed class ConsultasPortal
    {
        private readonly EstadoPortal _estado;

        public ConsultasPortal(EstadoPortal estado)
        {
            _estado = Guarda.NaoNulo(estado, nameof(estado));
        }

        public IReadOnlyList<Membro> ListarMembros()
        {
            return _estado.Membros;
        }

        public IReadOnlyList<Projeto> ListarProjetos()
        {
            return _estado.Projetos;
        }

        public Membro BuscarMembro(int id)
        {
            return _estado.BuscarMembro(id);
        }

        public Projeto BuscarProjeto(int id)
        {
            return _estado.BuscarProjeto(id);
        }

        public IReadOnlyList<Projeto> ProjetosDoEstudante(Estudante estudante)
        {
            return _estado.ProjetosDoEstudante(estudante);
        }

        public IReadOnlyList<Projeto> ProjetosDoPesquisador(Pesquisador pesquisador)
        {
            return _estado.ProjetosDoPesquisador(pesquisador);
        }

        public IReadOnlyList<Pesquisador> PesquisadoresDoProjeto(Projeto projeto)
        {
            return _estado.PesquisadoresDoProjeto(projeto);
        }

        public IReadOnlyList<Estudante> EstudantesDoProjeto(Projeto projeto)
        {
            return _estado.EstudantesDoProjeto(projeto);
        }

        public IReadOnlyList<Estudante> OrientandosAtuais(Pesquisador pesquisador)
        {
            return _estado.OrientandosAtuais(pesquisador);
        }

        public IReadOnlyList<Estudante> OrientandosPassados(Pesquisador pesquisador)
        {
            return _estado.OrientandosPassados(pesquisador);
        }

        public IReadOnlyList<VinculoOrientacao> OrientacoesDoEstudante(Estudante estudante)
        {
            return _estado.OrientacoesDoEstudante(estudante);
        }

        public Pesquisador OrientadorAtual(Estudante estudante)
        {
            return _estado.OrientadorAtual(estudante);
        }

        public IReadOnlyList<Coorientador> CoorientadoresDoEstudante(Estudante estudante)
        {
            return _estado.CoorientadoresDoEstudante(estudante);
        }
    }
}
