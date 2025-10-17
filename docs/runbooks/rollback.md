# Runbook: Rollback

1. Pausar deploys automáticos no GitHub Actions.
2. Selecionar release estável anterior (`helm rollback bolasocial <release-id>`).
3. Executar `kubectl delete job bolasocial-migrations` para evitar reexecução automática.
4. Restaurar snapshot do banco se necessário (Point-in-time recovery).
5. Documentar causa e plano de correção no incidente.
