# Catálogo de Funcionalidades — Puchta PLC Insight + SID

**Data base:** 2026-03-11  
**Objetivo:** listar, de forma completa e objetiva, o que existe hoje e o que será entregue na plataforma final (unificando o melhor do Puchta PLC Insight e do SID).

---

## 1) Puchta PLC Insight (atual)

### 1.1 Operação (Painel Web / Web Manager)
- Seleção de `vendor` (`auto|siemens|rockwell`) antes da origem.
- Seleção de origem (pasta/arquivo) com validação.
- Persistência de configuração (`vendor` + `tiaPath`) em `Logs/web_settings.json`.
- Execução de scripts do ecossistema (exportação, documentação, etc.) via endpoints do painel.
- Visualizadores:
  - Mermaid estrutural (projeto/blocos) via endpoint.
  - Mermaid de fluxo de execução (quando aplicável).
- Ação “Abrir Puchta PLC Insight” para abrir o mapa React/FastAPI.

### 1.2 Análise (App React + FastAPI)
- Backend multi-vendor com pipeline:
  - Siemens: leitura de XML exportado (OB/FB/FC/DB quando aplicável).
  - Rockwell: leitura de `.L5X/.L5K`.
- Geração de grafo padronizado (nodes/edges) para UI.
- UI com ReactFlow:
  - mapa navegável,
  - filtros (por tipo/vendor),
  - painel de detalhes (quando disponível),
  - base para evoluir para “modo iniciante” e “modo especialista”.

### 1.3 Documentação e scripts (atual)
- Scripts para geração de documentação (Siemens e Rockwell).
- Scripts para exportação/attach com TIA Portal (com restrições de lock/read-only).
- Registro de mudanças e tarefas nos arquivos em `Logs/`.

---

## 2) SID-master (atual)

### 2.1 Aplicação desktop (legado)
- Designer/Editor visual (WPF) com estrutura de “viewer” e páginas.
- Sistema de plugins:
  - descoberta/gerenciamento de plugins,
  - ordem de execução,
  - integração com o Designer.
- Bibliotecas e templates:
  - módulos voltados a SQL Server, PostgreSQL e outros templates do ecossistema.
- Módulos adicionais (no repositório):
  - integração com ClickUp,
  - pacote Rockwell (TPM),
  - componentes “Standard” e “Complex”.

### 2.2 Base web (embrionária)
- `SID-WEBAPI` (ASP.NET Core .NET 8): template inicial (Swagger + endpoint exemplo).
- `sid-react` (Vite + ReactFlow + Tailwind): UI placeholder inicial.

---

## 3) Plataforma final (alvo) — “o melhor dos mundos”

### 3.1 Entrada única e simples
- Portal único (web) que o usuário abre e resolve tudo:
  - escolher vendor,
  - selecionar origem,
  - executar ações,
  - abrir análise,
  - criar projeto do zero,
  - aplicar mudanças com segurança.

### 3.2 Visualização “do jeito que executa”
- Estrutural:
  - módulos, blocos/rotinas, dependências, tags/dados.
- Execução:
  - ordem de ciclo (OB/Tasks), chamadas (JSR/CALL), condições e “atalhos”.
- “Story mode” (iniciante):
  - passos guiados (ex.: “por onde o ciclo começa?”, “o que chama o quê?”).
- “Explorador” (especialista):
  - busca rápida, filtros avançados, destaque de caminhos críticos.

### 3.3 Autoria (novo) com bibliotecas SID
- Biblioteca navegável (componentes/blocos/templates), com:
  - descrição,
  - parâmetros,
  - exemplos,
  - dependências.
- Wizard de criação:
  - montar projeto do zero com templates.
- Editor de lógica/código:
  - inserir trechos,
  - gerar artefatos,
  - validar antes de aplicar.

### 3.4 Edição segura em projeto existente
- Aplicação de mudanças com:
  - diff,
  - prévia,
  - validação,
  - histórico e rollback.
- Modo “read-only” por padrão, com “habilitar edição” explícito.

### 3.5 Qualidade, rastreabilidade e colaboração
- Logs completos por ação (UI → API → pipeline).
- Registro entre IAs e humanos (o que mudou, onde, por quê, e como validar).
- Documentação gerada e navegável.

---

## 4) Limites e riscos (a tratar no backlog)

- Siemens TIA Openness: restrições de lock/read-only e necessidade de “attach” na instância GUI.
- Rockwell: disponibilidade/qualidade do export (`.L5X/.L5K`) e variações de projeto.
- Aplicar mudanças “de volta” ao ambiente do PLC pode exigir fluxo de export/import bem definido (sem prometer o que a ferramenta não suporta).

---

## 5) Referências no repositório

- Visão/arquitetura: `Side_Siemens-main v2/Side_Siemens-main/docs/PLATAFORMA_VISAO_E_ARQUITETURA.md`
- Backlog: `Side_Siemens-main v2/Side_Siemens-main/docs/PLANO_DE_TRABALHO_E_BACKLOG.md`
- Log entre IAs: `Side_Siemens-main v2/Side_Siemens-main/Logs/AI_SYNC_PLATFORM.md`

