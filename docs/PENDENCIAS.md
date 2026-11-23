# Decisões e pendências

## Confirmado pelo usuário

- Novo projeto e repositório, sem reaproveitar a concessionária.
- C# e ASP.NET Web Forms.
- Edição em Linux; Windows disponível somente nas aulas da universidade.
- Documentar as duas etapas e implementar somente a Parte 1.
- Decisão técnica de 22/11/2026: C# 7.3, .NET Framework 4.8, ASP.NET Web Forms no modelo Web Application, solução e projeto compatíveis com Visual Studio 2022, um único projeto Web com separação lógica em pastas.
- Verba por pesquisador ainda sem esclarecimento; manter pendente.

## Registro de pendências

| ID | Informação faltante | Impacto e tratamento |
| --- | --- | --- |
| P01a | Escolha técnica de framework, linguagem, modelo de projeto e IDE | **Resolvida em 22/11/2026 por decisão do usuário:** C# 7.3, .NET Framework 4.8, ASP.NET Web Forms no modelo Web Application, Visual Studio 2022. A solução `PortalGpfa.sln` e o projeto `PortalGpfa.Web` foram criados com esse alvo. |
| P01b | Compatibilidade e execução no laboratório: versão instalada do Visual Studio, targeting pack 4.8, carga de trabalho ASP.NET, IIS Express e restrições de instalação | Não bloqueia mais a criação da solução, mas bloqueia declarar build e execução aprovados. Conferir na universidade e registrar o observado, sem presumir que as ferramentas já existem. |
| P02 | Atribuição da verba ao pesquisador: total do projeto, rateio, coordenação ou outra regra | Bloqueia somente o cálculo por pesquisador da Parte 2. Não somar, dividir ou atribuir automaticamente. |
| P03 | Tecnologia de acesso ao banco e versão/instância SQL Server/LocalDB | Bloqueia configuração da Parte 2; não afeta vitrine em memória. |
| P04 | Alcance do esqueleto: somente consulta ou também formulários de manutenção em memória | Consulta com dados fictícios faz parte do escopo inicial; não adicionar CRUD em memória sem esclarecimento. |
| P05 | Como cadastrar um membro antes de possuir exatamente cinco artigos | Não flexibilizar RN06. Massa inicial terá cinco por membro; bloqueia apenas eventual fluxo de cadastro incompleto. |
| P06 | Tratamento de ex-estudantes: permanência no portal e em projetos, término/troca da orientação e histórico | Demonstrar listas atuais/passadas com vínculos coerentes; não implementar transições nem impor projeto ativo aos já formados sem esclarecimento. A massa usa um vínculo de orientação marcado como passado e um projeto concluído; o perfil exibe o rótulo “Passado” e não há status explícito de ex-aluno, operação de formatura, troca de orientador nem exclusão de histórico. **Limitação:** sem definir quem é estudante atual, o domínio aceita estudante somente com vínculo passado e não garante orientador atual obrigatório. |

## Conferência na universidade

A decisão de 22/11/2026 define o alvo pretendido, não confirma a disponibilidade das ferramentas. Registrar, sem credenciais: versão do Visual Studio; presença do .NET Framework 4.8 Developer Pack/targeting pack; carga de trabalho ASP.NET e desenvolvimento Web; suporte a Web Forms no modelo Web Application; IIS Express; restrições de instalação. Atualizar P01b apenas com informação observada ou confirmada. Se o ambiente não suportar o alvo, não alterar silenciosamente a versão: registrar o conflito e pedir orientação.

## Protocolo de decisão

Para cada esclarecimento, registrar data, origem (usuário/professor/ambiente), decisão e requisitos/tarefas afetados. Não preencher uma resposta presumida para encerrar uma pendência. Novas regras exigem revisão da modelagem e da massa de dados antes de código afetado.
