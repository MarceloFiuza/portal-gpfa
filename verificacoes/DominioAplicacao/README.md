# Verificação isolada de domínio e aplicação

Harness de linha de comando para executar as regras de domínio e as consultas da camada de aplicação no Linux.

## Escopo

- Compila os arquivos reais de `PortalGpfa.Web/Dominio`, `Infraestrutura/CargaPortalFicticio.cs` e `Aplicacao/ConsultasPortal.cs`.
- Usa `LangVersion` 7.3 e um runtime .NET disponível no Linux, **fora** do alvo do produto.
- Não compila Web Forms, não usa `System.Web` e não substitui a validação no Windows.
- Não faz parte da solução `PortalGpfa.sln` e não altera o alvo .NET Framework 4.8 do produto.

## Como executar

Na raiz do repositório:

```bash
dotnet run --project verificacoes/DominioAplicacao/PortalGpfa.Verificacoes.DominioAplicacao.csproj
```

Resultado esperado:

```text
OK: 105 verificações de domínio e aplicação concluídas.
```

## Limitação relevante

A massa inclui Felipe com apenas vínculo de orientação passado. As regras atuais aceitam esse estado, pois exigem ao menos um vínculo e no máximo um vínculo atual, mas não exigem vínculo atual. Isso demonstra a limitação registrada em P06; o harness não deve ser interpretado como prova de que todo estudante atual tem orientador atual.
