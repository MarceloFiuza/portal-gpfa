using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PortalGpfa.Web.Dominio
{
    public sealed class EstadoPortal
    {
        private readonly ReadOnlyCollection<Membro> _membros;
        private readonly ReadOnlyCollection<Projeto> _projetos;
        private readonly ReadOnlyCollection<VinculoOrientacao> _orientacoes;
        private readonly ReadOnlyCollection<Coorientador> _coorientadores;
        private readonly ReadOnlyCollection<ParticipacaoPesquisador> _participacoesPesquisadores;
        private readonly ReadOnlyCollection<VinculoEstudanteProjeto> _vinculosEstudantesProjetos;

        public IReadOnlyList<Membro> Membros { get { return _membros; } }
        public IReadOnlyList<Projeto> Projetos { get { return _projetos; } }
        public IReadOnlyList<VinculoOrientacao> Orientacoes { get { return _orientacoes; } }
        public IReadOnlyList<Coorientador> Coorientadores { get { return _coorientadores; } }
        public IReadOnlyList<ParticipacaoPesquisador> ParticipacoesPesquisadores { get { return _participacoesPesquisadores; } }
        public IReadOnlyList<VinculoEstudanteProjeto> VinculosEstudantesProjetos { get { return _vinculosEstudantesProjetos; } }

        public EstadoPortal(
            IEnumerable<Membro> membros,
            IEnumerable<Projeto> projetos,
            IEnumerable<VinculoOrientacao> orientacoes,
            IEnumerable<Coorientador> coorientadores,
            IEnumerable<ParticipacaoPesquisador> participacoesPesquisadores,
            IEnumerable<VinculoEstudanteProjeto> vinculosEstudantesProjetos)
        {
            _membros = Copiar(membros, nameof(membros));
            _projetos = Copiar(projetos, nameof(projetos));
            _orientacoes = Copiar(orientacoes, nameof(orientacoes));
            _coorientadores = Copiar(coorientadores, nameof(coorientadores));
            _participacoesPesquisadores = Copiar(participacoesPesquisadores, nameof(participacoesPesquisadores));
            _vinculosEstudantesProjetos = Copiar(vinculosEstudantesProjetos, nameof(vinculosEstudantesProjetos));

            Validar();
        }

        public Membro BuscarMembro(int id)
        {
            return _membros.FirstOrDefault(m => m.Id == id);
        }

        public Projeto BuscarProjeto(int id)
        {
            return _projetos.FirstOrDefault(p => p.Id == id);
        }

        public IReadOnlyList<Projeto> ProjetosDoEstudante(Estudante estudante)
        {
            Guarda.NaoNulo(estudante, nameof(estudante));

            return _vinculosEstudantesProjetos
                .Where(v => ReferenceEquals(v.Estudante, estudante))
                .Select(v => v.Projeto)
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyList<Projeto> ProjetosDoPesquisador(Pesquisador pesquisador)
        {
            Guarda.NaoNulo(pesquisador, nameof(pesquisador));

            return _participacoesPesquisadores
                .Where(p => ReferenceEquals(p.Pesquisador, pesquisador))
                .Select(p => p.Projeto)
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyList<VinculoOrientacao> OrientacoesDoEstudante(Estudante estudante)
        {
            Guarda.NaoNulo(estudante, nameof(estudante));

            return _orientacoes
                .Where(o => ReferenceEquals(o.Estudante, estudante))
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyList<Pesquisador> PesquisadoresDoProjeto(Projeto projeto)
        {
            Guarda.NaoNulo(projeto, nameof(projeto));

            return _participacoesPesquisadores
                .Where(p => ReferenceEquals(p.Projeto, projeto))
                .Select(p => p.Pesquisador)
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyList<Estudante> EstudantesDoProjeto(Projeto projeto)
        {
            Guarda.NaoNulo(projeto, nameof(projeto));

            return _vinculosEstudantesProjetos
                .Where(v => ReferenceEquals(v.Projeto, projeto))
                .Select(v => v.Estudante)
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyList<Estudante> OrientandosAtuais(Pesquisador pesquisador)
        {
            Guarda.NaoNulo(pesquisador, nameof(pesquisador));

            return _orientacoes
                .Where(o => o.Atual && ReferenceEquals(o.Pesquisador, pesquisador))
                .Select(o => o.Estudante)
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyList<Estudante> OrientandosPassados(Pesquisador pesquisador)
        {
            Guarda.NaoNulo(pesquisador, nameof(pesquisador));

            return _orientacoes
                .Where(o => !o.Atual && ReferenceEquals(o.Pesquisador, pesquisador))
                .Select(o => o.Estudante)
                .ToList()
                .AsReadOnly();
        }

        public Pesquisador OrientadorAtual(Estudante estudante)
        {
            Guarda.NaoNulo(estudante, nameof(estudante));

            VinculoOrientacao vinculo = _orientacoes
                .FirstOrDefault(o => o.Atual && ReferenceEquals(o.Estudante, estudante));

            return vinculo == null ? null : vinculo.Pesquisador;
        }

        public IReadOnlyList<Coorientador> CoorientadoresDoEstudante(Estudante estudante)
        {
            Guarda.NaoNulo(estudante, nameof(estudante));

            return _coorientadores
                .Where(c => ReferenceEquals(c.Estudante, estudante))
                .ToList()
                .AsReadOnly();
        }

        private void Validar()
        {
            ValidarIdentificadoresUnicos();

            List<Pesquisador> pesquisadores = _membros.OfType<Pesquisador>().ToList();
            List<Estudante> estudantes = _membros.OfType<Estudante>().ToList();

            ValidarProjetos(pesquisadores);
            ValidarOrientacoes(pesquisadores, estudantes);
            ValidarCoorientadores(pesquisadores, estudantes);
            ValidarParticipacoesPesquisadores(pesquisadores);
            ValidarVinculosEstudantesProjetos(estudantes);
        }

        private void ValidarIdentificadoresUnicos()
        {
            if (_membros.Select(m => m.Id).Distinct().Count() != _membros.Count)
            {
                throw new InvalidOperationException("Há membros com identificadores repetidos.");
            }

            if (_projetos.Select(p => p.Id).Distinct().Count() != _projetos.Count)
            {
                throw new InvalidOperationException("Há projetos com identificadores repetidos.");
            }
        }

        private void ValidarProjetos(IReadOnlyList<Pesquisador> pesquisadores)
        {
            foreach (Projeto projeto in _projetos)
            {
                if (!pesquisadores.Contains(projeto.Coordenador))
                {
                    throw new InvalidOperationException(
                        "O coordenador do projeto '" + projeto.Titulo + "' não é um pesquisador cadastrado.");
                }
            }
        }

        private void ValidarOrientacoes(IReadOnlyList<Pesquisador> pesquisadores, IReadOnlyList<Estudante> estudantes)
        {
            // As regras abaixo exigem ao menos um vínculo e no máximo um vínculo atual.
            // Elas não exigem vínculo atual: um estudante pode ter somente histórico passado.
            // Sem uma definição de "estudante atual" e de ex-estudante (P06), o domínio
            // não consegue impor orientador atual obrigatório para todo estudante atual.
            HashSet<string> chaves = new HashSet<string>();
            HashSet<Estudante> comOrientacaoAtual = new HashSet<Estudante>();

            foreach (VinculoOrientacao vinculo in _orientacoes)
            {
                if (!pesquisadores.Contains(vinculo.Pesquisador))
                {
                    throw new InvalidOperationException("Há orientação com pesquisador não cadastrado.");
                }

                if (!estudantes.Contains(vinculo.Estudante))
                {
                    throw new InvalidOperationException("Há orientação com estudante não cadastrado.");
                }

                string chave = vinculo.Estudante.Id + ":" + vinculo.Pesquisador.Id;
                if (!chaves.Add(chave))
                {
                    throw new InvalidOperationException("Há vínculo de orientação repetido.");
                }

                if (vinculo.Atual && !comOrientacaoAtual.Add(vinculo.Estudante))
                {
                    throw new InvalidOperationException("Há estudante com mais de um orientador principal atual.");
                }
            }

            foreach (Estudante estudante in estudantes)
            {
                if (!_orientacoes.Any(o => ReferenceEquals(o.Estudante, estudante)))
                {
                    throw new InvalidOperationException(
                        "O estudante '" + estudante.Nome + "' não possui orientação principal registrada.");
                }
            }
        }

        private void ValidarCoorientadores(IReadOnlyList<Pesquisador> pesquisadores, IReadOnlyList<Estudante> estudantes)
        {
            HashSet<string> chaves = new HashSet<string>();

            foreach (Coorientador coorientador in _coorientadores)
            {
                if (!estudantes.Contains(coorientador.Estudante))
                {
                    throw new InvalidOperationException("Há coorientação com estudante não cadastrado.");
                }

                string chave;
                if (coorientador.EhExterno)
                {
                    chave = coorientador.Estudante.Id + ":externo:" + coorientador.Nome.ToUpperInvariant();
                }
                else
                {
                    if (!pesquisadores.Contains(coorientador.PesquisadorInterno))
                    {
                        throw new InvalidOperationException("Há coorientação com pesquisador não cadastrado.");
                    }

                    chave = coorientador.Estudante.Id + ":interno:" + coorientador.PesquisadorInterno.Id;
                }

                if (!chaves.Add(chave))
                {
                    throw new InvalidOperationException("Há coorientação repetida para o mesmo estudante.");
                }
            }
        }

        private void ValidarParticipacoesPesquisadores(IReadOnlyList<Pesquisador> pesquisadores)
        {
            HashSet<string> chaves = new HashSet<string>();

            foreach (ParticipacaoPesquisador participacao in _participacoesPesquisadores)
            {
                if (!pesquisadores.Contains(participacao.Pesquisador))
                {
                    throw new InvalidOperationException("Há participação de pesquisador não cadastrado.");
                }

                if (!_projetos.Contains(participacao.Projeto))
                {
                    throw new InvalidOperationException("Há participação em projeto não cadastrado.");
                }

                string chave = participacao.Projeto.Id + ":" + participacao.Pesquisador.Id;
                if (!chaves.Add(chave))
                {
                    throw new InvalidOperationException("Há participação de pesquisador repetida no mesmo projeto.");
                }
            }
        }

        private void ValidarVinculosEstudantesProjetos(IReadOnlyList<Estudante> estudantes)
        {
            HashSet<string> chaves = new HashSet<string>();

            foreach (VinculoEstudanteProjeto vinculo in _vinculosEstudantesProjetos)
            {
                if (!estudantes.Contains(vinculo.Estudante))
                {
                    throw new InvalidOperationException("Há vínculo com estudante não cadastrado.");
                }

                if (!_projetos.Contains(vinculo.Projeto))
                {
                    throw new InvalidOperationException("Há vínculo com projeto não cadastrado.");
                }

                string chave = vinculo.Estudante.Id + ":" + vinculo.Projeto.Id;
                if (!chaves.Add(chave))
                {
                    throw new InvalidOperationException("Há vínculo repetido entre estudante e projeto.");
                }
            }

            foreach (Estudante estudante in estudantes)
            {
                if (!_vinculosEstudantesProjetos.Any(v => ReferenceEquals(v.Estudante, estudante)))
                {
                    throw new InvalidOperationException(
                        "O estudante '" + estudante.Nome + "' não está vinculado a nenhum projeto.");
                }
            }
        }

        private static ReadOnlyCollection<T> Copiar<T>(IEnumerable<T> itens, string nome) where T : class
        {
            if (itens == null)
            {
                throw new ArgumentNullException(nome);
            }

            List<T> lista = itens.ToList();
            if (lista.Any(item => item == null))
            {
                throw new ArgumentException("A coleção não pode conter valores nulos.", nome);
            }

            return lista.AsReadOnly();
        }
    }
}
