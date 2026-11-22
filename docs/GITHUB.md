# GitHub — criação e uso do repositório

Este roteiro é para uma pasta nova. Os comandos serão executados pelo usuário; este pacote não criou repositório nem fez push.

## 1. Pasta local

No Linux, escolha uma pasta fora de qualquer repositório existente:

```bash
mkdir -p ~/Documentos/portal-gpfa
cd ~/Documentos/portal-gpfa
code .
```

Copie para essa pasta o CONTEÚDO de `portal-gpfa` deste pacote: README.md, AGENTS.md, .gitignore e docs/. Evite criar uma segunda pasta portal-gpfa dentro dela.

Confira ferramentas e eventual repositório pai:

```bash
git --version
git rev-parse --show-toplevel
```

Em pasta ainda fora de Git, a segunda chamada deve informar que não é repositório. Se mostrar caminho de um repositório pai, pare e escolha outro local; não inicialize sem compreender essa situação.

## 2. GitHub pelo navegador

Abra https://github.com/new e crie `portal-gpfa` na sua conta. Escolha a visibilidade conforme as regras da disciplina. Deixe desmarcadas as opções de criar README, .gitignore e licença, pois os arquivos iniciais já serão locais. Copie a URL HTTPS real gerada.

## 3. Identidade e primeiro commit

Dentro da pasta correta:

```bash
git init -b main
git config --get user.name
git config --get user.email
```

Se precisar definir a identidade, use os seus dados (configuração somente deste repositório):

```bash
git config user.name "SEU NOME"
git config user.email "SEU EMAIL OU EMAIL NOREPLY DO GITHUB"
```

Depois:

```bash
git status
git add README.md AGENTS.md .gitignore docs/
git diff --cached --stat
git diff --cached
git commit -m "docs: prepara orientacoes da parte 1 do portal GPFA"
```

## 4. Associar e enviar

Substitua SEU_USUARIO pela sua conta e use a URL real copiada do GitHub:

```bash
git remote add origin https://github.com/SEU_USUARIO/portal-gpfa.git
git remote -v
git push -u origin main
git status
```

O push requer autenticação. A senha comum da conta não autentica operações Git por HTTPS; use um gerenciador de credenciais, GitHub CLI autenticada ou token apropriado quando solicitado. Não escreva token na URL nem nos arquivos.

Se já usa GitHub CLI e ela está instalada, uma opção é `gh auth login`, escolhendo GitHub.com, HTTPS e autenticação pelo navegador. Isso não é necessário se seu Git já estiver autenticado.

## 5. Próxima tarefa

Atualize antes de começar, com árvore de trabalho limpa:

```bash
git switch main
git pull --ff-only
git switch -c feat/estrutura-webforms
```

Crie essa branch de implementação quando P01 estiver resolvida. Para planejamento prévio, use nome como `docs/revisao-inicial`.

Ao concluir uma alteração pequena, revise `git diff`, adicione arquivos específicos, faça commit e envie a branch. Abra Pull Request no GitHub para revisar antes de integrar à main. Não inclua credenciais, saídas de build ou configurações pessoais.

## 6. No computador da universidade

Com autenticação apropriada caso o repositório seja privado:

```bash
git clone https://github.com/SEU_USUARIO/portal-gpfa.git
cd portal-gpfa
```

Nas visitas seguintes, dentro da cópia existente, use `git status` e, com árvore limpa, `git pull --ff-only`. Para validar uma branch ainda não integrada, faça `git fetch origin` e mude para a branch correspondente antes de abrir a solução no Visual Studio.

Não deixe credenciais persistidas em computador compartilhado. Confira alterações antes de sair e não dependa de arquivos que existam somente no laboratório.

## Fontes

- https://docs.github.com/en/migrations/importing-source-code/using-the-command-line-to-import-source-code/adding-locally-hosted-code-to-github
- https://docs.github.com/en/get-started/git-basics/managing-remote-repositories
