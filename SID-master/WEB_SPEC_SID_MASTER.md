# Especificação Técnica — SID Master Web

**Data base:** 2026-03-11  
**Autor:** Gemini (Full-Stack Engineer)
**Objetivo:** Detalhar a arquitetura, componentes, fluxos e validação para a primeira versão da plataforma web do SID-master, garantindo a unificação da API e da UI em um único serviço.

---

## 1. Arquitetura de Serviço Único

O SID Web será executado como um único serviço ASP.NET Core que serve tanto a API quanto a interface do usuário (Single Page Application).

- **Host/Porta:** `http://localhost:5301`
- **Servidor:** ASP.NET Core (.NET 8)
- **UI:** React 18 + Vite (build estático)
- **Roteamento:**
  - Requisições que começam com `/api/` são tratadas pela API ASP.NET.
  - Todas as outras requisições são direcionadas para a SPA React, que gerencia o roteamento no lado do cliente.

### Implementação (`Program.cs`)

1.  **Servir Arquivos Estáticos:** O serviço será configurado para servir os arquivos da pasta `wwwroot`, que conterá o build de produção do `sid-react`.
2.  **SPA Fallback:** Para qualquer rota não-API que não corresponda a um arquivo estático, o servidor retornará `index.html`. Isso permite que o React Router domine a navegação.
3.  **Proxy de Desenvolvimento (Opcional):** Em ambiente de desenvolvimento (`ASPNETCORE_ENVIRONMENT=Development`), o `Program.cs` pode ser configurado para redirecionar as requisições da UI para o servidor de desenvolvimento do Vite (`http://localhost:5300`), permitindo Hot Module Replacement (HMR).

---

## 2. Design System Mínimo (Tailwind CSS)

A UI será construída com Tailwind CSS, seguindo um conjunto de tokens e convenções para garantir consistência.

- **Paleta de Cores (Exemplo):**
  - `primary`: `blue-600`
  - `secondary`: `gray-500`
  - `background`: `gray-100` (claro), `gray-900` (escuro)
  - `text`: `gray-800` (claro), `gray-200` (escuro)
  - `success`: `green-500`
  - `error`: `red-600`
  - `warning`: `yellow-500`
- **Tipografia:** Fonte `Inter` ou `Segoe UI`.
  - `h1`: `@apply text-2xl font-bold;`
  - `h2`: `@apply text-xl font-semibold;`
  - `body`: `@apply text-base font-normal;`
- **Componentes (Convenções de Classe):**
  - **Button:** `@apply px-4 py-2 rounded-md font-semibold text-white;`
    - `Button-primary`: `@apply bg-blue-600 hover:bg-blue-700;`
    - `Button-secondary`: `@apply bg-gray-500 hover:bg-gray-600;`
  - **Card:** `@apply bg-white rounded-lg shadow-md p-6;`
  - **Modal:** Fundo semi-transparente (`bg-black/50`) com um `Card` centralizado.
- **Layout:**
  - **Topbar:** `h-16`, `flex`, `items-center`, `justify-between`, `px-6`, `bg-white`, `shadow-sm`.
  - **Sidebar:** `w-64`, `bg-gray-800`, `text-white`, `p-4`.

---

## 3. Modelo de Dados e API Endpoints (MVP)

### Modelo JSON: `Componente`

```json
{
  "id": "string",
  "nome": "string",
  "descricao": "string",
  "tags": ["string"],
  "parametros": [
    {
      "nome": "string",
      "tipo": "string" | "number" | "boolean",
      "descricao": "string",
      "obrigatorio": "boolean",
      "valorPadrao": "any"
    }
  ],
  "artefatosGerados": ["string"],
  "dependencias": ["string"],
  "versao": "string"
}
```

### Modelo JSON: `Template`

```json
{
  "id": "string",
  "nome": "string",
  "descricao": "string",
  "componentes": ["string"]
}
```

### API Endpoints (`/api/*`)

- **`GET /api/health`**
  - **Descrição:** Verifica a saúde do serviço.
  - **Resposta (200 OK):** `application/json`
    ```json
    { "status": "ok", "timestamp": "2026-03-11T17:00:00Z" }
    ```

- **`GET /api/version`**
  - **Descrição:** Retorna a versão da aplicação.
  - **Resposta (200 OK):** `application/json`
    ```json
    { "version": "1.0.0-mvp" }
    ```

- **`GET /api/library/components`**
  - **Descrição:** Retorna a lista de todos os componentes disponíveis.
  - **Resposta (200 OK):** `application/json` (Array de `Componente`)
    ```json
    [
      {
        "id": "comp-001",
        "nome": "Bloco de Alarme Padrão",
        "descricao": "Componente para gerenciamento e sinalização de alarmes simples.",
        "tags": ["siemens", "s7-1500", "alarme"],
        "parametros": [
          { "nome": "TagInput", "tipo": "string", "descricao": "Tag de entrada que dispara o alarme.", "obrigatorio": true },
          { "nome": "TempoAtraso", "tipo": "number", "descricao": "Atraso em segundos para ativação.", "obrigatorio": false, "valorPadrao": 0 }
        ],
        "artefatosGerados": ["AlarmBlock.scl", "AlarmDB.db"],
        "dependencias": [],
        "versao": "1.1.0"
      },
      {
        "id": "comp-002",
        "nome": "Controle de Motor (Reversão)",
        "descricao": "Controla um motor com capacidade de reversão de sentido.",
        "tags": ["rockwell", "controllogix", "motor"],
        "parametros": [
          { "nome": "Enable", "tipo": "boolean", "descricao": "Habilita o bloco.", "obrigatorio": true },
          { "nome": "ForwardCmd", "tipo": "boolean", "descricao": "Comando para girar para frente.", "obrigatorio": true },
          { "nome": "ReverseCmd", "tipo": "boolean", "descricao": "Comando para girar para trás.", "obrigatorio": true }
        ],
        "artefatosGerados": ["MotorControl.L5X"],
        "dependencias": ["StandardLib_v1.2"],
        "versao": "2.0.0"
      }
    ]
    ```

---

## 4. Estrutura e Fluxos da UI (`sid-react`)

### Estrutura de Pastas (sugestão)

```
src/
|-- api/             # Funções para chamar a API
|-- components/      # Componentes reutilizáveis (Button, Card, etc.)
|-- layouts/         # Estruturas de página (MainLayout com Sidebar+Topbar)
|-- pages/           # Telas da aplicação (Home, Library, Wizard)
|-- routes/          # Configuração do React Router
|-- App.jsx
|-- main.jsx
```

### Rotas e Telas

- **`/` (Home):**
  - **Conteúdo:** Página de boas-vindas com uma breve descrição do SID Web e links para as principais seções ("Ver Bibliotecas", "Criar Projeto").
- **`/library` (Bibliotecas):**
  - **Conteúdo:**
    - Campo de busca para filtrar componentes por nome, tag, etc.
    - Lista de `Card`s, cada um representando um componente.
    - Ao clicar em um card, abre um `Modal` com os detalhes completos do componente (parâmetros, descrição, etc.).
  - **Lógica:**
    - Ao carregar, chama `GET /api/library/components`.
    - Exibe `Loading` enquanto a requisição está em andamento.
    - Exibe `Error` se a requisição falhar.
    - Exibe `EmptyState` se a lista de componentes estiver vazia.
- **`/map-demo` (Mapa Demo):**
  - **Conteúdo:** Uma instância do `ReactFlow` renderizando um grafo estático simples (2 nós, 1 aresta) para validar a biblioteca de grafos.
- **`/wizard` (Wizard de Criação - MVP):**
  - **Conteúdo:** Um componente `Stepper` com passos:
    1.  "Escolha um Template": Lista de templates disponíveis (pode ser mockado no MVP).
    2.  "Configure Parâmetros": Formulário para preencher os parâmetros do template.
    3.  "Gerar e Baixar": Confirmação e botão para baixar um `.zip` com os artefatos gerados.
  - **Lógica:** Foco na UI no MVP. A geração real do `.zip` pode ser implementada depois.

---

## 5. Script de Execução (`Run-SID-Web.ps1`)

Este script será o ponto de entrada único para iniciar o serviço.

```powershell
# Run-SID-Web.ps1

# Parâmetros
$reactAppPath = ".\sid-react"
$apiPath = ".\SID-WEBAPI"
$apiPort = 5301

# Passo 1: Instalar dependências do frontend (se necessário)
Write-Host "Verificando dependências do Node..."
if (-not (Test-Path "$reactAppPath
ode_modules")) {
    Write-Host "Instalando dependências do npm..."
    Push-Location $reactAppPath
    npm install
    Pop-Location
}

# Passo 2: Build do React
Write-Host "Iniciando o build da aplicação React..."
Push-Location $reactAppPath
npm run build
Pop-Location

# Passo 3: Limpar wwwroot anterior e copiar build novo
$wwwRootPath = "$apiPath\wwwroot"
if (Test-Path $wwwRootPath) {
    Write-Host "Limpando diretório wwwroot anterior..."
    Remove-Item $wwwRootPath -Recurse -Force
}
Write-Host "Copiando arquivos do build para wwwroot..."
Copy-Item -Path "$reactAppPath\dist" -Destination $wwwRootPath -Recurse

# Passo 4: Iniciar a API ASP.NET
Write-Host "Iniciando o servidor ASP.NET Core na porta $apiPort..."
Write-Host "----------------------------------------------------"
Write-Host "A aplicação estará disponível em:"
Write-Host "UI: http://localhost:$apiPort"
Write-Host "API Health: http://localhost:$apiPort/api/health"
Write-Host "Pressione CTRL+C para parar o servidor."
Write-Host "----------------------------------------------------"

dotnet run --project $apiPath --launch-profile "http"

# Fim do script
```

---

## 6. Checklist de Validação Técnica

- [ ] Executar `.\Run-SID-Web.ps1`. O script deve completar todas as etapas sem erros.
- [ ] O servidor ASP.NET deve iniciar e escutar na porta `5301`.
- [ ] Acessar `http://localhost:5301` no navegador. A página `Home` do `sid-react` deve ser exibida.
- [ ] Acessar `http://localhost:5301/library` diretamente no navegador. A página `Bibliotecas` deve ser exibida (sem 404), e os dados da API mockada devem ser carregados e mostrados na tela.
- [ ] Acessar `http://localhost:5301/api/health` e `http://localhost:5301/api/library/components`. As respostas JSON devem ser válidas.
- [ ] Verificar o log do servidor ASP.NET. Requisições para `/api/*` devem ser logadas como tratadas pelo Kestrel, e outras rotas devem fazer o fallback para `index.html`.
- [ ] O código-fonte modificado segue os padrões definidos (comentários em PT-BR, estrutura de pastas, etc.).
