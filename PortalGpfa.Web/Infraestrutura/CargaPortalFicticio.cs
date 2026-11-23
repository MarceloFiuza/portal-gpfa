using System.Collections.Generic;
using PortalGpfa.Web.Dominio;

namespace PortalGpfa.Web.Infraestrutura
{
    /// <summary>
    /// Fornece uma massa determinística e fictícia para a demonstração da Parte 1.
    /// Nomes, vínculos, publicações, valores e endereços são inventados para uso acadêmico.
    /// </summary>
    public static class CargaPortalFicticio
    {
        public static EstadoPortal Criar()
        {
            Pesquisador ana = new Pesquisador(
                1,
                "Ana Beatriz Moura",
                "Doutorado em Ciência da Computação — Universidade Exemplo (fictícia)",
                "Perfil fictício criado para demonstração acadêmica. Pesquisadora de sistemas distribuídos inspirados em colônias de formigas.",
                "~/Conteudo/Imagens/ana-moura.svg",
                "https://example.org/lattes/ana-beatriz",
                "https://example.org/orcid/ana-beatriz",
                "https://example.org/scholar/ana-beatriz",
                "Sistemas distribuídos bioinspirados e formação de novos pesquisadores.",
                CriarArtigos("sistemas distribuídos bioinspirados", "ana"));

            Pesquisador bruno = new Pesquisador(
                2,
                "Bruno Tavares Lima",
                "Doutorado em Sistemas de Informação — Instituto Exemplo (fictício)",
                "Perfil fictício criado para demonstração acadêmica. Pesquisador de redes de sensores e comunicação química simulada.",
                "~/Conteudo/Imagens/bruno-lima.svg",
                "https://example.org/lattes/bruno-tavares",
                "https://example.org/orcid/bruno-tavares",
                "https://example.org/scholar/bruno-tavares",
                "Redes de sensores de baixo consumo e modelos computacionais de comunicação química.",
                CriarArtigos("redes de sensores e comunicação química", "bruno"));

            Pesquisador carla = new Pesquisador(
                3,
                "Carla Nogueira",
                "Doutorado em Engenharia de Software — Universidade Exemplo (fictícia)",
                "Perfil fictício criado para demonstração acadêmica. Pesquisadora de engenharia de software experimental e visualização de dados.",
                "~/Conteudo/Imagens/carla-nogueira.svg",
                "https://example.org/lattes/carla-nogueira",
                "https://example.org/orcid/carla-nogueira",
                "https://example.org/scholar/carla-nogueira",
                "Engenharia de software experimental e visualização de dados científicos.",
                CriarArtigos("engenharia de software experimental", "carla"));

            Estudante diego = new Estudante(
                4,
                "Diego Alves",
                "Graduação em Ciência da Computação (em curso) — Universidade Exemplo (fictícia)",
                "Perfil fictício criado para demonstração acadêmica. Estudante de graduação com interesse em sistemas distribuídos.",
                "~/Conteudo/Imagens/diego-alves.svg",
                "https://example.org/lattes/diego-alves",
                "https://example.org/orcid/diego-alves",
                "https://example.org/scholar/diego-alves",
                "Sistemas distribuídos e monitoramento de sensores.",
                NivelEstudante.Graduacao,
                CriarArtigos("monitoramento distribuído de sensores", "diego"));

            Estudante elisa = new Estudante(
                5,
                "Elisa Prado",
                "Mestrado em Ciência da Computação (em curso) — Universidade Exemplo (fictícia)",
                "Perfil fictício criado para demonstração acadêmica. Estudante de pós-graduação com interesse em comunicação química simulada.",
                "~/Conteudo/Imagens/elisa-prado.svg",
                "https://example.org/lattes/elisa-prado",
                "https://example.org/orcid/elisa-prado",
                "https://example.org/scholar/elisa-prado",
                "Comunicação química simulada e avaliação experimental de protocolos.",
                NivelEstudante.PosGraduacao,
                CriarArtigos("comunicação química simulada", "elisa"));

            Estudante felipe = new Estudante(
                6,
                "Felipe Ramos",
                "Graduação em Sistemas de Informação (concluída) — Instituto Exemplo (fictício)",
                "Perfil fictício criado para demonstração acadêmica. Ex-estudante de graduação com vínculo de orientação registrado como passado.",
                "~/Conteudo/Imagens/felipe-ramos.svg",
                "https://example.org/lattes/felipe-ramos",
                "https://example.org/orcid/felipe-ramos",
                "https://example.org/scholar/felipe-ramos",
                "Análise de trajetórias e visualização de dados urbanos.",
                NivelEstudante.Graduacao,
                CriarArtigos("análise de trajetórias urbanas", "felipe"));

            Tecnico helena = new Tecnico(
                7,
                "Helena Duarte",
                "Técnica em Laboratório de Sistemas — Instituto Exemplo (fictício)",
                "Perfil fictício criado para demonstração acadêmica. Técnica responsável pela preparação de experimentos e organização do laboratório.",
                "~/Conteudo/Imagens/helena-duarte.svg",
                "https://example.org/lattes/helena-duarte",
                "https://example.org/orcid/helena-duarte",
                "https://example.org/scholar/helena-duarte",
                "Apoio técnico a experimentos de redes e organização de dados de laboratório.",
                CriarArtigos("apoio técnico a experimentos de redes", "helena"));

            List<Membro> membros = new List<Membro> { ana, bruno, carla, diego, elisa, felipe, helena };

            Projeto colonias = new Projeto(
                101,
                "Colônias Artificiais e Observação Distribuída",
                "Projeto fictício sobre observação de colônias artificiais com sensores distribuídos e registro de trajetórias.",
                SituacaoProjeto.EmAndamento,
                120000.00m,
                ana);

            Projeto sensores = new Projeto(
                102,
                "Rede de Sensores Inspirada em Formigas",
                "Projeto fictício sobre protocolos de comunicação de baixo consumo inspirados em interações de colônias de formigas.",
                SituacaoProjeto.EmAndamento,
                85000.00m,
                bruno);

            Projeto trilhas = new Projeto(
                103,
                "Trilhas Urbanas e Memória Coletiva",
                "Projeto fictício concluído sobre análise de trajetórias urbanas e visualização de padrões coletivos.",
                SituacaoProjeto.Concluido,
                null,
                carla);

            List<Projeto> projetos = new List<Projeto> { colonias, sensores, trilhas };

            List<VinculoOrientacao> orientacoes = new List<VinculoOrientacao>
            {
                new VinculoOrientacao(ana, diego, true),
                new VinculoOrientacao(ana, felipe, false),
                new VinculoOrientacao(bruno, elisa, true)
            };

            List<Coorientador> coorientadores = new List<Coorientador>
            {
                Coorientador.Externo(diego, "Dra. Patrícia Nunes — Universidade Exemplo (externa fictícia)"),
                Coorientador.Interno(elisa, carla),
                Coorientador.Externo(elisa, "Dr. Otávio Ramos — Instituto Exemplo (externo fictício)")
            };

            List<ParticipacaoPesquisador> participacoes = new List<ParticipacaoPesquisador>
            {
                new ParticipacaoPesquisador(ana, colonias),
                new ParticipacaoPesquisador(bruno, colonias),
                new ParticipacaoPesquisador(bruno, sensores),
                new ParticipacaoPesquisador(carla, sensores),
                new ParticipacaoPesquisador(carla, trilhas)
            };

            List<VinculoEstudanteProjeto> vinculosEstudantes = new List<VinculoEstudanteProjeto>
            {
                new VinculoEstudanteProjeto(diego, colonias),
                new VinculoEstudanteProjeto(diego, sensores),
                new VinculoEstudanteProjeto(elisa, sensores),
                new VinculoEstudanteProjeto(felipe, trilhas)
            };

            return new EstadoPortal(
                membros,
                projetos,
                orientacoes,
                coorientadores,
                participacoes,
                vinculosEstudantes);
        }

        private static IReadOnlyList<Artigo> CriarArtigos(string tema, string identificador)
        {
            string[] titulos =
            {
                "Panorama fictício de " + tema,
                "Métodos de laboratório para " + tema,
                "Modelagem didática de " + tema,
                "Estudo de caso simulado sobre " + tema,
                "Perspectivas de pesquisa em " + tema
            };

            string[] veiculos =
            {
                "Caderno de Estudos Fictícios",
                "Revista Exemplo de Computação",
                "Anais do Simpósio Imaginário",
                "Boletim de Laboratório Exemplar",
                "Coleção Didática de Pesquisa"
            };

            int[] anos = { 2021, 2022, 2023, 2024, 2025 };

            List<Artigo> artigos = new List<Artigo>();
            for (int i = 0; i < titulos.Length; i++)
            {
                artigos.Add(new Artigo(
                    titulos[i],
                    veiculos[i],
                    anos[i],
                    "https://example.org/artigos/" + identificador + "/" + (i + 1)));
            }

            return artigos;
        }
    }
}
