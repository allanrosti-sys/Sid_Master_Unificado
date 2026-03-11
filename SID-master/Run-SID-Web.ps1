# Script de Inicialização do SID Web (Unificado)
# Objetivo: buildar o frontend, preparar o backend e servir a aplicação completa.
#
# Observação: este script foi escrito para funcionar mesmo quando o PowerShell
# bloqueia a execução de `npm.ps1` (ExecutionPolicy). Por isso usamos `npm.cmd`.

param(
    [switch]$NoBuild,
    [switch]$SmokeTest,
    [int]$Port = 5301
)

$ErrorActionPreference = "Stop"

$root = if ($PSScriptRoot) { $PSScriptRoot } else { (Get-Location).Path }
$apiProject = Join-Path $root "SID-WEBAPI\\SID-WEBAPI.csproj"
$reactDir = Join-Path $root "sid-react"
$wwwroot = Join-Path $root "SID-WEBAPI\\wwwroot"
$url = "http://localhost:$Port"

function Assert-PathExists {
    param([string]$Path, [string]$Label)
    if (-not (Test-Path $Path)) {
        throw "$Label não encontrado: $Path"
    }
}

function Invoke-Cmd {
    param([string]$Command)
    cmd /c $Command
    if ($LASTEXITCODE -ne 0) {
        throw "Falha ao executar: $Command (ExitCode=$LASTEXITCODE)"
    }
}

function Wait-PortListening {
    param([int]$ListenPort, [int]$TimeoutSeconds = 30)
    $deadline = (Get-Date).AddSeconds($TimeoutSeconds)
    while ((Get-Date) -lt $deadline) {
        $conn = Get-NetTCPConnection -LocalPort $ListenPort -State Listen -ErrorAction SilentlyContinue | Select-Object -First 1
        if ($conn) { return $true }
        Start-Sleep -Milliseconds 500
    }
    return $false
}

Write-Host "=== INICIANDO O SERVIÇO UNIFICADO SID WEB ===" -ForegroundColor Cyan
Write-Host ("Root: {0}" -f $root) -ForegroundColor DarkGray
Write-Host ("URL : {0}" -f $url) -ForegroundColor DarkGray

Assert-PathExists -Path $apiProject -Label "Projeto da API"
Assert-PathExists -Path $reactDir -Label "Pasta do frontend"

if (-not $NoBuild) {
    Write-Host "`n[1/4] Preparando Frontend (sid-react)..." -ForegroundColor Yellow
    Push-Location $reactDir
    try {
        if (-not (Test-Path "node_modules")) {
            Write-Host "Instalando dependências npm..." -ForegroundColor DarkYellow
            Invoke-Cmd "npm.cmd install"
        }

        Write-Host "Gerando build de produção (Vite)..." -ForegroundColor DarkYellow
        Invoke-Cmd "npm.cmd run build"
    } finally {
        Pop-Location
    }

    Write-Host "`n[2/4] Atualizando wwwroot da API..." -ForegroundColor Yellow
    if (Test-Path $wwwroot) { Remove-Item $wwwroot -Recurse -Force }
    New-Item -ItemType Directory -Path $wwwroot | Out-Null
    Copy-Item -Path (Join-Path $reactDir "dist\\*") -Destination $wwwroot -Recurse -Force
    Write-Host "Arquivos estáticos copiados para wwwroot." -ForegroundColor Green
} else {
    Write-Host "`n[1/4 & 2/4] Pulando build do frontend (-NoBuild ativo)..." -ForegroundColor DarkGray
}

Write-Host "`n[3/4] Compilando API ASP.NET Core..." -ForegroundColor Yellow
dotnet build $apiProject --nologo --verbosity quiet
if ($LASTEXITCODE -ne 0) { throw "Falha ao compilar a API (dotnet build). ExitCode=$LASTEXITCODE." }

if ($SmokeTest) {
    Write-Host "`n[TESTE] Smoke Test (API)..." -ForegroundColor Magenta

    $alreadyListening = Get-NetTCPConnection -LocalPort $Port -State Listen -ErrorAction SilentlyContinue | Select-Object -First 1
    if ($alreadyListening) {
        throw "A porta $Port já está em uso (PID $($alreadyListening.OwningProcess)). Encerre o processo e tente novamente."
    }

    # Importante: Start-Process não adiciona aspas automaticamente. Caminhos com espaço precisam ser citados.
    $dotnetArgs = "run --project `"$apiProject`" --urls `"$url`""
    $proc = Start-Process dotnet -ArgumentList $dotnetArgs -PassThru -NoNewWindow

    try {
        if (-not (Wait-PortListening -ListenPort $Port -TimeoutSeconds 40)) {
            throw "Servidor não iniciou na porta $Port dentro do tempo limite."
        }

        $health = Invoke-RestMethod -Uri "$url/api/health" -Method Get -TimeoutSec 10
        Write-Host "  [OK] GET /api/health" -ForegroundColor Green

        $version = Invoke-RestMethod -Uri "$url/api/version" -Method Get -TimeoutSec 10
        Write-Host ("  [OK] GET /api/version -> {0}" -f $version.version) -ForegroundColor Green

        $components = Invoke-RestMethod -Uri "$url/api/catalog/components" -Method Get -TimeoutSec 10
        $count = if ($components) { ($components | Measure-Object).Count } else { 0 }
        Write-Host ("  [OK] GET /api/catalog/components -> {0} item(ns)" -f $count) -ForegroundColor Green

        Write-Host "`nSmoke Test concluído com sucesso." -ForegroundColor Cyan
        exit 0
    } finally {
        if ($proc -and -not $proc.HasExited) {
            Stop-Process -Id $proc.Id -Force -ErrorAction SilentlyContinue
        }
    }
}

Write-Host "`n[4/4] Iniciando Servidor..." -ForegroundColor Green
Write-Host "----------------------------------------------------"
Write-Host "UI         : $url"
Write-Host "Health     : $url/api/health"
Write-Host "Versão     : $url/api/version"
Write-Host "Dashboard  : $url/dashboard"
Write-Host "Pressione Ctrl+C para parar o servidor."
Write-Host "----------------------------------------------------"

dotnet run --project $apiProject --urls $url
