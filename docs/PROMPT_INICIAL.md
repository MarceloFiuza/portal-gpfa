# Prompt inicial para o OpenCode

> **Documento histórico.** Este foi o prompt do primeiro bloco, já executado. Consulte `README.md` e `docs/TAREFAS.md` para o estado atual. Não use este texto para recriar a estrutura nem como autorização para repetir etapas concluídas.

Prompt usado no início do desenvolvimento. Mantido para registro na raiz do repositório.

---

Estamos desenvolvendo o Portal GPFA, trabalho de Programação Web em C# e ASP.NET Web Forms. Este é um projeto novo; não use regras nem código da concessionária.

Leia `AGENTS.md`, `README.md` e todos os documentos de `docs/`. Confira o estado real da pasta e do Git, preservando alterações existentes. Não faça commit nem push.

Decisões técnicas já aprovadas pelo usuário:

- C# com `LangVersion` 7.3.
- .NET Framework 4.8.
- ASP.NET Web Forms no modelo Web Application clássico.
- Solução `.sln` e projeto `.csproj` compatíveis com Visual Studio 2022.
- Um único projeto Web, com separação lógica de responsabilidades em pastas.
- Edição no Linux; compilação e execução reais no Windows da universidade.

A escolha técnica encerra a parte de decisão de P01; a compatibilidade e a execução no laboratório continuam pendentes como P01b. Não afirme que as ferramentas estão instaladas.

Implemente somente o primeiro bloco da Parte 1:

1. Crie a solução e o projeto Web Forms com alvo explícito .NET Framework 4.8 e C# 7.3.
2. Separe domínio, aplicação, infraestrutura em memória e apresentação Web sem classes ou projetos vazios.
3. Implemente Master Page com cabeçalho, navegação e rodapé.
4. Implemente página inicial, página de membros e página de projetos, com estado vazio informativo quando não houver dados.
5. Use CSS local simples, responsivo e acessível.
6. Não implemente entidades completas, massa de dados, cadastros, históricos, banco, login ou relatórios.

No Linux, verifique apenas o que for possível: estrutura, XML, referências, namespaces, `Inherits`, controles e ContentPlaceHolders. Não altere o alvo para executar no Linux e não use stubs para declarar o Web Forms validado.

Atualize a documentação afetada, incluindo README, PENDENCIAS e TAREFAS, separando implementação de validação em Windows. Documente como abrir a solução, compilar e executar pelo IIS Express, quais páginas conferir e o que continua pendente.

Ao terminar, apresente arquivos criados/alterados, arquitetura efetiva, verificações realizadas e roteiro mínimo de teste na universidade. Encerre o bloco para revisão. Não faça commit, push nem publicação. Não declare build ou execução Web Forms aprovados por análise estática ou stubs.
