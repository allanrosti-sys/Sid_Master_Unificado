# SID-master → Totalmente Web (Plano base)

**Data base:** 2026-03-11  
**Objetivo:** descrever tudo que precisa ser feito para converter o SID-master (desktop/legado) em um produto totalmente web, integrado à plataforma “Puchta PLC Insight”.

---

## 1) O que o SID é hoje (resumo objetivo)

O `SID-master` hoje é um conjunto de projetos .NET com:
- Um app desktop (WPF / .NET Framework) que centraliza **Designer + Plugins**.
- Bibliotecas e templates (Standard/Complex/PGTemplate/MSQL/ClickUp/Rockwell etc.).
- Uma base web embrionária:
  - `SID-WEBAPI` (ASP.NET Core .NET 8).
  - `sid-react` (Vite + ReactFlow + Tailwind).

**Meta:** levar as capacidades valiosas (bibliotecas, templates, geração e edição) para um fluxo web moderno.

---

## 2) Princípios da migração (para não quebrar tudo)

1. **Strangler Pattern:** migrar por fatias; manter o legado funcionando até o web cobrir os fluxos.
2. **Extrair núcleo reutilizável:** tudo que for “regra de negócio” sai de UI WPF e vira biblioteca/API.
3. **Web read-only primeiro:** catálogo e visualização antes de edição/aplicação de mudanças.
4. **Edição sempre segura:** diff, validação, histórico, rollback.
5. **Comentário em português:** padrão obrigatório ao escrever código novo/migrado.

---

## 3) Arquitetura-alvo do SID Web (proposta)

### 3.1 Backend (`SID-WEBAPI`)
Evoluir o `SID-WEBAPI` para um **Core API** com:
- Autenticação local (se necessário) e sessão.
- Endpoints de biblioteca/templates.
- Endpoints de geração (“criar projeto do zero”).
- Endpoints de “ChangeSet” (preview/apply/rollback) para projetos carregados.

### 3.2 Frontend (`sid-react`)
Evoluir o `sid-react` para:
- Portal do SID (biblioteca, wizard, editor).
- Integração com mapa/execução do Puchta (abrir análise no mesmo contexto).
- UI limpa, com “modo iniciante” (guiado) e “modo especialista” (rápido).

### 3.3 Integração com Puchta PLC Insight
O programa unificado define um **portal único** e contratos comuns.

- Visão/arquitetura (programa): `Side_Siemens-main v2/Side_Siemens-main/docs/PLATAFORMA_VISAO_E_ARQUITETURA.md`
- Backlog/fases (programa): `Side_Siemens-main v2/Side_Siemens-main/docs/PLANO_DE_TRABALHO_E_BACKLOG.md`
- Log unificado (programa): `Side_Siemens-main v2/Side_Siemens-main/Logs/AI_SYNC_PLATFORM.md`

No curto prazo, o SID Web pode coexistir com os serviços do Puchta, mas deve:
- herdar o `ProjectContext` (vendor + origem + id),
- reutilizar o `GraphContract` para visualizações,
- e evoluir para um fluxo único de navegação (sem o usuário “pular” entre produtos).

---

## 4) Entregas (em ordem recomendada)

### Entrega 1 — Inventário e catálogo (MVP)
- Levantar tudo que o SID oferece hoje como “biblioteca”:
  - o que vira componente,
  - parâmetros,
  - exemplos,
  - dependências.
- Criar um “catálogo web” (read-only) com busca e filtros.

**Critério de pronto**
- Usuário encontra um componente em menos de 10 segundos (busca + filtros).
- A tela do componente explica propósito, parâmetros e um exemplo de uso.

### Entrega 2 — Wizard de criação do zero
- Criar 1–3 templates “vitrine” (bem guiados e com documentação).
- Gerar artefatos (estrutura + arquivos) em uma pasta de workspace.
- Exportar pacote (zip) para o usuário baixar/compartilhar.

### Entrega 3 — Editor (prévia e diff)
- Editor de texto/código com:
  - validação,
  - diff,
  - histórico local por projeto.
- Ainda sem aplicar no vendor (foco em UX e contratos).

### Entrega 4 — Aplicar mudanças com segurança (vendor-aware)
- Definir e implementar `ChangeSet`:
  - preview,
  - apply,
  - rollback por snapshot.
- Integrar aos fluxos reais Siemens/Rockwell respeitando limitações das ferramentas.

### Entrega 5 — “Produto”
- Design system consolidado.
- Logs e diagnóstico (facilitar suporte).
- Guia de instalação/atualização/release.

---

## 5) Itens técnicos obrigatórios (checklist)

- Modelo de dados:
  - `Componente`, `Template`, `Projeto`, `Artefato`, `ChangeSet`.
- API:
  - versionamento (`/api/v1/...`),
  - erros padronizados (mensagens curtas e úteis),
  - logs por request (correlação).
- Segurança:
  - validação de caminhos (evitar path traversal),
  - execução de scripts com lista permitida (sem “rodar qualquer coisa”).
- Qualidade:
  - testes mínimos de contrato e geração,
  - smoke tests de UI.
- Código:
  - **comentários em português** em todo código novo/migrado.

---

## 6) Onde registrar progresso e decisões

- Log unificado (programa): `Side_Siemens-main v2/Side_Siemens-main/Logs/AI_SYNC_PLATFORM.md`
- Log local (SID-master): `SID-master/AI_SYNC_PLATFORM.md`
