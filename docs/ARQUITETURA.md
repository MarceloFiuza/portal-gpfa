# Arquitetura inicial proposta

## Direção

Separação simples entre apresentação Web Forms, regras/casos de uso e armazenamento. “Arquitetura limpa” significa responsabilidades claras e dependências controladas; não exige implementar um catálogo de padrões. A estrutura física da solução só será definida após confirmar ferramentas e tipo de projeto no Windows.

## Responsabilidades

| Área lógica | Responsabilidade | Não deve conter |
| --- | --- | --- |
| Domínio | Entidades, relacionamentos e invariantes | Session, System.Web, controles ou SQL |
| Aplicação | Consultas para telas e operações autorizadas; validação coordenada das relações | HTML, Page, acesso direto a Session |
| Infraestrutura em memória | Repositórios específicos, carga fictícia coerente e armazenamento temporário | Regras de interface ou cálculo financeiro não definido |
| Web | Master Page, páginas, code-behind, binding e adaptação do estado da sessão | SQL ou regras acadêmicas dispersas |

Aplicação depende do domínio e de contratos mínimos de repositório. Infraestrutura implementa esses contratos. Web compõe os objetos e chama a aplicação. Somente Web conhece Session; fornece o estado em memória aos repositórios. Não criar abstrações genéricas sem uso.

## Modelo conceitual proposto

- `Membro` abstrato: identidade, nome, formação, biografia, foto, links científicos, pretensões e artigos destacados.
- `Pesquisador : Membro`: papel elegível para orientação principal e coordenação.
- `Estudante : Membro`: nível (graduação/pós-graduação), orientador principal e vínculos com projetos.
- `Tecnico : Membro`: perfil público; não recebe automaticamente funções de pesquisador.
- `Projeto`: título, descrição, situação, financiamento opcional, coordenador pesquisador e relações de participação.
- `Artigo`: título, veículo, ano e DOI/link. Associação ao membro não exige duplicar um mesmo artigo compartilhado; representação concreta deve permanecer simples.
- `VinculoOrientacao`: pesquisador, estudante e indicação de atual/passado; permite separar listas sem apagar o histórico.
- `Coorientador`: nome, permitindo representar pessoa externa sem cadastro obrigatório como pesquisador.
- Vínculos estudante–projeto e pesquisador–projeto: N:N, com referências válidas.

Esta hierarquia é proposta técnica, não texto literal do enunciado. Não criar subclasses sem propriedades ou regras distintas além dessas. Não inventar restrições de exclusividade de papéis, vínculo do orientador ao mesmo projeto, quantidade mínima de pesquisadores participantes ou demais regras não explicitadas. Ver dúvidas de históricos em PENDENCIAS.

## Estado da Parte 1

Escolha técnica inicial: um agregado `EstadoPortal` por Session com membros, projetos e relações consistentes. Inicialização única, com guarda contra valor ausente; não recarregar a massa em cada request. Cada navegador/sessão possui cópia independente; dados não são compartilhados nem persistentes. Expiração ou reinício pode perder alterações. Essa escolha usa uma das opções admitidas pelo enunciado.

Não usar Session como fachada de domínio. A criação/recuperação ocorre na Web, e serviços recebem dependências normais. Carga fictícia precisa respeitar todas as regras, inclusive cinco artigos por membro, estudantes vinculados e coordenador único.

## Páginas propostas

- Início: apresentação e navegação.
- Membros: diretório, identificação do tipo e links para perfis.
- Perfil: dados do membro, cinco artigos e relações aplicáveis; pesquisador com orientandos atuais/passados.
- Projetos: cards para projetos em andamento e concluídos.
- Detalhe do projeto: informações, coordenador, participantes e estudantes vinculados.

Nomes físicos de arquivos serão definidos na criação do projeto. IDs de URL precisam ser validados; registro inexistente gera mensagem amigável e retorno, sem detalhes internos de exceção.

## Web Forms

Master Page concentra cabeçalho, navegação, rodapé e ContentPlaceHolders. Repeater/ListView/GridView podem ser usados conforme a apresentação, sem escolher todos por obrigação. Não atribuir DataBind indiscriminadamente a cada Page_Load. Conferir IsPostBack, eventos, ViewState quando aplicável e atualização deliberada de dados.

## Interface

CSS simples e responsivo, navegação por teclado, foco visível, headings organizados, labels nos campos existentes, contraste e alternativas textuais. Evitar dependência de CDN para a demonstração no laboratório; biblioteca visual não é obrigatória. Não produzir aplicativo paralelo em HTML para substituir a entrega Web Forms.

## Evolução futura

Na Parte 2, trocar implementação dos repositórios sem mover regras para code-behind. SQL Server/LocalDB é o destino do enunciado; versão e ADO.NET/ORM permanecem em aberto. Não implementar essa troca antecipadamente.
