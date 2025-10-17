# API BolaSocial

## Autenticação

| Método | Rota                | Descrição                         |
|--------|---------------------|-----------------------------------|
| POST   | `/api/auth/register`| Cria usuário local                |
| POST   | `/api/auth/login`   | Login com email/senha             |
| POST   | `/api/auth/refresh` | Renova tokens                     |
| POST   | `/api/auth/logout`  | Revoga refresh token              |

## Usuário / Time

- `GET /api/users/me`
- `GET /api/teams/my`
- `POST /api/teams`
- `PATCH /api/teams/{id}`

## Ranking

- `GET /api/ranking/global`
- `GET /api/ranking/friends`

## Matchmaking & Partidas

| Método | Rota                          | Descrição |
|--------|-------------------------------|-----------|
| POST   | `/api/matchmaking/queue`      | Entra na fila |
| DELETE | `/api/matchmaking/queue`      | Sai da fila  |
| GET    | `/api/matches/{id}`           | Detalhes da partida |
| GET    | `/api/matches/{id}/events`    | Eventos resumidos |

## Painel GM

- `GET /api/gm/rooms`
- `POST /api/gm/bots`
- `POST /api/gm/rooms/{id}/end`

## Hubs SignalR

### `/hubs/match`

- Métodos: `JoinMatch(matchId)`, `Input(matchId, ClientInput)`
- Eventos: `snapshot`, `matchEvent`, `matchEnded`

### `/hubs/lobby`

- Eventos: `matchFound`, `queueStatus`

## Contratos Compartilhados

Os DTOs estão versionados em `packages/shared` e publicados como pacote npm (`@bolasocial/shared`).
