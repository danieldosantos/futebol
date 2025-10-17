# Contribuindo para o BolaSocial

Obrigado por dedicar tempo para contribuir! Este documento descreve o fluxo sugerido.

## Requisitos

- Node.js 20, pnpm, .NET 9 SDK, Docker.
- Execute `pnpm install` em `apps/web` e `dotnet restore` em `apps/api`.

## Fluxo de Trabalho

1. Crie branch a partir de `main` (`feat/`, `fix/`, `docs/`).
2. Garanta que `pnpm lint`, `pnpm test`, `dotnet test` e `dotnet format` estão passando.
3. Adicione ou atualize testes relevantes.
4. Atualize `docs/CHANGELOG.md` (secção _Unreleased_).
5. Abra PR com descrição clara + checklist de QA.

## Convenções

- Commits seguem [Conventional Commits](https://www.conventionalcommits.org/).
- Código TypeScript deve evitar `any`; use tipos de `packages/shared`.
- C# deve manter cobertura mínima de 90% para regras críticas.

## Código de Conduta

O projeto segue o [Contributor Covenant](https://www.contributor-covenant.org/).
