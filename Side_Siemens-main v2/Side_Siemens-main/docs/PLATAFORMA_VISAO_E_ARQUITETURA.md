# Puchta PLC Insight + SID (Migração para Web) — Visão e Arquitetura

**Data base:** 2026-03-11  
**Objetivo deste documento:** alinhar a visão do produto, a arquitetura-alvo e as decisões que precisamos tomar para (1) evoluir o **Puchta PLC Insight** e (2) converter o **SID-master** para uma solução **totalmente web**, com uma experiência única e profissional.

---

## 1) Visão do produto

Criar uma plataforma web para automação industrial que permita:

1. **Ler um projeto existente** (Siemens/Rockwell inicialmente) e **entender como ele executa** (estrutura + fluxo de execução).
2. **Criar um projeto do zero** usando bibliotecas/componentes do SID (blocos, templates, padrões).
3. **Inserir/alterar partes de código** em um projeto previamente carregado, com validação e rastreabilidade.
4. Ter **UX fluida e limpa**, adequada para:
   - um usuário com pouco conhecimento de automação (modo guiado),
   - e um usuário avançado (modo especialista), sem “pagar preço” de complexidade na UI principal.

**Princípio-mestre:** “O usuário sempre sabe onde está, o que pode fazer agora, e o que vai acontecer quando clicar”.

---

## 2) Personas (para orientar a UX)

### 2.1 Operador/Analista (iniciante)
- Quer carregar um projeto e “ver o que ele faz”.
- Precisa de termos simples, passos guiados, e validações claras.
- Evita janelas técnicas, logs longos e opções perigosas.

### 2.2 Engenheiro de Automação (avançado)
- Quer navegar rápido, filtrar, comparar e editar.
- Precisa de visão de chamadas, dependências, tags e geração de documentação.
- Exige consistência, performance e confiabilidade.

### 2.3 Integrador/Dev (plataforma)
- Quer scripts reprodutíveis, API estável, e contratos de dados versionados.
- Precisa de logs estruturados, testes e CI.

---

## 3) Jornadas principais (User Journeys)

### Jornada A — “Carregar e entender”
1. Selecionar `Vendor` (Auto/Siemens/Rockwell) + origem (pasta/arquivo).
2. Validar origem (feedback imediato).
3. Gerar:
   - mapa estrutural (grafos),
   - fluxo de execução (quando aplicável),
   - documentação navegável.
4. Abrir o mapa visual e explorar com filtros coerentes ao vendor.

### Jornada B — “Criar do zero (bibliotecas SID)”
1. Escolher um **template** (ex.: “CIP”, “Esteira”, “Bomba”, “Sistema de alarmes”).
2. Montar o projeto com blocos/bibliotecas (drag-and-drop + parâmetros).
3. Gerar artefatos (código, configuração, documentação).

### Jornada C — “Aplicar mudanças em um projeto existente”
1. Abrir projeto existente.
2. Selecionar um módulo/rotina/bloco alvo.
3. Inserir/alterar trechos com:
   - validação,
   - “diff” e prévia,
   - histórico e rollback.
4. Exportar resultado (e opcionalmente gerar pacote de release).

---

## 4) Estado atual (resumo técnico)

### 4.1 Puchta PLC Insight (Side_Siemens-main v2)
- Painel web (PowerShell `HttpListener`) aciona scripts e expõe endpoints para UI.
- App de análise separado: `FastAPI (8021)` + `Vite/React (5173)` com contrato de grafo.
- Multi-vendor já iniciado (Siemens + Rockwell), com regras de detecção e pipeline no backend.

### 4.2 SID-master
- Aplicação principal “legacy” é desktop (WPF + plugins, .NET Framework) com lógica relevante de bibliotecas/templates.
- Existe um `SID-WEBAPI` (ASP.NET Core .NET 8) e um `sid-react` (Vite/ReactFlow), ainda como base.

---

## 5) Arquitetura-alvo (proposta)

### 5.1 Objetivo de arquitetura
Unificar em um **ponto de entrada web único** com três camadas bem definidas:

1. **Web Manager (Portal)**: configuração de origem, vendor, ações (exportar, documentar, mapear, editar, gerar pacote).
2. **Core API (backend)**: contrato único de projeto + grafo + execução + edição.
3. **UI de Análise/Autoria (frontend)**: mapa, explorer, editor, wizard de criação, documentação.

### 5.2 Contratos fundamentais (o que não pode “quebrar”)
- `ProjectContext` (vendor, origem, id, metadados, versões).
- `GraphContract` (nodes/edges padronizados, tipos, cores/estilos por vendor).
- `ExecutionContract` (representação do ciclo/ordem de execução, quando aplicável).
- `ChangeSet` (alterações propostas/aplicadas, diff, autor, validação, rollback).

### 5.3 Decisões de arquitetura (precisamos fechar)
1. **Backend unificado**:
   - **Curto prazo:** manter Python (parsers) + endpoints existentes e integrar o SID via API e contratos comuns.
   - **Médio prazo:** avaliar convergência para um “Core API” único (sem pressa), mantendo parsers como módulos.
2. **Persistência e versionamento de projetos**:
   - onde ficam os projetos “criados do zero” (workspace local / repositório / banco),
   - como versionar (Git-like, snapshots, ou ambos).
3. **Edição segura**:
   - como representar e aplicar mudanças em Siemens/Rockwell (limitações de ferramentas, permissões, export/import).

---

## 6) Padrões de qualidade (obrigatórios)

- **Código sempre comentado em português**, com comentários objetivos (o “porquê”, não só o “o quê”).
- Logs estruturados e rastreáveis (correlação por request e por ação do usuário).
- UX: ações perigosas exigem confirmação e “prévia do impacto”.
- Documentação: toda feature nova atualiza o catálogo e o backlog.
- Testes mínimos por camada (backend: unidade/integração; frontend: smoke/contratos).

---

## 7) Entregáveis da migração SID → Web (visão)

### MVP (primeira entrega útil)
- Catálogo de bibliotecas do SID no browser.
- Criador simples de projeto (wizard) com geração de artefatos.
- Integração com o Puchta: abrir mapa/análise a partir do mesmo portal.

### Próximas entregas
- Editor com diff + validação.
- Aplicação de mudanças em projeto carregado (com pipeline seguro).
- Experiência “iniciante” (guiada) e “especialista” (rápida).

---

## 8) Onde registrar o trabalho em andamento

- Log entre IAs: `Side_Siemens-main v2/Side_Siemens-main/Logs/AI_SYNC_PLATFORM.md`
- Backlog e fases: `Side_Siemens-main v2/Side_Siemens-main/docs/PLANO_DE_TRABALHO_E_BACKLOG.md`
- Catálogo de funcionalidades: `Side_Siemens-main v2/Side_Siemens-main/docs/CATALOGO_FUNCIONALIDADES.md`

