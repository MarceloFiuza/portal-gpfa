# Validação no Windows — Parte 1

Roteiro curto para o laboratório. Não substitui a leitura de `README.md` e `docs/TAREFAS.md`.

## Situação

| Etapa | Situação |
| --- | --- |
| Implementação | Domínio, carga fictícia, sessão InProc, diretório, perfis, projetos, detalhe, CSS e mensagens de ID inválido implementados. |
| Verificação no Linux | XML/estrutura, 105 verificações isoladas de domínio/aplicação e sintaxe C# 7.3 conferidos. Web Forms não compilado. |
| Pendente no Windows | Build .NET Framework 4.8, execução no IIS Express, comportamento real de Data Binding, navegação, layout e sessão. |

## Pré-requisitos

- Windows com Visual Studio 2022.
- Carga de trabalho **ASP.NET e desenvolvimento Web**.
- **.NET Framework 4.8 Developer Pack / targeting pack**.
- **IIS Express**.
- Navegador atualizado.

## Abrir, compilar e executar

1. Abra `PortalGpfa.sln` no Visual Studio 2022.
2. Confirme `PortalGpfa.Web` como projeto de inicialização.
3. Compile com **Build > Build Solution** (`Ctrl+Shift+B`) e registre erros/avisos.
4. Execute com **IIS Express** (`F5` ou `Ctrl+F5`).
5. Use a URL exibida pelo Visual Studio. O projeto tem URL local padrão `http://localhost:62345/`; a porta pode variar.

Se o Visual Studio sugerir atualização do projeto, revise antes de aceitar. O alvo deve permanecer `.NET Framework 4.8` e `LangVersion` 7.3.

## Massa e URLs

Base: `http://localhost:<porta>/` (substitua pela porta exibida).

| URL | Resultado esperado |
| --- | --- |
| `/Default.aspx` | Página inicial, navegação e aviso de demonstração em memória. |
| `/Membros.aspx` | Sete cartões fictícios: Ana, Bruno, Carla, Diego, Elisa, Felipe e Helena; nome, tipo, avatar e link de perfil. |
| `/PerfilMembro.aspx?id=1` | Ana (pesquisadora): formação, biografia, foto, links Lattes/ORCID/Scholar, pretensões e 5 artigos. Orientandos atuais: Diego. Passados: Felipe. Projetos: 101. |
| `/PerfilMembro.aspx?id=2` | Bruno (pesquisador): orientanda atual Elisa; 5 artigos; projetos 101 e 102. |
| `/PerfilMembro.aspx?id=3` | Carla (pesquisadora): 5 artigos; projetos 102 e 103. |
| `/PerfilMembro.aspx?id=4` | Diego (graduação): orientação atual Ana; coorientadora externa; projetos 101 e 102; 5 artigos. |
| `/PerfilMembro.aspx?id=5` | Elisa (pós-graduação): orientação atual Bruno; coorientadores Carla (interna) e Otávio (externo); projeto 102; 5 artigos. |
| `/PerfilMembro.aspx?id=6` | Felipe (graduação): orientação **Passado** com Ana, sem orientador atual; projeto 103; 5 artigos. Não deve aparecer “Atual”. |
| `/PerfilMembro.aspx?id=7` | Helena (técnica): perfil e artigos; aviso de ausência de relações acadêmicas. |
| `/Projetos.aspx` | Três cartões: 101 e 102 em andamento; 103 concluído; links para detalhe. |
| `/ProjetoDetalhe.aspx?id=101` | Coordenador Ana; pesquisadores Ana e Bruno; estudante Diego; financiamento `R$ 120.000,00`. |
| `/ProjetoDetalhe.aspx?id=102` | Coordenador Bruno; pesquisadores Bruno e Carla; estudantes Diego e Elisa; financiamento `R$ 85.000,00`. |
| `/ProjetoDetalhe.aspx?id=103` | Coordenador Carla; pesquisadora Carla; estudante Felipe; “Financiamento não informado”. |

Todos os nomes, artigos, projetos, valores e links `example.org` são fictícios.

## Identificadores inválidos

Teste em `/PerfilMembro.aspx` e `/ProjetoDetalhe.aspx`:

- sem `id`;
- `?id=0`;
- `?id=-1`;
- `?id=abc`;
- `?id=999`.

Resultado esperado: mensagem amigável (“Identificador inválido” ou “Membro/Projeto não encontrado”), link de retorno para a listagem, sem detalhes internos de exceção. Confira no navegador/DevTools que o status HTTP é 404.

## Orientação, financiamento e apresentação

- Em `/PerfilMembro.aspx?id=6`, o vínculo com Ana deve estar rotulado **Passado**; não deve existir orientação atual.
- Em `/PerfilMembro.aspx?id=4` e `?id=5`, a orientação atual deve estar rotulada **Atual**.
- Em `/ProjetoDetalhe.aspx?id=101` e `?id=102`, o valor deve usar formato pt-BR.
- Em `/ProjetoDetalhe.aspx?id=103`, deve aparecer exatamente “Financiamento não informado”, sem zero.
- Não deve existir cálculo de verba por pesquisador.

## Navegação, teclado e layout

1. Percorra `Início → Membros → perfil → projetos → detalhe` e use os links de retorno.
2. Confira o item ativo do menu e o link “Pular para o conteúdo”.
3. Navegue apenas com `Tab`, `Shift+Tab` e `Enter`; confira foco visível.
4. Teste larguras aproximadas de 360 px e 1280 px.
5. Confira contraste, textos alternativos dos avatares e ausência de rolagem horizontal.
6. Confira que os links externos são http/https e abrem em nova aba quando aplicável.

## Sessão

- As telas atuais **não têm controles que gerem postback** (não há botões, formulários de manutenção ou ações de escrita). Portanto, o cenário “postback reutiliza a mesma instância” não se aplica a esta versão.
- Abra a mesma página em dois navegadores ou em uma janela privada. Confirme que ambos carregam e que os cookies `ASP.NET_SessionId` são diferentes (DevTools → Application/Storage → Cookies).
- Navegue entre páginas na mesma janela e confirme a continuidade dos dados.
- Reinicie o IIS Express (ou aguarde o timeout de 20 minutos) e recarregue. As páginas devem voltar a carregar com a carga fictícia recriada.
- Sem controles de escrita ou instrumentação de diagnóstico, a identidade interna da instância e o isolamento de conteúdo não podem ser observados diretamente; registre essa limitação.

## Registro

Anote no README e em `docs/PENDENCIAS.md`: versão do Visual Studio, presença do targeting pack 4.8, resultado do build, páginas executadas, erros encontrados, resultado dos IDs inválidos e observações de sessão. Não marque a Parte 1 como integralmente validada antes de concluir este roteiro.
