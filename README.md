# Portal GPFA — Programação Web

Projeto acadêmico de portal de visibilidade científica e gestão de projetos do Grupo de Pesquisa sobre Formigas Atômicas (GPFA), em C# e ASP.NET Web Forms. Projeto novo, independente da concessionária.

## Estado real

Repositório: https://github.com/MarceloFiuza/portal-gpfa

A solução `PortalGpfa.sln` e o projeto `PortalGpfa.Web` já existem. O primeiro bloco entregou a estrutura e a navegação; o segundo, o domínio e a carga fictícia; o terceiro conectou o estado à Session, criou a camada de aplicação e implementou o diretório de membros, os perfis, a listagem de projetos e o detalhe do projeto. As páginas já exibem os dados fictícios em memória. Tudo passou apenas por verificações estáticas e por testes isolados de domínio/aplicação no Linux. **Não houve compilação nem execução em Windows**: build, IIS Express e comportamento real de Web Forms continuam pendentes de validação na universidade.

## Escopo

- Parte 1, entrega em 02/12/2026: modelo orientado a objetos, vitrine de membros e projetos, perfis, navegação, controles de servidor, Master Page, Data Binding, ciclo de vida e dados em memória.
- Parte 2, entrega em 11/12/2026: referência futura; banco SQL Server/LocalDB, CRUD persistente, tratamento de erros e relatórios. Não implementar agora.
- Autenticação, upload de arquivos, integrações com ORCID/Lattes/Scholar e deploy não são requisitos aprovados. Os endereços dos perfis científicos são links externos.

## Leitura inicial

1. `AGENTS.md`: instruções de trabalho para o agente.
2. `docs/REQUISITOS.md`: requisitos e critérios de aceitação.
3. `docs/ARQUITETURA.md`: separação de responsabilidades e estrutura física.
4. `docs/PENDENCIAS.md`: decisões resolvidas, pendências e bloqueios.
5. `docs/TAREFAS.md`: implementação e validação separadas.
6. `docs/VALIDACAO_WINDOWS.md`: roteiro executável no laboratório.
7. `docs/GITHUB.md`: criação e sincronização do repositório.
8. `docs/PROMPT_INICIAL.md`: registro histórico do primeiro comando de trabalho.

## Decisões técnicas

- C# com `LangVersion` 7.3.
- .NET Framework 4.8.
- ASP.NET Web Forms no modelo **Web Application** clássico, não ASP.NET Core.
- Solução e projeto compatíveis com Visual Studio 2022.
- Um único projeto Web, com separação lógica em pastas.
- Edição no Linux; compilação e execução reais no Windows da universidade.

A escolha técnica encerra a parte de decisão da pendência P01 original (registrada como P01a). A compatibilidade das ferramentas instaladas no laboratório (targeting pack, carga de trabalho ASP.NET, IIS Express) permanece pendente como P01b.

## Estrutura

```text
PortalGpfa.sln
PortalGpfa.Web/
  PortalGpfa.Web.csproj
  Web.config
  Site.Master / Site.Master.cs / Site.Master.designer.cs
  Default.aspx / Membros.aspx / Projetos.aspx (+ code-behind e designer)
  PerfilMembro.aspx / ProjetoDetalhe.aspx (+ code-behind e designer)
  SessaoPortal.cs      acesso central ao EstadoPortal da Session
  Rotulos.cs           rótulos de apresentação
  Urls.cs              validação de links http/https
  Conteudo/
    Site.css
    Imagens/           avatares SVG locais e fictícios
  Dominio/
    Artigo.cs
    Membro.cs / Pesquisador.cs / Estudante.cs / Tecnico.cs
    Projeto.cs
    VinculoOrientacao.cs / Coorientador.cs
    ParticipacaoPesquisador.cs / VinculoEstudanteProjeto.cs
    EstadoPortal.cs
    Guarda.cs
  Aplicacao/
    ConsultasPortal.cs consultas de leitura para as telas
  Infraestrutura/
    CargaPortalFicticio.cs
  Properties/AssemblyInfo.cs
verificacoes/DominioAplicacao/  harness isolado de domínio/aplicação (fora da solução)
```

`Dominio`, `Aplicacao` e `Infraestrutura` não referenciam `System.Web` nem Session. `SessaoPortal` é o único ponto da Web que acessa a Session. Nenhum projeto adicional faz parte da solução do produto; o harness em `verificacoes` é somente para execução isolada no Linux.

## Domínio e carga fictícia

O agregado `EstadoPortal` concentra os membros, projetos e vínculos. As entidades não mantêm listas públicas mutáveis das mesmas relações: orientações, coorientações, participações de pesquisadores e vínculos de estudantes com projetos ficam em coleções somente leitura do agregado. `EstadoPortal` valida as referências, duplicidades e cardinalidades na construção.

Invariantes aplicadas:

- todo estudante tem pelo menos um vínculo com projeto existente;
- todo estudante tem pelo menos um vínculo de orientação registrado, atual ou passado;
- cada estudante tem no máximo uma orientação marcada como atual;
- toda orientação e coorientação interna referencia pesquisador cadastrado;
- cada projeto tem exatamente um coordenador, obrigatoriamente um `Pesquisador` cadastrado;
- coorientadores externos são representados apenas por nome, sem criar membro fictício;
- todo membro possui exatamente cinco artigos de destaque, sem repetição;
- IDs de membros e projetos são únicos e os vínculos não se repetem;
- links científicos e de artigos aceitam apenas `http` ou `https`.

**Limitação de orientação:** “pelo menos um vínculo” e “no máximo um vínculo atual” permitem que um estudante tenha somente vínculos passados. Portanto, o domínio **não garante** que todo estudante atual tenha um orientador atual enquanto não houver uma definição de como distinguir estudante atual de ex-estudante. A massa mantém Felipe apenas com orientação passada para demonstrar P06; as telas exibem esse vínculo como “Passado”. Não foi criado status de ex-aluno nem regra de formatura.


A massa de `CargaPortalFicticio` é determinística e composta por 3 pesquisadores, 3 estudantes (graduação e pós-graduação), 1 técnico, 3 projetos (2 em andamento e 1 concluído), orientações atuais e passadas, coorientador interno, coorientadores externos e 5 artigos por membro. Nomes, biografias, projetos, artigos, valores e links `example.org` são explicitamente fictícios. Fotos são avatares SVG locais gerados para a demonstração.

## Sessão e fluxo de dados

1. A página chama `SessaoPortal.Obter()`.
2. `SessaoPortal` lê `EstadoPortal` da `Session`; se ausente, chama `CargaPortalFicticio.Criar()` uma vez e guarda a instância.
3. A página cria `ConsultasPortal` com essa instância e faz as consultas de leitura.
4. O Data Binding apresenta os objetos do domínio; a página apenas formata rótulos, codifica textos e valida links.

A sessão usa `mode="InProc"`: o estado fica na memória do processo do IIS Express, isolado por sessão/navegador, sem compartilhamento entre visitantes e sem persistência. Expiração da sessão (20 minutos de inatividade), reinício do IIS Express ou reciclagem do processo descartam o estado; a próxima requisição recria a carga fictícia. Não há acesso a Session em `Application_Start` e não foi criado `Global.asax`.

## Abrir no Visual Studio 2022

1. No Windows, abra `PortalGpfa.sln` com o Visual Studio 2022.
2. Confirme que a carga de trabalho **ASP.NET e desenvolvimento Web** está instalada.
3. Confirme o **.NET Framework 4.8 Developer Pack / targeting pack** e o **IIS Express**.
4. No Gerenciador de Soluções, confira `PortalGpfa.Web` como projeto de inicialização.
5. Compile com **Build > Build Solution** (ou `Ctrl+Shift+B`).
6. Execute com **IIS Express** (`F5` ou `Ctrl+F5`). O projeto usa uma URL local padrão (`http://localhost:62345/`) e não contém caminho pessoal; o Visual Studio pode ajustar a porta.

Se o Visual Studio oferecer atualização do projeto, revise o diff antes de aceitar: o alvo deve continuar `v4.8` com `LangVersion` 7.3.

## Páginas para conferir

- `/Default.aspx`: apresentação, navegação e aviso de demonstração em memória.
- `/Membros.aspx`: sete membros fictícios com nome, tipo, avatar e link para o perfil.
- `/PerfilMembro.aspx?id=1`: formação, biografia, foto, links científicos, pretensões e cinco artigos.
- `/PerfilMembro.aspx?id=1`: pesquisador com orientandos atuais e passados em listas separadas.
- `/PerfilMembro.aspx?id=4`: estudante com orientação atual, coorientador externo e dois projetos.
- `/PerfilMembro.aspx?id=6`: estudante com orientação apenas passada, sem apresentá-la como atual.
- `/PerfilMembro.aspx?id=7`: técnico sem relações de orientação.
- `/Projetos.aspx`: cards de dois projetos em andamento e um concluído.
- `/ProjetoDetalhe.aspx?id=101`: coordenador, pesquisadores, estudantes e verba em pt-BR.
- `/ProjetoDetalhe.aspx?id=103`: projeto concluído com “Financiamento não informado”.
- `/PerfilMembro.aspx?id=0`, `?id=abc` e `?id=999`: mensagem amigável e retorno.
- `/ProjetoDetalhe.aspx?id=0`, `?id=abc` e `?id=999`: mensagem amigável e retorno.
- Navegação entre as páginas, item ativo no menu, links de perfil e de projeto.
- Layout em tela larga e estreita, navegação por teclado, foco visível, contraste e textos alternativos.
- Ausência de links quebrados e de botões sem ação.

## Verificações realizadas no Linux

- Leitura de XML do `.csproj`, do `Web.config` e dos SVGs.
- Conferência dos arquivos `.cs` incluídos no `.csproj`, sem arquivos fora do projeto.
- Conferência de `Inherits`, namespace e classe dos code-behind.
- Conferência entre controles de servidor e campos dos arquivos `.designer.cs`.
- Conferência dos IDs dos `ContentPlaceHolder` entre a Master Page e as páginas.
- Conferência dos caminhos `~/`, links de navegação e imagens da massa.
- Inspeção do markup, do CSS e dos controles de Data Binding.
- Ausência de `System.Web` e de Session no domínio, na aplicação e na carga fictícia.
- Conferência da configuração `sessionState mode="InProc"` e do ponto único `SessaoPortal`.
- Teste isolado de domínio e aplicação com o harness versionado em `verificacoes/DominioAplicacao`, compilando os arquivos reais com `LangVersion` 7.3. Resultado: **105 verificações aprovadas**, incluindo composição da massa, cardinalidades, consultas de `ConsultasPortal`, orientação atual/passada e rejeição de estados inválidos. O harness também aceita Felipe com apenas vínculo passado, evidenciando a limitação descrita acima.
- Verificação de sintaxe C# 7.3 dos 30 arquivos `.cs` do produto com o parser Roslyn do SDK, sem erros de sintaxe. Isso não verifica tipos de `System.Web` nem substitui o build.

O teste foi executado sobre o SDK .NET disponível no Linux, não sobre .NET Framework 4.8. Ele demonstra a lógica de domínio e aplicação, mas não substitui o build real do projeto Web Forms. Não há MSBuild/targeting pack de .NET Framework neste Linux, e o alvo não foi alterado para contornar isso.

### Como repetir a verificação isolada

Na raiz do repositório:

```bash
dotnet run --project verificacoes/DominioAplicacao/PortalGpfa.Verificacoes.DominioAplicacao.csproj
```

O resultado esperado é `OK: 105 verificações de domínio e aplicação concluídas.` O harness fica fora de `PortalGpfa.sln`, não compila Web Forms e usa apenas os arquivos reais de domínio, infraestrutura e aplicação.

## Verificações pendentes no Windows

Roteiro detalhado: `docs/VALIDACAO_WINDOWS.md`.

- Build real da solução no .NET Framework 4.8, incluindo todas as páginas e arquivos novos.
- Execução das páginas no IIS Express e funcionamento do code-behind/Data Binding.
- Navegação completa: listagens, perfis, projetos, detalhe e retorno.
- Identificadores válidos, ausentes e malformados nas páginas de perfil e projeto.
- Orientandos atuais/passados, orientação passada de Felipe sem rótulo de atual, coorientadores e listas vazias.
- Verba formatada em pt-BR e ausência exibida como “Financiamento não informado”.
- Session InProc: isolamento entre dois navegadores, recriação após reinício/expiração e continuidade entre navegações. As telas atuais não têm controles que gerem postback, então esse cenário específico não se aplica.
- Layout responsivo, teclado, foco, contraste, imagens e ausência de HTML injetado.
- Registro da versão observada do Visual Studio e das ferramentas do laboratório em `docs/PENDENCIAS.md`.

## Desenvolvimento

Implementar somente a Parte 1 em etapas pequenas. Separar apresentação, regras e armazenamento em memória; não criar uma arquitetura empresarial desnecessária. O próximo passo é a validação no Windows e a correção do que for observado. Não confundir prévia estática, teste isolado de domínio/aplicação ou compilação com stubs com execução real de Web Forms.

## Origem das decisões

Fonte: `Projeto Final da disciplina.pdf`, fornecido pelo usuário. A data da Parte 2 considera a correção manuscrita/impressa para 11/12/2026, substituindo 16/12/2026 riscado. Datas devem ser conferidas se houver atualização do professor.

O usuário confirmou projeto novo, Linux para desenvolvimento, Windows apenas nas aulas, execução limitada à Parte 1 e, em 22/11/2026, as decisões técnicas listadas acima. A atribuição de verba por pesquisador continua sem esclarecimento.
