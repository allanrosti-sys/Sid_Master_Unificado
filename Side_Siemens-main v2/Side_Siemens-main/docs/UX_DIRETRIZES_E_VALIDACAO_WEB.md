# Diretrizes de UX e Validação Web — Plataforma Unificada

**Data base:** 2026-03-11  
**Autor:** Gemini (UX Lead)
**Objetivo:** Estabelecer diretrizes de experiência do usuário (UX), interface (UI) e um plano de validação claro para a integração da plataforma Puchta PLC Insight e a migração do SID-master para a web.

---

## 1. Visão e Princípios de UX

A plataforma deve ser **confiável, clara e eficiente**. O usuário nunca deve se sentir perdido.

- **Clareza em primeiro lugar:** A interface deve comunicar o que está acontecendo, o que é esperado do usuário e qual o resultado de suas ações. Use linguagem simples e direta.
- **Consistência é chave:** Componentes, cores, ícones e fluxos de navegação devem ser consistentes em toda a aplicação. Uma ação deve sempre gerar o mesmo tipo de resposta visual e funcional.
- **Feedback imediato:** O sistema deve fornecer feedback instantâneo para interações do usuário (cliques, preenchimento de formulários, etc.), usando estados de *loading*, *success*, *error* e *disabled*.
- **Modos de uso (Iniciante vs. Especialista):**
  - **Iniciante:** A interface deve guiar o usuário com passo-a-passo (wizards, steppers), textos explicativos e menos opções visíveis por padrão.
  - **Especialista:** A interface deve oferecer atalhos, filtros avançados, visualizações densas de dados e ações rápidas na mesma tela.

---

## 2. Integração Puchta PLC Insight ↔ SID Web

No curto prazo, os sistemas coexistirão em portas diferentes. A integração será feita através de **links contextuais** e uma identidade visual compartilhada.

- **Puchta PLC Insight (Análise):** `http://localhost:5173` (Frontend) e `http://localhost:8021` (Backend)
- **SID Web (Autoria/Bibliotecas):** `http://localhost:5300` (Frontend) e `http://localhost:5301` (Backend)
- **Painel Legado (Manager):** Porta variável (ex: `http://localhost:8099`)

### Fluxo de Integração (MVP)

1.  **Do Painel para o Mundo Web:**
    - O painel legado (`WebServer.ps1`) terá links claros:
      - "Analisar Projeto" → Abre `http://localhost:5173` passando o contexto do projeto (ex: via query string `?projectPath=...`).
      - "Explorar Bibliotecas SID" → Abre `http://localhost:5300/library`.
      - "Criar Novo Projeto (SID)" → Abre `http://localhost:5300/wizard`.

2.  **Entre Aplicações Web:**
    - A UI do Puchta e do SID compartilharão um **header/topbar comum** (mesmo que em apps separados) com links para navegar entre as "casas".
    - Ex: Dentro do mapa do Puchta, um botão "Usar Template SID" pode levar ao wizard do SID, pré-preenchendo o vendor.

---

## 3. Diretrizes de Microcopy (Tom e Voz)

- **Idioma:** PT-BR.
- **Tom:** Profissional, mas acessível. Evite jargões excessivos no modo iniciante.
- **Exemplos:**
  - **Botões:** Use verbos no infinitivo (Ex: "Salvar Alterações", "Abrir Projeto", "Gerar Documentação").
  - **Mensagens de erro:** Explique o **problema** e a **solução** de forma simples.
    - **Ruim:** "Erro 500."
    - **Bom:** "Não foi possível conectar à API. Verifique se o serviço `SID-WEBAPI` está em execução e tente novamente."
  - **Textos de ajuda:** Concisos e contextuais. Use `(?)` ícones ou tooltips.

---

## 4. Checklist de Validação de UI/UX (Obrigatório)

Este checklist deve ser executado a cada nova entrega de frontend para garantir a consistência e qualidade da experiência.

### 4.1. Validação do Puchta PLC Insight (Análise)

1.  **Acesso:**
    - [ ] Abrir `http://localhost:5173`. A aplicação carrega sem erros.
    - [ ] Acessar um projeto via link do painel legado (ex: `http://localhost:5173/?projectPath=...`). O mapa do projeto correto é carregado.

2.  **Funcionalidade (Não Regressão):**
    - [ ] O mapa do projeto é renderizado corretamente.
    - [ ] Zoom, pan e seleção de nós funcionam.
    - [ ] Filtros por tipo de bloco continuam operantes.
    - [ ] O painel de detalhes exibe as informações do nó selecionado.

3.  **Integração:**
    - [ ] O header da aplicação contém um link claro para "Bibliotecas SID" (`http://localhost:5300/library`).
    - [ ] O header da aplicação contém um link claro para "Início" (página inicial do portal unificado, se houver).

### 4.2. Validação do SID Web (Autoria/Bibliotecas)

1.  **Acesso e Serviços:**
    - [ ] Executar o script `Run-SID-Web.ps1`.
    - [ ] Acessar `http://localhost:5301/api/health`. O status `{ "status": "ok" }` é retornado.
    - [ ] Acessar `http://localhost:5300`. A página principal do SID Web é carregada sem erros.
    - [ ] Abrir o console do navegador. Não deve haver erros de CORS ou 404 para os assets da página.

2.  **Navegação e Rotas (SPA):**
    - [ ] Acessar diretamente `http://localhost:5300/library`. A página de bibliotecas carrega corretamente (sem 404).
    - [ ] Acessar diretamente `http://localhost:5300/wizard`. A página do wizard carrega corretamente.
    - [ ] Navegar entre as páginas ("Início", "Bibliotecas", "Wizard") usando os links da UI. A transição é suave e o conteúdo é atualizado.

3.  **Estados da Interface:**
    - [ ] **Loading:** Em uma página que consome dados da API (ex: `/library`), simular lentidão na rede (via DevTools). Um indicador de carregamento (spinner, skeleton) deve ser exibido.
    - [ ] **Error:** Parar o serviço `SID-WEBAPI` e recarregar a página `/library`. Uma mensagem de erro clara (conforme microcopy) deve ser exibida no lugar da lista.
    - [ ] **Empty:** Com a API rodando, mas sem componentes cadastrados, a página `/library` deve exibir um "estado vazio" (`EmptyState`) com uma mensagem amigável (ex: "Nenhum componente encontrado") e talvez um botão para "Sugerir Componente".

4.  **Acessibilidade Mínima:**
    - [ ] **Foco de Teclado:** Navegar pela aplicação usando apenas a tecla `Tab`. Todos os elementos interativos (links, botões, inputs) devem receber foco de forma visível e lógica.
    - [ ] **Contraste:** Verificar se os textos principais sobre os fundos têm contraste suficiente (usar ferramentas de desenvolvedor do navegador).
    - [ ] **Labels:** Todos os campos de formulário (`input`, `select`) devem ter uma `label` associada. Ícones usados como botões devem ter `aria-label`.

### 4.3. Validação Geral (Ambos os Apps)
- [ ] O Design System (cores, fontes, espaçamentos) é consistente entre as duas aplicações web.
- [ ] Os textos e mensagens estão em PT-BR, sem erros de digitação ou tradução.
- [ ] A aplicação é responsiva o suficiente para ser usada confortavelmente em telas de desktop comuns (não precisa ser mobile-first no MVP).
