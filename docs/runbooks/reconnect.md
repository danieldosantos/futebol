# Runbook: Reconnect SignalR

1. Validar status de Redis e backplane.
2. Checar logs do Traefik para erros 502/504.
3. Forçar reconexão dos clientes afetados via `/api/gm/rooms/{id}/end` se necessário.
4. Abrir incidente se mais de 10% dos jogadores estiverem impactados.
