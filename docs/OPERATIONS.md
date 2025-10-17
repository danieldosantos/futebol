# Operações e Observabilidade

## Stack de Observabilidade

- **Prometheus**: coleta métricas da API via `/metrics` (AspNetCore + Prometheus.Net).
- **Grafana**: dashboards pré-configurados (`infra/grafana/dashboards`).
- **Loki**: ingestão de logs estruturados do Serilog.
- **Tempo/Otel Collector**: traces distribuídos com OpenTelemetry.

## Painéis

- `MatchLoop`: tempo por tick, backlog de inputs, latência SignalR.
- `Auth`: logins por minuto, erros, taxa de sucesso Google OAuth.
- `DB`: conexões PostgreSQL, tempo de query, deadlocks.

## Alertas

- Latência média > 150 ms por 5 min → alerta amarelo.
- Fila de matchmaking > 50 jogadores por 2 min → escalar instâncias.
- Erros 5xx > 1% → investigar releases recentes.

## Deploy

1. CI executa lint, testes, build e publica imagens no registro.
2. CD (manual) aplica Helm chart (`infra/helm/bolasocial`).
3. Migrações executadas via `dotnet ef database update` dentro do Job Kubernetes.
4. Seed roda automaticamente se banco estiver vazio.

## Runbooks

- `docs/runbooks/reconnect.md`: lidar com instabilidade SignalR.
- `docs/runbooks/match-stuck.md`: forçar encerramento de partida presa.
- `docs/runbooks/rollback.md`: executar rollback seguro.
