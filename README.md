# Portal GPFA — Programação Web

Projeto acadêmico de portal de visibilidade científica e gestão de projetos do Grupo de Pesquisa sobre Formigas Atômicas (GPFA), em C# e ASP.NET Web Forms. Projeto novo, independente da concessionária.

## Estado real

Repositório: https://github.com/MarceloFiuza/portal-gpfa

Contém somente documentação inicial versionada em `main`. Não existe aplicação implementada, compilada ou validada. O desenvolvimento será conduzido pelo OpenCode, com revisão humana e validação em Windows na universidade.

## Escopo

- Parte 1, entrega em 02/12/2026: modelo orientado a objetos, vitrine de membros e projetos, perfis, navegação, controles de servidor, Master Page, Data Binding, ciclo de vida e dados em memória.
- Parte 2, entrega em 11/12/2026: referência futura; banco SQL Server/LocalDB, CRUD persistente, tratamento de erros e relatórios. Não implementar agora.
- Autenticação, upload de arquivos, integrações com ORCID/Lattes/Scholar e deploy não são requisitos aprovados. Os endereços dos perfis científicos são links externos.

## Leitura inicial

1. `AGENTS.md`: instruções de trabalho para o agente.
2. `docs/REQUISITOS.md`: requisitos e critérios de aceitação.
3. `docs/ARQUITETURA.md`: separação de responsabilidades proposta.
4. `docs/PENDENCIAS.md`: decisões ainda não disponíveis e seus bloqueios.
5. `docs/TAREFAS.md`: execução incremental e verificação.
6. `docs/GITHUB.md`: criação e sincronização do repositório.
7. `docs/PROMPT_INICIAL.md`: primeiro comando de trabalho para o OpenCode.

## Ambientes

Ambiente de edição: Linux, com VSCode. Execução e validação do Web Forms estão previstas no Windows da universidade, com Visual Studio e ferramentas compatíveis com o projeto. ASP.NET Web Forms usa .NET Framework; instalar o SDK moderno do .NET no Linux não fornece seu runtime.

Versão do .NET Framework, versão de C# e ferramentas do laboratório: pendentes. Não criar uma solução com versão presumida. Primeiro conferir o ambiente e registrar a decisão em `docs/PENDENCIAS.md`. Não há comandos de execução definidos enquanto não houver projeto real.

## Desenvolvimento

Implementar somente a Parte 1 em etapas pequenas. Separar apresentação, regras e armazenamento em memória; não criar uma arquitetura empresarial desnecessária. Não confundir prévia estática ou compilação com stubs com execução real de Web Forms.

## Origem das decisões

Fonte: `Projeto Final da disciplina.pdf`, fornecido pelo usuário. A data da Parte 2 considera a correção manuscrita/impressa para 11/12/2026, substituindo 16/12/2026 riscado. Datas devem ser conferidas se houver atualização do professor.

O usuário confirmou projeto novo, Linux para desenvolvimento, Windows apenas nas aulas e execução limitada à Parte 1. A versão do framework e a atribuição de verba por pesquisador não foram esclarecidas.
