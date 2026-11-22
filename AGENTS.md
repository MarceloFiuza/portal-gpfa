# Instruções para o OpenCode

## Objetivo e precedência

Desenvolver o Portal GPFA em C# e ASP.NET Web Forms, conforme o enunciado e as decisões do usuário. Este projeto não é a concessionária. Leia README, requisitos, arquitetura, pendências e tarefas antes de alterar código. Instruções atuais do usuário e esclarecimentos do professor prevalecem; atualize a documentação quando alterarem decisões.

## Escopo atual

Somente Parte 1, com dados em memória. A Parte 2 existe como contexto e não autoriza implementar SQL, ORM, migrations, CRUD persistente ou relatórios financeiros. Não adicionar login, APIs ou outras funcionalidades por iniciativa própria. Não migrar para ASP.NET Core, MVC, Blazor ou outra stack.

## Trate dúvidas explicitamente

Não invente regras, requisitos ou informações do ambiente. Registre a dúvida, identifique a tarefa afetada e peça esclarecimento antes de implementá-la. Só prossiga em tarefas independentes quando já autorizadas. A versão do framework é pendência bloqueante para criar/configurar a solução Web Forms; não assumir 4.8 ou outra versão. A verba por pesquisador bloqueia apenas esse cálculo futuro.

## Código e responsabilidades

- C# simples, compatível com o compilador confirmado no laboratório.
- Domínio sem dependência de System.Web, controles ASP.NET ou SQL.
- Code-behind recebe entradas, chama serviços e apresenta resultados; não concentra regras ou consultas.
- Repositórios em memória cuidam do armazenamento, não da renderização.
- Use Master Page, controles de servidor e Data Binding reais.
- Faça binding inicial sob `!IsPostBack` quando adequado. Recarregue após operações deliberadas; não apague entradas ou listas em todo postback.
- Não acesse Session em Application_Start. Inicialize o estado por sessão e não o recrie em toda requisição.
- Preserve invariantes ao atualizar relações. Uma falha não pode deixar estudante sem orientador/projeto nem projeto sem coordenador.
- Evite interfaces genéricas, contêiner de injeção, CQRS, microserviços e camadas sem responsabilidade concreta.
- Mantenha os arquivos .aspx, .aspx.cs e .designer.cs, quando aplicáveis, coerentes com o tipo de projeto. Confira Inherits, namespace, controles e referências no .csproj.
- Codifique texto dinâmico na saída e aceite apenas URLs http/https nos links informados. Valide entradas no servidor nas operações implementadas.
- Use dados fictícios e imagens locais próprias ou com licença adequada; não apresente perfis ou artigos fictícios como reais.

## Fluxo de execução

1. Confira o estado atual e alterações do usuário antes de editar.
2. Selecione uma tarefa autorizada em `docs/TAREFAS.md` e explique o resultado esperado.
3. Faça a menor alteração coesa; não reescreva arquivos sem necessidade.
4. Verifique o que for possível; registre comando, ambiente, resultado e limitações.
5. Atualize a tarefa e a documentação afetada.
6. Informe arquivos alterados, comportamento, verificação e próximo passo.

Não apague trabalho, instale ferramentas do sistema, faça commits, push ou publique sem solicitação. Não exponha credenciais. Use branches pequenas quando houver autorização para trabalhar com Git.

## Honestidade de validação

Inspeção estática, build do domínio, compilação com stubs e teste visual de HTML não demonstram funcionamento do Web Forms. Nunca declare um fluxo aprovado sem executá-lo no ambiente adequado. Identifique verificações pendentes em Windows e não marque a Parte 1 concluída antes delas.
