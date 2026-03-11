# Sid Master Unificado (SID Web + Puchta Insight)

Criado para facilitar os projetos da Puchta Engenharia.

Repositório de trabalho para unificar o **SID Master** em uma plataforma **100% Web**, integrando as funcionalidades do **Puchta Insight**.

## Como rodar (dev)

No PowerShell:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\SID-master\Run-SID-Web.ps1 -Port 5301
```

## Estrutura

- `SID-master/` -> SID Web (API ASP.NET Core + React/Vite)
- `Side_Siemens-main v2/Side_Siemens-main/` -> Puchta Insight (scripts, painel e TiaMap)
