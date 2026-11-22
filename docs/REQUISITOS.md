# Requisitos e aceitação

## Convenções

RF = funcionalidade extraída do enunciado; RN = regra explícita; CA = critério de aceitação proposto para verificá-la. Detalhes de implementação estão na arquitetura e não são exigências adicionais do professor.

## Parte 1 — escopo autorizado

| ID | Requisito | Aceitação |
| --- | --- | --- |
| RF01 | Diretório público de membros | Visitante encontra os membros e abre seus perfis individuais. |
| RF02 | Perfil com formação acadêmica, mini-biografia, foto, links Scholar/ORCID/Lattes e pretensões atuais de pesquisa | Dados fictícios de demonstração exibem todos os campos; imagens possuem alternativa textual e links têm nomes compreensíveis. |
| RF03 | Cinco artigos de destaque por membro, com título, veículo, ano e DOI/link | Cada perfil da demonstração apresenta exatamente cinco artigos e seus campos. Não reduzir silenciosamente para “até cinco”. |
| RF04 | Projetos em andamento e concluídos | Listagem em cards e detalhe exibem título, descrição, situação e verba captada quando houver financiamento. |
| RF05 | Relações acadêmicas navegáveis | É possível conferir os vínculos de estudantes, pesquisadores, orientadores e projetos nas telas pertinentes. |
| RF06 | Orientandos atuais e passados | Perfil do pesquisador exibe duas listas distintas, com dados de exemplo em ambas quando aplicável. |
| RF07 | Esqueleto visual e navegação com dados em memória | Fluxos não dependem de banco, funcionam em Web Forms e preservam o estado temporário conforme o ciclo de vida. |

O enunciado cita pesquisadores, estudantes de pós-graduação, bolsistas de iniciação científica e técnicos, e explicita regras para estudantes de graduação e pós-graduação. Não excluir técnicos da vitrine. Não confundir “estudante” com “pesquisador” para atribuir funções restritas a pesquisadores.

## Regras explícitas

| ID | Regra | Verificação |
| --- | --- | --- |
| RN01 | Todo estudante de graduação/pós está vinculado a um ou mais projetos do grupo. | Estado válido nunca possui estudante sem projeto; vínculos referenciam projetos existentes. |
| RN02 | Orientandos são apresentados como atuais ou passados. | Listas separadas refletem o vínculo de orientação; não depender de texto livre para classificar. |
| RN03 | Um projeto pode ter vários pesquisadores participantes e tem exatamente um coordenador professor/pesquisador. | Coordenador único e pesquisador cadastrado; nenhum estudante ocupa essa função. |
| RN04 | Todo estudante tem um orientador principal, obrigatoriamente pesquisador do grupo cadastrado. | Orientador inexistente ou de tipo incompatível não gera estado válido. |
| RN05 | Um estudante pode ter múltiplos coorientadores, inclusive externos não cadastrados como pesquisadores. | Nomes externos podem ser representados sem criar pesquisador artificialmente; coorientação é opcional. |
| RN06 | Cada membro destaca exatamente cinco artigos. | Dados exibidos respeitam a cardinalidade; política de cadastro incompleto permanece pendente para eventuais formulários. |

RN01 e RN03 coexistem: alunos são vinculados aos projetos; o trecho sobre participação de pesquisadores não elimina esse vínculo obrigatório.

## Funcionalidades não autorizadas nesta etapa

A Parte 1 pede esqueleto visual e navegação em memória. Não presumir que todos os cadastros, edições e exclusões precisam ser implementados agora. Começar pela consulta com dados fictícios coerentes. Se o professor exigir manutenção em memória, confirmar e detalhar as operações antes de ampliar o escopo. As invariantes continuam obrigatórias para a carga inicial e para qualquer operação autorizada.

## Parte 2 — somente referência

- Modelo relacional com PK, FK e tabelas associativas N:N.
- Separação de responsabilidades em camadas.
- Inserção, leitura, atualização e exclusão integradas ao SQL Server/LocalDB.
- Comandos parametrizados e tratamento de erros, incluindo violações de FK.
- Verba por projeto e por pesquisador; política por pesquisador pendente.
- Artigos de destaque vinculados corretamente no banco.

Não escolher tecnologia de acesso ao banco, escrever SQL ou implementar essa etapa agora.

## Rubrica da Parte 1

Cada critério vale 25%: interpretação/elicitação; modelagem orientada a objetos; domínio do Web Forms e ciclo de vida; interface/usabilidade/responsividade. A verificação precisa cobrir os quatro, não apenas a aparência das páginas.
