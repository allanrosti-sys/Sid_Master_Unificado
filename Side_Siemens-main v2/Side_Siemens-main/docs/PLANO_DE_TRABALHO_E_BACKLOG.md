# Plano de Trabalho e Backlog — Plataforma Web (Puchta PLC Insight + SID)

**Data base:** 2026-03-11  
**Objetivo:** listar atividades e entregas para chegar em um produto altamente profissional, unificando análise multi-vendor (Puchta) e autoria/bibliotecas (SID) em uma experiência web única.

---

## 1) Regras do projeto (não negociáveis)

- **Código comentado em português**, sem comentários genéricos e sem “mentir” sobre comportamento.
- Toda mudança relevante registra:
  - o que foi feito,
  - arquivos alterados,
  - como validar,
  - risco/limitação.
  Em: `Side_Siemens-main v2/Side_Siemens-main/Logs/AI_SYNC_PLATFORM.md`.
- Contratos de dados versionados (grafo, execução, mudanças).
- UX: primeiro funciona, depois fica bonito; mas beleza e fluidez entram cedo (não só no final).

---

## 2) Fases (macro)

### Fase 0 — Alinhamento e base (1–3 dias)
**Entregáveis**
- Visão e arquitetura fechadas (decisões pendentes listadas).
- Backlog priorizado.
- Padrões de qualidade definidos.

**Atividades**
- Definir “entrada única” (porta/URL e fluxo).
- Definir contrato `ProjectContext` e `GraphContract` (versão 1).
- Definir estratégia de armazenamento (workspace local inicialmente).

### Fase 1 — Fundação técnica (1–2 semanas)
**Entregáveis**
- Build/Run padronizados (um comando por serviço).
- Log estruturado (UI → API → pipeline).
- Contrato de grafo validado com amostras Siemens e Rockwell.

**Atividades**
- Criar “workspace” do projeto (pasta padrão, configuração, cache).
- Padronizar respostas de API e erros (mensagens curtas e úteis ao usuário final).
- Performance mínima (projetos grandes não podem travar UI).

### Fase 2 — SID para Web (MVP) (2–4 semanas)
**Entregáveis**
- Web: catálogo de bibliotecas SID (navegação + busca + detalhes).
- Web: wizard para criar projeto do zero com template simples.
- API: endpoints para listar bibliotecas/templates e gerar artefatos.

**Atividades**
- Inventariar bibliotecas atuais do SID (o que vira “componente”).
- Extrair lógica reutilizável para um núcleo (sem dependência de WPF).
- Definir formato de “componente” (metadados + parâmetros + geração).

### Fase 3 — Integração “Portal Único” (1–3 semanas)
**Entregáveis**
- Um único painel web para:
  - configurar vendor/origem,
  - abrir mapa/análise,
  - criar projeto do zero (SID),
  - abrir biblioteca e documentação.

**Atividades**
- Navegação consistente e “modo iniciante” (primeiro contato).
- SSO local (mesma sessão/estado entre módulos).
- Ajustar CORS/rotas/portas para reduzir fricção (ideal: 1 domínio/porta).

### Fase 4 — Edição segura em projeto existente (3–6 semanas)
**Entregáveis**
- Editor com diff + validação + histórico.
- Aplicação de mudanças via fluxo seguro (export/import) por vendor.

**Atividades**
- Definir `ChangeSet` (contrato) e armazenamento de histórico.
- Implementar preview (“o que vai mudar”) antes de aplicar.
- Implementar rollback básico (por snapshot).

### Fase 5 — Produto profissional (contínuo)
**Entregáveis**
- UX refinada (fluida, limpa, rápida).
- Documentação gerada com padrão visual.
- Pacote de release (instalação, atualização, logs e diagnóstico).

**Atividades**
- Design system (cores, tipografia, componentes, acessibilidade).
- Telemetria local (opcional) para saber o que o usuário usa/onde trava.
- Hardening de segurança (permissões, path traversal, exec de scripts).

---

## 3) Backlog (épicos e tarefas)

### Épico A — Contratos e contexto
- Definir `ProjectContext v1` (vendor, origem, id, status, timestamps).
- Definir `GraphContract v1` (tipos, estilos por vendor, metadados mínimos).
- Definir `ExecutionContract v1` (representação de ciclo/ordem e chamadas).

### Épico B — Experiência do usuário (portal)
- Home com “passo-a-passo” (iniciante) e “atalhos” (avançado).
- Seleção de origem com validação e mensagens claras.
- Central de ações com “o que faz / quanto demora / o que gera”.

### Épico C — Catálogo de bibliotecas (SID)
- Modelo de “componente” com metadados e parâmetros.
- Busca, filtros, exemplos e dependências.
- Geração de projeto (MVP) com template simples.

### Épico D — Mapa e execução
- Melhorar layout (evitar “grudar tudo no centro”; salvar posições).
- Filtros coerentes por vendor (somente o que faz sentido aparece).
- Destaque de “caminho crítico” e navegação por chamadas.

### Épico E — Editor e mudanças
- Editor com diff e validação.
- Histórico por projeto e por módulo.
- Aplicação/rollback por snapshot.

### Épico F — Qualidade e entrega
- Scripts de build/run padronizados.
- Testes mínimos (contratos + smoke).
- Guia de instalação e diagnóstico.

---

## 4) Critérios de aceite (exemplos práticos)

- Usuário iniciante consegue, em 2–3 cliques:
  - carregar um projeto e visualizar o fluxo principal sem ler log.
- Usuário avançado consegue:
  - encontrar uma rotina/bloco em menos de 10 segundos (busca/filtros).
- A plataforma mantém:
  - logs claros e acionáveis (erros explicam causa e solução).
- Toda entrega atualiza:
  - `CATALOGO_FUNCIONALIDADES.md` e o log `AI_SYNC_PLATFORM.md`.

