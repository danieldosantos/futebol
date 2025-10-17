# Arquitetura do BolaSocial

BolaSocial é um monorepositório composto por cliente web, serviços backend e infraestrutura compartilhada. A arquitetura foi planejada para suportar partidas em tempo real com baixa latência e escalabilidade horizontal.

## Visão Geral

- **Cliente (Next.js 14 + Phaser 3)**: renderiza o campo, HUD e aplica prediction/reconciliação. Comunicação em tempo real via SignalR.
- **API Autoritativa (ASP.NET 9)**: hospeda endpoints REST, hub SignalR e o loop principal de partidas (`MatchLoopHostedService`).
- **Persistência**: PostgreSQL (dados relacionais) + Redis (cache, matchmaking, backplane SignalR).
- **Observabilidade**: OpenTelemetry (traces), Prometheus (métricas), Loki (logs estruturados com Serilog).
- **Infraestrutura**: Docker Compose para desenvolvimento local, com Traefik atuando como reverse proxy.

## Diretórios

| Diretório          | Descrição |
|--------------------|-----------|
| `apps/web`         | Cliente Next.js + Phaser + Zustand + React Query. |
| `apps/api`         | API em .NET com camadas Domain/Application/Infrastructure. |
| `packages/shared`  | Tipos TypeScript e contratos OpenAPI compartilhados entre cliente e servidor. |
| `infra`            | Arquivos de orquestração (Docker Compose, Traefik), scripts de migração e seed. |

## Fluxo de Partida

1. Usuário autentica (Google OAuth ou credenciais locais) e entra no lobby.
2. Cliente chama `POST /api/matchmaking/queue` para entrar na fila.
3. Serviço de matchmaking enfileira jogadores em Redis e cria salas com 2 ou 10 participantes.
4. `MatchLoopHostedService` inicia a simulação autoritativa com tick de 60 Hz.
5. Clientes enviam inputs (60 Hz); servidor processa, atualiza estado da física e emite snapshots (20 Hz).
6. Resultado e telemetria são persistidos em PostgreSQL; ranking é recalculado.

## Físicas e Netcode

- Engine custom simplificada (AABB e círculos) com integração semi-implícita.
- Controles com aceleração, limites de velocidade, atrito e stamina.
- Prediction local + reconciliação com re-aplicação de inputs ainda não confirmados.
- Interpolação de jogadores remotos via buffer de ~100 ms.

## Escalabilidade

- SignalR com backplane Redis garante horizontalidade.
- `MatchLoopHostedService` é stateless, mas salas podem ser distribuídas em múltiplas instâncias usando `MatchScheduler`.
- Traefik + Docker Compose em desenvolvimento; recomenda-se Kubernetes para produção (manifests de exemplo em `infra/k8s`).

## Segurança

- JWT com refresh + cookies HttpOnly.
- Rate limiting e proteção anti replay de inputs.
- RBAC com policies (User, Game Master) e auditoria de ações sensíveis.

## Convenções de Código

- TypeScript/React: ESLint (Airbnb) + Prettier + `@total-typescript/ts-reset`.
- C#: `dotnet format`, `StyleCop.Analyzers`.
- Commits: Conventional Commits (`feat:`, `fix:`, `docs:` etc.).
- Pipelines GitHub Actions: lint, testes, build e publicação de imagens.

## Próximos Passos

- Implementar física completa da bola e dos jogadores.
- Integrar matchmaking com Redis e fila priorizada por MMR.
- Concluir HUD, chat in-game e loja.
- Automatizar seed inicial no container da API.
