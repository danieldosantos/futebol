# Runbook: Partida Travada

1. Confirmar `matchId` via painel GM.
2. Executar `POST /api/gm/rooms/{id}/end`.
3. Conferir que resultado foi persistido (`GET /api/matches/{id}`).
4. Criar tarefa para investigar causa raiz (logs + telemetria).
