# Plano incremental — Parte 1

Legenda: `[ ]` pendente; `[x]` concluído com evidência. Este documento não atesta implementação. A documentação inicial foi preparada, mas ainda precisa ser lida no repositório pelo agente.

## T00 — Preparação e ambiente

- [ ] Extrair os arquivos na nova pasta e criar o repositório conforme GITHUB.md.
- [ ] Conferir instruções e estado real dos arquivos.
- [ ] Conferir P01 no computador da universidade e registrar a versão aprovada.
- [ ] Confirmar tipo de projeto e comandos reais de compilação/execução.

Bloqueio: não criar solução com framework presumido. Enquanto P01 não for resolvida, produzir apenas diagnóstico e planejamento autorizado.

## T01 — Estrutura mínima

Depende de P01.

- [ ] Criar solução/projeto Web Forms compatível com a universidade.
- [ ] Separar domínio, aplicação, infraestrutura em memória e Web sem camadas vazias.
- [ ] Criar Master Page e navegação básica.
- [ ] Compilar e abrir a página inicial no Windows; registrar evidência textual de ambiente e resultado.

## T02 — Modelo e massa de dados

- [ ] Implementar entidades e relacionamentos aprovados.
- [ ] Criar massa fictícia cobrindo os tipos de membro, projetos em andamento/concluídos, orientações atuais/passadas e coorientador externo.
- [ ] Garantir exatamente cinco artigos por perfil, alunos vinculados, orientador principal pesquisador e coordenador único.
- [ ] Implementar estado por sessão sem reinicialização em todo request.
- [ ] Verificar invariantes com testes focados nas regras, quando houver ferramenta compatível, e revisão das relações da massa.

Não implementar cadastro incompleto, transições históricas ou cálculo de verba por pesquisador.

## T03 — Diretório e perfis

- [ ] Listar membros usando controle de servidor e Data Binding.
- [ ] Abrir perfil pelo identificador e exibir campos/artigos.
- [ ] Exibir relações e duas listas de orientandos no perfil do pesquisador.
- [ ] Tratar identificador inválido ou inexistente.
- [ ] Conferir ausência de HTML injetado nos campos textuais.

## T04 — Projetos

- [ ] Exibir cards de projetos e navegação para detalhe.
- [ ] Exibir situação, descrição, verba por projeto quando informada, coordenador, pesquisadores e estudantes vinculados.
- [ ] Não exibir cálculo de verba por pesquisador.

## T05 — Interface e ciclo de vida

- [ ] Conferir layout em tela estreita e larga, teclado, foco, contraste e imagens.
- [ ] Testar navegação completa e retorno às listagens.
- [ ] Testar requests/postbacks existentes sem recriação indevida dos dados.
- [ ] Verificar isolamento entre sessões e comportamento esperado após expiração.
- [ ] Usar recursos locais necessários à demonstração.

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
