# BolaSocial — Monorepo

BolaSocial é um monorepositório profissional que replica o clássico jogo social de futebol "Bola Social" para a web moderna. O projeto foi arquitetado para permitir partidas online em tempo real com simulação autoritativa no servidor, economia interna e recursos sociais completos.

> ⚠️ **Status:** Este repositório contém a base arquitetural, documentação extensiva e protótipos mínimos para os serviços centrais. Ele serve como ponto de partida sólido para evoluir até uma implementação completa de produção.

## Visão Geral do Monorepo

```
.
├── apps
│   ├── api            # ASP.NET 9 + SignalR + EF Core (Minimal APIs) - servidor autoritativo
│   └── web            # Next.js 14 + Phaser 3 - cliente web
├── packages
│   └── shared         # Tipos/DTOs compartilhados, protocolo de netcode
├── infra              # Docker Compose, Traefik, scripts de migração e seed
└── docs               # Documentação abrangente (arquitetura, netcode, operações, etc.)
```

## Funcionalidades Planejadas

- Partidas online 1v1 e 5v5 com bots substitutos.
- Simulação autoritativa com netcode de previsão, reconciliação e interpolação.
- Controles completos por teclado com suporte a remapeamento.
- Ranking global, ranking entre amigos e economia interna baseada em moedas.
- Progresso persistido em PostgreSQL + Redis para matchmaking e cache.
- Autenticação com Google OAuth, JWT com refresh tokens e RBAC.
- Observabilidade com Prometheus, OpenTelemetry e logs estruturados (Serilog + Loki).

## Como executar (modo desenvolvimento)

1. **Pré-requisitos**
   - Docker e Docker Compose.
   - Node.js 20.x + pnpm.
   - .NET SDK 9 (preview) ou superior.

2. **Instalação das dependências do cliente**

   ```bash
   cd apps/web
   pnpm install
   pnpm dev
   ```

3. **API**

   ```bash
   cd apps/api
   dotnet restore
   dotnet run
   ```

4. **Ambiente completo (docker-compose)**

   ```bash
   docker compose up -d --build
   ```

   Acesse `http://localhost` para o cliente e `http://localhost/api/swagger` para a documentação da API.

## Credenciais Seed (modo desenvolvimento)

| Usuário       | Email                | Senha      | Papel |
|---------------|----------------------|------------|-------|
| Game Master   | gm@bolasocial.gg     | Bola@123   | GM    |
| Jogador Azul  | azul@bolasocial.gg   | Bola@123   | User  |
| Jogador Rosa  | rosa@bolasocial.gg   | Bola@123   | User  |

## Scripts úteis

| Comando                  | Descrição                                                 |
|--------------------------|-----------------------------------------------------------|
| `pnpm dev:all`           | Roda cliente e servidor em modo watch/hot reload.         |
| `dotnet ef database update` | Aplica migrações do banco.                             |
| `dotnet test`            | Executa testes backend.                                   |
| `pnpm lint`              | Lint + typecheck do cliente.                              |
| `docker compose up -d`   | Sobe o ambiente com Traefik, API, Web, Postgres e Redis.  |

## Licença

[MIT](./docs/LICENCA.md)

Para detalhes aprofundados consulte a pasta [`docs/`](./docs).
