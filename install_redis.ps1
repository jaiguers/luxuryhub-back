# Script de instalación de Redis para Windows
# Ejecutar como Administrador

Write-Host "🚀 Instalando Redis para LuxuryHub..." -ForegroundColor Green

# Verificar si Docker está disponible
$dockerAvailable = Get-Command docker -ErrorAction SilentlyContinue

if ($dockerAvailable) {
    Write-Host "📦 Instalando Redis con Docker..." -ForegroundColor Yellow
    
    # Detener contenedor existente si existe
    docker stop redis 2>$null
    docker rm redis 2>$null
    
    # Crear nuevo contenedor Redis
    docker run -d --name redis -p 6379:6379 redis:alpine
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Redis instalado exitosamente con Docker" -ForegroundColor Green
        Write-Host "🌐 Redis disponible en: localhost:6379" -ForegroundColor Cyan
    } else {
        Write-Host "❌ Error al instalar Redis con Docker" -ForegroundColor Red
    }
} else {
    Write-Host "📦 Docker no disponible, intentando con Chocolatey..." -ForegroundColor Yellow
    
    # Verificar si Chocolatey está disponible
    $chocoAvailable = Get-Command choco -ErrorAction SilentlyContinue
    
    if ($chocoAvailable) {
        Write-Host "📦 Instalando Redis con Chocolatey..." -ForegroundColor Yellow
        choco install redis-64 -y
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✅ Redis instalado exitosamente con Chocolatey" -ForegroundColor Green
            Write-Host "🔄 Iniciando servicio Redis..." -ForegroundColor Yellow
            Start-Service redis
            Write-Host "🌐 Redis disponible en: localhost:6379" -ForegroundColor Cyan
        } else {
            Write-Host "❌ Error al instalar Redis con Chocolatey" -ForegroundColor Red
        }
    } else {
        Write-Host "❌ Ni Docker ni Chocolatey están disponibles" -ForegroundColor Red
        Write-Host "📋 Instalación manual requerida:" -ForegroundColor Yellow
        Write-Host "   1. Instalar Docker Desktop: https://www.docker.com/products/docker-desktop" -ForegroundColor White
        Write-Host "   2. O instalar Chocolatey: https://chocolatey.org/install" -ForegroundColor White
        Write-Host "   3. O descargar Redis directamente: https://redis.io/download" -ForegroundColor White
    }
}

Write-Host ""
Write-Host "🔧 Próximos pasos:" -ForegroundColor Cyan
Write-Host "   1. Verificar Redis: redis-cli ping" -ForegroundColor White
Write-Host "   2. Ejecutar índices MongoDB: mongosh y load('mongodb_indexes.js')" -ForegroundColor White
Write-Host "   3. Compilar proyecto: dotnet build LuxuryHub.sln" -ForegroundColor White
Write-Host "   4. Ejecutar API: dotnet run --project LuxuryHub.API" -ForegroundColor White

Write-Host ""
Write-Host "📚 Documentación completa: PERFORMANCE_OPTIMIZATION.md" -ForegroundColor Cyan
