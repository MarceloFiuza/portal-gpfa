# Arquitetura e modelo

## Direção

Separação simples entre apresentação Web Forms, regras/casos de uso e armazenamento. “Arquitetura limpa” significa responsabilidades claras e dependências controladas; não exige implementar um catálogo de padrões. A escolha técnica foi definida em 22/11/2026: um único projeto Web (`PortalGpfa.Web`) em .NET Framework 4.8, C# 7.3 e modelo Web Application, com separação lógica em pastas. A compatibilidade do laboratório (P01b) ainda será conferida.

## Estrutura física atual

```text
PortalGpfa.sln
PortalGpfa.Web/
  SessaoPortal.cs      ponto único de acesso à Session (InProc)
  Rotulos.cs           rótulos de apresentação
  Urls.cs              validação de http/https
  Dominio/
    Artigo.cs
    Membro.cs (abstrato)
    Pesquisador.cs
    Estudante.cs (com NivelEstudante)
    Tecnico.cs
    Projeto.cs (com SituacaoProjeto)
    VinculoOrientacao.cs
    Coorientador.cs
    ParticipacaoPesquisador.cs
    VinculoEstudanteProjeto.cs
    EstadoPortal.cs (agregado e validação)
    Guarda.cs (validações de argumento)
  Aplicacao/
    ConsultasPortal.cs (consultas de leitura para as telas)
  Infraestrutura/
    CargaPortalFicticio.cs (fábrica determinística)
  Conteudo/
    Site.css
    Imagens/        avatares SVG fictícios
  Properties/       AssemblyInfo
  Site.Master       layout, navegação e ContentPlaceHolders
  Default.aspx      apresentação do portal
  Membros.aspx      diretório com Data Binding
  PerfilMembro.aspx perfil individual
  Projetos.aspx     cards de projetos
  ProjetoDetalhe.aspx detalhe do projeto
verificacoes/DominioAplicacao/ harness isolado Linux (fora da solução)
```

`Dominio`, `Aplicacao` e `Infraestrutura` não referenciam `System.Web` nem Session. A Web acessa o estado somente por `SessaoPortal.Obter()`. Não há repositório nem interface genérica nesta etapa.

## Responsabilidades

| Área lógica | Responsabilidade | Não deve conter |
| --- | --- | --- |
| Domínio | Entidades, relacionamentos e invariantes | Session, System.Web, controles ou SQL |
| Aplicação | Consultas de leitura para as telas e, no futuro, operações autorizadas | HTML, Page, acesso direto a Session |
| Infraestrutura em memória | Carga fictícia coerente; no futuro, repositórios específicos e armazenamento temporário | Regras de interface ou cálculo financeiro não definido |
| Web | Master Page, páginas, code-behind, binding e adaptação do estado da sessão | SQL ou regras acadêmicas dispersas |

Aplicação depende apenas do domínio e recebe `EstadoPortal` no construtor. Infraestrutura fornece a carga fictícia, sem repositório nesta etapa. Web compõe a instância da sessão, chama a aplicação e apresenta os resultados; somente Web conhece Session. Não criar abstrações genéricas sem uso.

## Modelo implementado

- `Membro` abstrato: identidade, nome, formação, biografia, foto local, links científicos, pretensões e exatamente cinco artigos de destaque.
- `Pesquisador : Membro`: papel elegível para orientação principal e coordenação.
- `Estudante : Membro`: nível (`Graduacao` ou `PosGraduacao`); os vínculos de orientação e de projeto ficam no agregado.
- `Tecnico : Membro`: perfil público, sem função automática de pesquisador.
- `Projeto`: título, descrição, situação (`EmAndamento`/`Concluido`), verba captada opcional e um coordenador `Pesquisador`.
- `Artigo`: título, veículo, ano e link http/https opcional. A massa não usa DOI real; quando necessário, o endereço é de exemplo.
- `VinculoOrientacao`: pesquisador orientador, estudante e indicação `Atual` ou passada.
- `Coorientador`: sempre ligado a um estudante; interno referencia `Pesquisador`, externo guarda apenas o nome. As fábricas `Interno` e `Externo` impedem duas fontes de nome.
- `ParticipacaoPesquisador` e `VinculoEstudanteProjeto`: relações N:N entre membros e projetos.

O agregado `EstadoPortal` é a única fonte das coleções de vínculos. Expõe listas somente leitura e consultas como projetos de um estudante, pesquisadores/estudantes de um projeto, orientandos atuais/passados e coorientadores. As entidades não mantêm listas paralelas dos mesmos vínculos.

Invariantes verificadas na construção do agregado:

- todo estudante tem ao menos um projeto existente e ao menos um vínculo de orientação, atual ou passado;
- cada estudante tem no máximo uma orientação atual, evitando dois orientadores principais simultâneos;
- orientações e coorientações internas referenciam pesquisador cadastrado;
- coorientadores externos não exigem cadastro de pesquisador;
- cada projeto tem exatamente um coordenador pesquisador cadastrado — a cardinalidade é estrutural, pois `Projeto` possui uma única propriedade `Coordenador`;
- cada membro tem exatamente cinco artigos, sem repetir o mesmo artigo;
- IDs e vínculos não se repetem;
- links científicos e de artigos são http/https.

**Limitação conhecida:** as regras “pelo menos um vínculo” e “no máximo um atual” não impedem que um estudante tenha somente vínculos passados. Como não existe status de estudante atual nem definição de ex-estudante (P06), o domínio não consegue exigir orientador atual para todo estudante atual. A massa mantém Felipe apenas com vínculo passado; as telas o exibem como “Passado”. Não criar status de ex-aluno, transição ou formatura para contornar essa pendência.

Não criar subclasses sem propriedades ou regras distintas. Não inventar restrições de exclusividade de papéis, vínculo do orientador ao mesmo projeto, quantidade mínima de pesquisadores participantes ou obrigação de todo projeto estar em andamento.

## Estado da Parte 1

A massa é criada por `CargaPortalFicticio.Criar()`, uma fábrica determinística em `Infraestrutura`, e validada pelo construtor de `EstadoPortal`. A Web usa `SessaoPortal.Obter()`: consulta a `Session` pela chave `PortalGpfa.EstadoPortal`; se ausente, cria a carga e guarda a instância; se presente, reutiliza a mesma referência. Não há recarga em postbacks e não há acesso a Session em `Application_Start`.

A configuração é `sessionState mode="InProc"`, com timeout de 20 minutos. O estado é isolado por sessão/navegador, não compartilhado entre visitantes e não persistente. Expiração, reinício do IIS Express ou reciclagem do processo descartam os dados; a próxima requisição recria a carga fictícia.

`ConsultasPortal` recebe o `EstadoPortal` e expõe as consultas de leitura usadas pelas telas. Páginas fazem Data Binding apenas no carregamento inicial (`!IsPostBack`), formatam rótulos no code-behind e não contêm consultas de domínio.

## Orientação principal e P06

`ValidarOrientacoes` exige pelo menos um vínculo de orientação e no máximo um vínculo marcado como atual. Essas regras **não** garantem orientador atual obrigatório: um estudante pode ter somente vínculos passados. Sem uma definição de “estudante atual” e de como tratar ex-estudantes (P06), não é possível impor essa obrigatoriedade sem inventar status ou regra de formatura. A demonstração mantém Felipe apenas com orientação passada e a tela usa o rótulo “Passado”; Diego e Elisa têm orientação atual na massa, mas isso é dado de demonstração, não uma garantia do domínio. P06 continua aberta.

## Testes do domínio e da aplicação

O domínio e a aplicação são verificados isoladamente no Linux pelo harness versionado em `verificacoes/DominioAplicacao`, compilando os arquivos reais com `LangVersion` 7.3. Foram aprovadas 105 verificações: composição da massa, contagens, orientações atuais/passadas, coorientação interna/externa, participações, consultas de `ConsultasPortal` e rejeição de estados inválidos. O harness também aceita Felipe somente com vínculo passado, o que evidencia a limitação de orientação atual descrita acima. Além disso, o parser Roslyn do SDK analisou a sintaxe C# 7.3 dos 30 arquivos `.cs` do produto sem erros. Esses testes não validam o projeto Web Forms nem o alvo .NET Framework 4.8 no Windows; o harness está fora de `PortalGpfa.sln` e não altera o alvo do produto.

## Páginas implementadas

- `Default.aspx`: apresentação e navegação.
- `Membros.aspx`: diretório com Repeater, nome, tipo, avatar e link para perfil.
- `PerfilMembro.aspx?id=...`: dados do membro, cinco artigos, links científicos e relações aplicáveis; orientandos atuais/passados do pesquisador e orientação/coorientadores/projetos do estudante.
- `Projetos.aspx`: cards com situação e link para detalhe.
- `ProjetoDetalhe.aspx?id=...`: título, descrição, situação, coordenador, pesquisadores, estudantes e verba do projeto.

IDs são lidos por `int.TryParse`, exigindo valor positivo. Ausente, malformado ou inexistente gera mensagem amigável, status 404 e link de retorno. Textos dinâmicos usam `Literal Mode="Encode"` ou `<%#: %>`; links externos passam por `Urls.EhHttp` antes de virar `NavigateUrl`.

## Web Forms

Master Page concentra cabeçalho, navegação, rodapé e ContentPlaceHolders. As listagens usam `Repeater` com Data Binding real; o binding ocorre sob `!IsPostBack`, e os painéis de estado vazio só aparecem quando a coleção está vazia. Não atribuir DataBind indiscriminadamente a cada Page_Load. Conferir IsPostBack, eventos, ViewState quando aplicável e atualização deliberada de dados.

## Interface

CSS simples e responsivo, navegação por teclado, foco visível, headings organizados, labels nos campos existentes, contraste e alternativas textuais. Evitar dependência de CDN para a demonstração no laboratório; biblioteca visual não é obrigatória. Não produzir aplicativo paralelo em HTML para substituir a entrega Web Forms.

## Evolução futura

Na Parte 2, trocar implementação dos repositórios sem mover regras para code-behind. SQL Server/LocalDB é o destino do enunciado; versão e ADO.NET/ORM permanecem em aberto. Não implementar essa troca antecipadamente.
