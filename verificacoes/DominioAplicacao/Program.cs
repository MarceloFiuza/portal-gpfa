using System;
using System.Collections.Generic;
using System.Linq;
using PortalGpfa.Web.Dominio;
using PortalGpfa.Web.Infraestrutura;
using PortalGpfa.Web.Aplicacao;

namespace PortalDomainTest
{
    internal static class Program
    {
        private static int _verificacoes;

        private static void Main()
        {
            EstadoPortal estado = CargaPortalFicticio.Criar();
            VerificarCarga(estado);
            VerificarInvariantesNegativas();
            Console.WriteLine("OK: " + _verificacoes + " verificações de domínio e aplicação concluídas.");
        }

        private static void VerificarCarga(EstadoPortal estado)
        {
            List<Pesquisador> pesquisadores = estado.Membros.OfType<Pesquisador>().ToList();
            List<Estudante> estudantes = estado.Membros.OfType<Estudante>().ToList();
            List<Tecnico> tecnicos = estado.Membros.OfType<Tecnico>().ToList();

            Verificar(estado.Membros.Count == 7, "massa deve ter 7 membros");
            Verificar(estado.Membros.Count(m => m is Pesquisador) == 3, "lista deve conter 3 pesquisadores");
            Verificar(estado.Membros.Count(m => m is Estudante) == 3, "lista deve conter 3 estudantes");
            Verificar(estado.Membros.Count(m => m is Tecnico) == 1, "lista deve conter 1 técnico");
            Verificar(pesquisadores.Count == 3, "massa deve ter 3 pesquisadores");
            Verificar(estudantes.Count == 3, "massa deve ter 3 estudantes");
            Verificar(tecnicos.Count == 1, "massa deve ter 1 técnico");
            Verificar(estado.Projetos.Count == 3, "massa deve ter 3 projetos");
            Verificar(estado.Projetos.Count(p => p.Situacao == SituacaoProjeto.EmAndamento) == 2, "deve haver 2 projetos em andamento");
            Verificar(estado.Projetos.Count(p => p.Situacao == SituacaoProjeto.Concluido) == 1, "deve haver 1 projeto concluído");
            Verificar(estudantes.All(e => e.Nivel == NivelEstudante.Graduacao || e.Nivel == NivelEstudante.PosGraduacao), "estudantes devem ter níveis válidos");
            Verificar(estudantes.Any(e => e.Nivel == NivelEstudante.Graduacao), "deve haver estudante de graduação");
            Verificar(estudantes.Any(e => e.Nivel == NivelEstudante.PosGraduacao), "deve haver estudante de pós-graduação");

            foreach (Membro membro in estado.Membros)
            {
                Verificar(membro.Artigos.Count == 5, "membro " + membro.Nome + " deve ter 5 artigos");
                Verificar(membro.Artigos.Distinct().Count() == 5, "membro " + membro.Nome + " não pode repetir artigo");
            }

            foreach (Estudante estudante in estudantes)
            {
                Verificar(estado.ProjetosDoEstudante(estudante).Count >= 1, "estudante " + estudante.Nome + " deve ter projeto");
                Verificar(estado.Orientacoes.Any(o => ReferenceEquals(o.Estudante, estudante)), "estudante " + estudante.Nome + " deve ter orientação");
                Verificar(estado.Orientacoes.Count(o => ReferenceEquals(o.Estudante, estudante) && o.Atual) <= 1, "estudante " + estudante.Nome + " deve ter no máximo uma orientação atual");
            }

            Estudante diego = estudantes.Single(e => e.Nome == "Diego Alves");
            Estudante elisa = estudantes.Single(e => e.Nome == "Elisa Prado");
            Estudante felipe = estudantes.Single(e => e.Nome == "Felipe Ramos");
            Pesquisador ana = pesquisadores.Single(p => p.Nome == "Ana Beatriz Moura");
            Pesquisador bruno = pesquisadores.Single(p => p.Nome == "Bruno Tavares Lima");
            Pesquisador carla = pesquisadores.Single(p => p.Nome == "Carla Nogueira");

            Verificar(estado.ProjetosDoEstudante(diego).Count == 2, "Diego deve estar em dois projetos");
            Verificar(estado.OrientandosAtuais(ana).Count == 1 && estado.OrientandosAtuais(ana)[0] == diego, "Ana deve ter Diego como orientando atual");
            Verificar(estado.OrientandosPassados(ana).Count == 1 && estado.OrientandosPassados(ana)[0] == felipe, "Ana deve ter Felipe como orientando passado");
            Verificar(estado.OrientandosAtuais(bruno).Count == 1 && estado.OrientandosAtuais(bruno)[0] == elisa, "Bruno deve ter Elisa como orientanda atual");
            Verificar(estado.OrientadorAtual(diego) == ana, "orientador atual de Diego deve ser Ana");
            Verificar(estado.OrientadorAtual(elisa) == bruno, "orientador atual de Elisa deve ser Bruno");
            Verificar(estado.OrientadorAtual(felipe) == null, "Felipe não deve ter orientador atual");
            Verificar(estado.OrientandosPassados(bruno).Count == 0, "Bruno não deve ter orientando passado na massa");

            Verificar(estado.CoorientadoresDoEstudante(diego).Count == 1, "Diego deve ter um coorientador");
            Verificar(estado.CoorientadoresDoEstudante(diego)[0].EhExterno, "coorientador de Diego deve ser externo");
            Verificar(estado.CoorientadoresDoEstudante(elisa).Count == 2, "Elisa deve ter dois coorientadores");
            Verificar(estado.CoorientadoresDoEstudante(elisa).Any(c => c.EhExterno), "Elisa deve ter coorientador externo");
            Verificar(estado.CoorientadoresDoEstudante(elisa).Any(c => !c.EhExterno && c.PesquisadorInterno == carla), "Elisa deve ter Carla como coorientadora interna");
            Verificar(estado.CoorientadoresDoEstudante(felipe).Count == 0, "Felipe não deve ter coorientador");
            Verificar(estado.Membros.Count(m => m.Nome == "Patrícia Nunes") == 0, "coorientador externo não deve virar membro");

            Projeto colonias = estado.Projetos.Single(p => p.Id == 101);
            Projeto sensores = estado.Projetos.Single(p => p.Id == 102);
            Projeto trilhas = estado.Projetos.Single(p => p.Id == 103);

            Verificar(colonias.Coordenador == ana, "coordenador do projeto 101 deve ser Ana");
            Verificar(sensores.Coordenador == bruno, "coordenador do projeto 102 deve ser Bruno");
            Verificar(trilhas.Coordenador == carla, "coordenador do projeto 103 deve ser Carla");
            Verificar(estado.PesquisadoresDoProjeto(colonias).Count == 2, "projeto 101 deve ter dois pesquisadores");
            Verificar(estado.PesquisadoresDoProjeto(sensores).Count == 2, "projeto 102 deve ter dois pesquisadores");
            Verificar(estado.EstudantesDoProjeto(colonias).Count == 1, "projeto 101 deve ter um estudante");
            Verificar(estado.EstudantesDoProjeto(sensores).Count == 2, "projeto 102 deve ter dois estudantes");
            Verificar(!estado.PesquisadoresDoProjeto(trilhas).Contains(ana), "orientador não precisa participar do projeto do estudante");
            Verificar(trilhas.Situacao == SituacaoProjeto.Concluido, "projeto 103 deve estar concluído");
            Verificar(trilhas.VerbaCaptada == null, "projeto 103 deve demonstrar ausência de verba");
            Verificar(colonias.VerbaCaptada.HasValue && colonias.VerbaCaptada.Value > 0, "projeto 101 deve ter verba");

            Verificar(estado.Membros.Select(m => m.Id).Distinct().Count() == estado.Membros.Count, "IDs de membros devem ser únicos");
            Verificar(estado.Projetos.Select(p => p.Id).Distinct().Count() == estado.Projetos.Count, "IDs de projetos devem ser únicos");
            Verificar(estado.Orientacoes.Select(o => o.Estudante.Id + ":" + o.Pesquisador.Id).Distinct().Count() == estado.Orientacoes.Count, "orientações não podem repetir");
            Verificar(estado.ParticipacoesPesquisadores.Select(p => p.Projeto.Id + ":" + p.Pesquisador.Id).Distinct().Count() == estado.ParticipacoesPesquisadores.Count, "participações não podem repetir");
            Verificar(estado.VinculosEstudantesProjetos.Select(v => v.Estudante.Id + ":" + v.Projeto.Id).Distinct().Count() == estado.VinculosEstudantesProjetos.Count, "vínculos estudante-projeto não podem repetir");
            Verificar(estado.Membros.Where(m => m.LattesUrl != null).All(m => m.LattesUrl.StartsWith("https://example.org/")), "links Lattes devem ser de exemplo");
            Verificar(estado.Membros.SelectMany(m => m.Artigos).Where(a => a.Link != null).All(a => a.Link.StartsWith("https://example.org/")), "links de artigos devem ser de exemplo");
            Verificar(estado.Membros.All(m => m.FotoUrl != null), "todos os membros devem ter foto local");
            Verificar(estado.Membros.All(m => m.LattesUrl != null && m.OrcidUrl != null && m.ScholarUrl != null), "todos os membros devem ter os três links científicos de exemplo");
            Verificar(estado.Membros.SelectMany(m => m.Artigos).Count() == 35, "massa deve ter 35 artigos no total");

            ConsultasPortal consultas = new ConsultasPortal(estado);
            Verificar(consultas.ListarMembros().Count == 7, "camada de aplicação deve listar 7 membros");
            Verificar(consultas.ListarProjetos().Count == 3, "camada de aplicação deve listar 3 projetos");
            Verificar(consultas.BuscarMembro(1) == ana, "camada de aplicação deve localizar Ana");
            Verificar(consultas.BuscarMembro(999) == null, "camada de aplicação deve retornar null para membro inexistente");
            Verificar(consultas.BuscarProjeto(102) == sensores, "camada de aplicação deve localizar o projeto 102");
            Verificar(consultas.BuscarProjeto(999) == null, "camada de aplicação deve retornar null para projeto inexistente");
            Verificar(consultas.ProjetosDoEstudante(diego).Count == 2, "camada de aplicação deve listar dois projetos de Diego");
            Verificar(consultas.ProjetosDoPesquisador(ana).Count == 1, "camada de aplicação deve listar um projeto de Ana");
            Verificar(consultas.OrientacoesDoEstudante(felipe).Count == 1 && !consultas.OrientacoesDoEstudante(felipe)[0].Atual, "Felipe deve ter apenas orientação passada");
            // Felipe evidencia a limitação de P06: as regras atuais aceitam estudante sem vínculo atual.
            Verificar(consultas.OrientacoesDoEstudante(diego).Count == 1 && consultas.OrientacoesDoEstudante(diego)[0].Atual, "Diego deve ter exatamente uma orientação atual");
            Verificar(consultas.OrientacoesDoEstudante(elisa).Count == 1 && consultas.OrientacoesDoEstudante(elisa)[0].Atual, "Elisa deve ter exatamente uma orientação atual");
            Verificar(consultas.OrientadorAtual(felipe) == null, "Felipe não deve ter orientador atual");
            Verificar(consultas.PesquisadoresDoProjeto(sensores).Count == 2, "camada de aplicação deve listar dois pesquisadores no projeto 102");
            Verificar(consultas.EstudantesDoProjeto(sensores).Count == 2, "camada de aplicação deve listar dois estudantes no projeto 102");
            Verificar(consultas.CoorientadoresDoEstudante(elisa).Count == 2, "camada de aplicação deve listar dois coorientadores de Elisa");
        }

        private static void VerificarInvariantesNegativas()
        {
            List<Artigo> cincoArtigos = CriarArtigos("tema");
            Pesquisador ana = NovoPesquisador(1, "Ana", cincoArtigos);
            Pesquisador bruno = NovoPesquisador(2, "Bruno", cincoArtigos);
            Estudante diego = NovoEstudante(3, "Diego", cincoArtigos, NivelEstudante.Graduacao);
            Projeto projeto = new Projeto(10, "Projeto", "Descrição", SituacaoProjeto.EmAndamento, 100m, ana);

            EsperarErro(
                () => new Pesquisador(9, "Sem cinco", "Formação", "Bio", null, null, null, null, "Pretensões", CriarArtigos("x").Take(4).ToList()),
                "membro com quatro artigos deve falhar",
                typeof(ArgumentException));
            EsperarErro(
                () => new Pesquisador(9, "Com seis", "Formação", "Bio", null, null, null, null, "Pretensões", CriarArtigos("x").Concat(CriarArtigos("y")).ToList()),
                "membro com seis artigos deve falhar",
                typeof(ArgumentException));
            EsperarErro(
                () => new Artigo("Título", "Veículo", 2024, "javascript:alert(1)"),
                "link de artigo não http deve falhar",
                typeof(ArgumentException));
            EsperarErro(
                () => new Pesquisador(9, "URL inválida", "Formação", "Bio", null, "ftp://example.org/perfil", null, null, "Pretensões", cincoArtigos),
                "link científico não http deve falhar",
                typeof(ArgumentException));

            EstadoPortal valido = CriarEstado(ana, bruno, diego, projeto);
            Verificar(valido.Membros.Count == 3, "estado mínimo válido deve ser criado");

            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { ana, bruno, diego },
                    new List<Projeto> { projeto },
                    new List<VinculoOrientacao> { new VinculoOrientacao(ana, diego, true) },
                    new List<Coorientador>(),
                    new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projeto) },
                    new List<VinculoEstudanteProjeto>()),
                "estudante sem projeto deve falhar",
                typeof(InvalidOperationException));

            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { ana, bruno, diego },
                    new List<Projeto> { projeto },
                    new List<VinculoOrientacao>(),
                    new List<Coorientador>(),
                    new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projeto) },
                    new List<VinculoEstudanteProjeto> { new VinculoEstudanteProjeto(diego, projeto) }),
                "estudante sem orientação deve falhar",
                typeof(InvalidOperationException));

            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { ana, bruno, diego },
                    new List<Projeto> { projeto },
                    new List<VinculoOrientacao> { new VinculoOrientacao(ana, diego, true), new VinculoOrientacao(bruno, diego, true) },
                    new List<Coorientador>(),
                    new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projeto) },
                    new List<VinculoEstudanteProjeto> { new VinculoEstudanteProjeto(diego, projeto) }),
                "duas orientações atuais para o mesmo estudante devem falhar",
                typeof(InvalidOperationException));

            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { ana, bruno, diego },
                    new List<Projeto> { projeto },
                    new List<VinculoOrientacao> { new VinculoOrientacao(ana, diego, true) },
                    new List<Coorientador>(),
                    new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projeto) },
                    new List<VinculoEstudanteProjeto>
                    {
                        new VinculoEstudanteProjeto(diego, projeto),
                        new VinculoEstudanteProjeto(diego, projeto)
                    }),
                "vínculo estudante-projeto repetido deve falhar",
                typeof(InvalidOperationException));

            Projeto projetoFora = new Projeto(11, "Fora", "Descrição", SituacaoProjeto.EmAndamento, null, ana);
            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { ana, bruno, diego },
                    new List<Projeto> { projeto },
                    new List<VinculoOrientacao> { new VinculoOrientacao(ana, diego, true) },
                    new List<Coorientador>(),
                    new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projetoFora) },
                    new List<VinculoEstudanteProjeto> { new VinculoEstudanteProjeto(diego, projeto) }),
                "participação em projeto fora do estado deve falhar",
                typeof(InvalidOperationException));

            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { ana, bruno, diego },
                    new List<Projeto> { projeto },
                    new List<VinculoOrientacao> { new VinculoOrientacao(ana, diego, true) },
                    new List<Coorientador>(),
                    new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projeto) },
                    new List<VinculoEstudanteProjeto> { new VinculoEstudanteProjeto(NovoEstudante(99, "Fora", cincoArtigos, NivelEstudante.Graduacao), projeto) }),
                "vínculo com estudante fora do estado deve falhar",
                typeof(InvalidOperationException));

            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { diego },
                    new List<Projeto> { projeto },
                    new List<VinculoOrientacao> { new VinculoOrientacao(ana, diego, true) },
                    new List<Coorientador>(),
                    new List<ParticipacaoPesquisador>(),
                    new List<VinculoEstudanteProjeto> { new VinculoEstudanteProjeto(diego, projeto) }),
                "coordenador fora do estado deve falhar",
                typeof(InvalidOperationException));

            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { ana, bruno, diego },
                    new List<Projeto> { projeto },
                    new List<VinculoOrientacao> { new VinculoOrientacao(ana, diego, true) },
                    new List<Coorientador>
                    {
                        Coorientador.Externo(diego, "Externa"),
                        Coorientador.Externo(diego, "externa")
                    },
                    new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projeto) },
                    new List<VinculoEstudanteProjeto> { new VinculoEstudanteProjeto(diego, projeto) }),
                "coorientação externa repetida deve falhar",
                typeof(InvalidOperationException));

            Pesquisador membroDuplicado = NovoPesquisador(1, "ID duplicado", cincoArtigos);
            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { ana, bruno, diego, membroDuplicado },
                    new List<Projeto> { projeto },
                    new List<VinculoOrientacao> { new VinculoOrientacao(ana, diego, true) },
                    new List<Coorientador>(),
                    new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projeto) },
                    new List<VinculoEstudanteProjeto> { new VinculoEstudanteProjeto(diego, projeto) }),
                "IDs de membros repetidos devem falhar",
                typeof(InvalidOperationException));

            Projeto projetoDuplicado = new Projeto(10, "ID duplicado", "Descrição", SituacaoProjeto.EmAndamento, null, ana);
            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { ana, bruno, diego },
                    new List<Projeto> { projeto, projetoDuplicado },
                    new List<VinculoOrientacao> { new VinculoOrientacao(ana, diego, true) },
                    new List<Coorientador>(),
                    new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projeto) },
                    new List<VinculoEstudanteProjeto> { new VinculoEstudanteProjeto(diego, projeto) }),
                "IDs de projetos repetidos devem falhar",
                typeof(InvalidOperationException));

            EsperarErro(
                () => new EstadoPortal(
                    new List<Membro> { ana, bruno, diego },
                    new List<Projeto> { projeto },
                    new List<VinculoOrientacao>
                    {
                        new VinculoOrientacao(ana, diego, true),
                        new VinculoOrientacao(ana, diego, false)
                    },
                    new List<Coorientador>(),
                    new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projeto) },
                    new List<VinculoEstudanteProjeto> { new VinculoEstudanteProjeto(diego, projeto) }),
                "orientação repetida para o mesmo par deve falhar",
                typeof(InvalidOperationException));

            Coorientador externo = Coorientador.Externo(diego, "Dra. Externa");
            Verificar(externo.EhExterno && externo.Nome == "Dra. Externa", "coorientador externo deve manter apenas nome");
            Coorientador interno = Coorientador.Interno(diego, bruno);
            Verificar(!interno.EhExterno && interno.Nome == "Bruno", "coorientador interno deve usar o nome do pesquisador");
        }

        private static EstadoPortal CriarEstado(Pesquisador ana, Pesquisador bruno, Estudante diego, Projeto projeto)
        {
            return new EstadoPortal(
                new List<Membro> { ana, bruno, diego },
                new List<Projeto> { projeto },
                new List<VinculoOrientacao> { new VinculoOrientacao(ana, diego, true) },
                new List<Coorientador>(),
                new List<ParticipacaoPesquisador> { new ParticipacaoPesquisador(ana, projeto), new ParticipacaoPesquisador(bruno, projeto) },
                new List<VinculoEstudanteProjeto> { new VinculoEstudanteProjeto(diego, projeto) });
        }

        private static List<Artigo> CriarArtigos(string tema)
        {
            return new List<Artigo>
            {
                new Artigo("A " + tema, "Veículo 1", 2021, "https://example.org/a/1"),
                new Artigo("B " + tema, "Veículo 2", 2022, "https://example.org/a/2"),
                new Artigo("C " + tema, "Veículo 3", 2023, "https://example.org/a/3"),
                new Artigo("D " + tema, "Veículo 4", 2024, "https://example.org/a/4"),
                new Artigo("E " + tema, "Veículo 5", 2025, "https://example.org/a/5")
            };
        }

        private static Pesquisador NovoPesquisador(int id, string nome, IEnumerable<Artigo> artigos)
        {
            return new Pesquisador(id, nome, "Formação", "Bio", null, null, null, null, "Pretensões", artigos);
        }

        private static Estudante NovoEstudante(int id, string nome, IEnumerable<Artigo> artigos, NivelEstudante nivel)
        {
            return new Estudante(id, nome, "Formação", "Bio", null, null, null, null, "Pretensões", nivel, artigos);
        }

        private static void EsperarErro(Action acao, string mensagem, Type tipoEsperado)
        {
            try
            {
                acao();
            }
            catch (Exception ex)
            {
                if (tipoEsperado.IsInstanceOfType(ex))
                {
                    _verificacoes++;
                    return;
                }

                throw new Exception("Tipo de erro inesperado em '" + mensagem + "': " + ex.GetType().Name, ex);
            }

            throw new Exception("Falha esperada não ocorreu: " + mensagem);
        }

        private static void Verificar(bool condicao, string mensagem)
        {
            if (!condicao)
            {
                throw new Exception("Verificação falhou: " + mensagem);
            }

            _verificacoes++;
        }
    }
}
