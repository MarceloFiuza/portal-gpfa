# Plano incremental — Parte 1

Legenda: `[ ]` pendente; `[x]` concluído com evidência. Implementação e validação são registradas separadamente: criar arquivos não conclui teste em Windows. Este documento não atesta funcionamento do Web Forms.

## T00 — Preparação e ambiente

- [x] Extrair os arquivos na nova pasta e criar o repositório conforme GITHUB.md. Evidência: repositório em https://github.com/MarceloFiuza/portal-gpfa com um commit inicial em `main`.
- [x] Conferir instruções e estado real dos arquivos. Evidência: `AGENTS.md` e `docs/` lidos; árvore de trabalho limpa e `main` sincronizada com `origin/main`; no estado inicial, ausência de solução, projeto ou código.
- [x] Registrar a decisão técnica de 22/11/2026. Evidência: C# 7.3, .NET Framework 4.8, ASP.NET Web Forms no modelo Web Application, Visual Studio 2022, um projeto Web; P01a resolvida em `docs/PENDENCIAS.md`.
- [ ] Conferir P01b no computador da universidade: Visual Studio, targeting pack 4.8, carga de trabalho ASP.NET, IIS Express e restrições.
- [ ] Confirmar comandos reais de compilação/execução no ambiente do laboratório.

P01b não bloqueia mais a implementação da estrutura, mas bloqueia declarar build e execução aprovados.

## T01 — Estrutura e navegação (implementação)

Sem bloqueio de escolha técnica desde P01a.

- [x] Criar `PortalGpfa.sln` e `PortalGpfa.Web` com alvo `.NET Framework 4.8`, `LangVersion` 7.3 e modelo Web Application. Evidência: arquivos no repositório; XML válido conferido no Linux.
- [x] Separar `Dominio`, `Aplicacao`, `Infraestrutura` e apresentação Web, sem classes vazias nem projetos adicionais. Evidência: estrutura separada; naquele bloco, nenhum tipo fictício foi criado.
- [x] Criar Master Page com cabeçalho, navegação, rodapé e ContentPlaceHolders. Evidência: `Site.Master` e code-behind.
- [x] Criar página inicial, página de membros e página de projetos com estado vazio informativo. Evidência: `Default.aspx`, `Membros.aspx`, `Projetos.aspx`.
- [x] Criar CSS local simples, responsivo e acessível. Evidência: `Conteudo/Site.css`.
- [x] Verificar no Linux estrutura, XML, referências do `.csproj`, namespaces, `Inherits`, controles/designer e correspondência dos ContentPlaceHolders. Evidência: script de conferência executado; resultados no README.
- [ ] Compilar e executar no Windows.

## T01-V — Validação no Windows

- [ ] Abrir `PortalGpfa.sln` no Visual Studio 2022 sem erro de projeto.
- [ ] Confirmar carga de trabalho ASP.NET, targeting pack 4.8 e IIS Express instalados.
- [ ] Build da solução sem erros.
- [ ] Executar pelo IIS Express e abrir as três páginas.
- [ ] Conferir Master Page, navegação, item ativo, links e `Page_Load` do rodapé.
- [ ] Conferir layout estreito/largo, teclado, foco, contraste e ausência de links quebrados.
- [ ] Registrar versão observada do ambiente e resultado em `docs/PENDENCIAS.md` e no README.

Não marcar esta seção com base em inspeção estática ou prévia HTML.

## T02 — Modelo e massa de dados (implementação)

Revisão estática de T01 feita. T01-V continua pendente e não bloqueia a implementação do domínio.

- [x] Implementar entidades e relacionamentos aprovados em `Dominio`, sem dependência de `System.Web`, Session, controles ou SQL.
- [x] Criar massa fictícia determinística em `Infraestrutura/CargaPortalFicticio.cs`, cobrindo pesquisadores, estudantes de graduação e pós-graduação, técnico, projetos em andamento/concluídos, orientações atuais/passadas, coorientador interno e externo e participações.
- [x] Garantir exatamente cinco artigos por membro e validar referências, IDs, duplicidades, estudante com projeto e orientação, coordenador pesquisador e no máximo uma orientação atual por estudante na construção de `EstadoPortal`.
- [x] Verificar invariantes com testes isolados no Linux, compilando os arquivos reais de domínio, aplicação e carga com `LangVersion` 7.3. Evidência: harness versionado em `verificacoes/DominioAplicacao`; **105 verificações aprovadas**, incluindo estados inválidos rejeitados. O harness aceita Felipe somente com vínculo passado, evidenciando a limitação de P06.
- [x] Conectar o estado à Session por ponto central `SessaoPortal.Obter()`, inicializando com `CargaPortalFicticio.Criar()` somente quando ausente e reutilizando a mesma instância. Evidência: `SessaoPortal.cs` e `sessionState mode="InProc"` no `Web.config`.

## T02-V — Validação do domínio no ambiente alvo

- [ ] Compilar domínio, aplicação e carga dentro do projeto `.NET Framework 4.8` no Visual Studio 2022.
- [ ] Reexecutar as verificações de invariantes no ambiente alvo, se houver ferramenta de teste disponível.
- [ ] Confirmar que o build do projeto Web Forms inclui os novos arquivos.

O teste isolado no Linux não valida o build .NET Framework nem o comportamento Web Forms.

**P06 permanece aberta:** as regras atuais exigem ao menos um vínculo e no máximo um atual, mas permitem somente vínculos passados. Não exigir vínculo atual enquanto não houver definição de estudante atual/ex-estudante; a massa mantém Felipe apenas com orientação passada.

Não implementar cadastro incompleto, transições históricas ou cálculo de verba por pesquisador.

## T03 — Diretório e perfis (implementação)

- [x] Listar os sete membros com nome, tipo, avatar e link usando `Repeater` e Data Binding em `Membros.aspx`.
- [x] Criar `PerfilMembro.aspx?id=...` com identificação, formação, biografia, foto, links, pretensões e cinco artigos.
- [x] Exibir relações por tipo: orientandos atuais/passados do pesquisador; orientação/coorientadores/projetos do estudante; aviso para técnico.
- [x] Tratar identificador ausente, malformado ou inexistente com mensagem e link de retorno; status 404.
- [x] Codificar textos dinâmicos (`Literal Mode="Encode"` e `<%#: %>`) e validar links científicos com `Urls.EhHttp`.
- [x] Exibir orientação passada com rótulo “Passado”, sem apresentá-la como atual.

## T04 — Projetos (implementação)

- [x] Exibir cards com título, descrição, situação e link para detalhe em `Projetos.aspx`.
- [x] Criar `ProjetoDetalhe.aspx?id=...` com título, descrição, situação, coordenador, pesquisadores, estudantes e verba.
- [x] Formatar verba em pt-BR e exibir “Financiamento não informado” quando ausente.
- [x] Não exibir cálculo de verba por pesquisador.
- [x] Tratar identificador inválido ou inexistente com mensagem e retorno.

## T05 — Interface, ciclo de vida e sessão (implementação)

- [x] Atualizar o CSS local para cards de membros/projetos, perfil, listas de relações e detalhe.
- [x] Fazer binding somente sob `!IsPostBack` e exibir estado vazio apenas quando a coleção estiver vazia.
- [x] Centralizar a Session em `SessaoPortal`, sem acesso em `Application_Start`.
- [x] Manter avatares, CSS e demais recursos locais.
- [ ] Testar navegação completa e retorno às listagens no Windows.
- [ ] Testar requests/postbacks existentes sem recriação indevida dos dados.
- [ ] Verificar isolamento entre sessões e comportamento após expiração.
- [ ] Conferir layout em tela estreita e larga, teclado, foco, contraste e imagens.
- [ ] Conferir visualmente a ausência de HTML injetado nos dados exibidos.

## T05-V — Validação de dados e sessão no Windows

- [ ] Build e execução no IIS Express.
- [ ] Abrir membros, cada perfil, projetos e cada detalhe pelos links.
- [ ] Testar `id` ausente, `0`, `abc` e inexistente nas duas páginas de detalhe.
- [ ] Conferir Ana com Diego atual e Felipe passado; Felipe sem orientador atual.
- [ ] Conferir verba em pt-BR e “Financiamento não informado” no projeto 103.
- [ ] Confirmar que não há controles que gerem postback; esse cenário não se aplica às telas atuais. Observar a continuidade do estado entre navegações.
- [ ] Abrir duas sessões/navegadores e confirmar isolamento.
- [ ] Reiniciar o IIS Express ou aguardar a expiração e confirmar recriação da carga.
- [ ] Registrar ambiente, resultados e limitações no README e em PENDENCIAS.

Não criar botões de escrita apenas para gerar um postback; verificar os controles efetivamente implementados.

## T06 — Validação na universidade e fechamento

- [ ] Build real sem erros no ambiente alvo.
- [ ] Execução real de todas as páginas e vínculos.
- [ ] Conferência de RN01–RN06 e RF01–RF07.
- [ ] Documentar comandos reais, ambiente, resultados e limitações no README.
- [ ] Registrar o que foi verificado no Linux e o que foi verificado em Windows, separadamente.
- [ ] Revisar os quatro critérios da rubrica e preparar roteiro curto de demonstração.

Não marcar concluído com base apenas em inspeção estática, stubs ou prévia HTML.

## Fora da execução atual

Parte 2: modelagem relacional, persistência, CRUD e relatórios financeiros. Será planejada depois, mediante solicitação e esclarecimento das pendências pertinentes.
