# Segurança

## Autenticação e Autorização

- OpenID Connect (Google) + login por email/senha.
- Argon2id (libsodium) para hash de senha.
- JWT curto (15 min) + refresh tokens persistidos.
- Cookies HttpOnly/SameSite=Lax em produção.
- MFA opcional via TOTP (roadmap).

## Proteções

- Rate limit adaptativo com Redis (IP + usuário) para `/auth` e `/matchmaking`.
- CSRF tokens sincronizados para rotas sensíveis.
- Validações com FluentValidation e DTOs explícitos.
- Sanitização de inputs (chat) com content filters.

## Auditoria

- Logs Serilog com enriquecedores (`matchId`, `userId`, `roomId`).
- Retenção configurável e exportação conforme LGPD.
- Endpoint `DELETE /api/users/me` para remoção de conta.

## Processo de Vulnerabilidade

1. Reporte via `security@bolasocial.gg` (PGP disponível).
2. SLA 48h para resposta inicial; patch ou mitigação em até 7 dias.
3. Divulgações coordenadas em `docs/SECURITY-ADVISORIES.md`.

## Ferramentas Automatizadas

- Dependabot semanal.
- GitHub Advanced Security (CodeQL + secret scanning).
- SBOM gerado com Syft e publicado nos releases.
