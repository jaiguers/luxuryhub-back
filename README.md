# LuxuryHub Real Estate API

Una API REST moderna para la gestión de propiedades inmobiliarias construida con .NET 9 y MongoDB, siguiendo los principios de Arquitectura Hexagonal (Clean Architecture).

## 🏗️ Arquitectura

El proyecto sigue los principios de **Arquitectura Hexagonal** (Clean Architecture) con las siguientes capas:

- **Domain**: Entidades, interfaces y lógica de negocio central
- **Application**: Casos de uso, servicios y DTOs
- **Infrastructure**: Implementación de repositorios y acceso a datos
- **API**: Controladores y configuración de la aplicación

## 🚀 Características

- ✅ **Filtros avanzados**: Por nombre, dirección y rango de precios
- ✅ **Paginación**: Soporte completo para paginación de resultados
- ✅ **Optimización MongoDB**: Consultas optimizadas con índices
- ✅ **Validación**: Validación robusta con FluentValidation
- ✅ **Mapeo automático**: AutoMapper para conversión de entidades
- ✅ **Manejo de errores**: Middleware centralizado para excepciones
- ✅ **Documentación**: Swagger/OpenAPI integrado
- ✅ **Pruebas unitarias**: Cobertura completa con xUnit y Moq
- ✅ **Logging**: Logging estructurado con Serilog

## 📋 Esquema de Base de Datos

### Owner
```json
{
  "id": "ObjectId",
  "name": "string",
  "address": "string", 
  "photo": "string",
  "birthday": "DateTime",
  "createdAt": "DateTime",
  "updatedAt": "DateTime"
}
```

### Property
```json
{
  "id": "ObjectId",
  "name": "string",
  "address": "string",
  "price": "decimal",
  "codeInternal": "string",
  "year": "int",
  "idOwner": "ObjectId (ref: Owner.id)",
  "createdAt": "DateTime",
  "updatedAt": "DateTime"
}
```

### PropertyImage
```json
{
  "id": "ObjectId",
  "idProperty": "ObjectId (ref: Property.id)",
  "file": "string",
  "enabled": "boolean",
  "createdAt": "DateTime",
  "updatedAt": "DateTime"
}
```

### PropertyTrace
```json
{
  "id": "ObjectId",
  "dateSale": "DateTime",
  "name": "string",
  "value": "decimal",
  "tax": "decimal",
  "idProperty": "ObjectId (ref: Property.id)",
  "createdAt": "DateTime",
  "updatedAt": "DateTime"
}
```

## 🛠️ Tecnologías

- **.NET 9**: Framework de desarrollo
- **MongoDB**: Base de datos NoSQL
- **AutoMapper**: Mapeo de objetos
- **FluentValidation**: Validación de datos
- **Swagger/OpenAPI**: Documentación de API
- **xUnit**: Framework de pruebas
- **Moq**: Mocking para pruebas
- **FluentAssertions**: Assertions más legibles

## 📦 Instalación

### Prerrequisitos

- .NET 9 SDK
- MongoDB (local o en la nube)
- Visual Studio 2022 o VS Code

### Configuración

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/tu-usuario/luxuryhub-back.git
   cd luxuryhub-back
   ```

2. **Configurar MongoDB**
   - Instalar MongoDB localmente o usar MongoDB Atlas
   - Actualizar la cadena de conexión en `appsettings.json`

3. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

4. **Ejecutar migraciones** (si es necesario)
   ```bash
   # Crear índices en MongoDB para optimizar consultas
   ```

5. **Ejecutar la aplicación**
   ```bash
   dotnet run --project LuxuryHub.API
   ```

## 🔧 Configuración

### appsettings.json

```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017/RealEstateDB"
  },
  "MongoDbSettings": {
    "DatabaseName": "RealEstateDB",
    "Collections": {
      "Owners": "owners",
      "Properties": "properties", 
      "PropertyImages": "propertyImages",
      "PropertyTraces": "propertyTraces"
    }
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## 📚 Endpoints de la API

### Propiedades

#### GET /api/properties
Obtiene todas las propiedades con filtros opcionales y paginación.

**Parámetros de consulta:**
- `pageNumber` (int): Número de página (default: 1)
- `pageSize` (int): Tamaño de página (default: 10, max: 100)
- `name` (string): Filtro por nombre (coincidencia parcial)
- `address` (string): Filtro por dirección (coincidencia parcial)
- `minPrice` (decimal): Precio mínimo
- `maxPrice` (decimal): Precio máximo

**Ejemplo:**
```bash
GET /api/properties?pageNumber=1&pageSize=10&name=villa&minPrice=100000&maxPrice=500000
```

#### GET /api/properties/{id}
Obtiene una propiedad específica por ID.

#### GET /api/properties/{propertyId}/images
Obtiene todas las imágenes de una propiedad.

#### GET /api/properties/{propertyId}/traces
Obtiene el historial de transacciones de una propiedad.

## 🧪 Pruebas

### Ejecutar pruebas unitarias
```bash
dotnet test
```

### Ejecutar pruebas con cobertura
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## 🚀 Optimizaciones de Rendimiento

### Índices MongoDB Recomendados

```javascript
// Índice para búsquedas por nombre
db.properties.createIndex({ "name": "text" })

// Índice para búsquedas por dirección
db.properties.createIndex({ "address": "text" })

// Índice compuesto para filtros de precio
db.properties.createIndex({ "price": 1, "createdAt": -1 })

// Índice para búsquedas por propietario
db.properties.createIndex({ "idOwner": 1 })

// Índice para imágenes de propiedades
db.propertyImages.createIndex({ "idProperty": 1, "enabled": 1 })

// Índice para trazas de propiedades
db.propertyTraces.createIndex({ "idProperty": 1, "dateSale": -1 })
```

### Estrategias de Optimización

1. **Paginación**: Implementada en todas las consultas
2. **Filtros eficientes**: Uso de índices apropiados
3. **Agregaciones optimizadas**: Para joins con propietarios
4. **Caching**: Preparado para implementar Redis
5. **Compresión**: Respuestas comprimidas automáticamente

## 📊 Estructura del Proyecto

```
LuxuryHub/
├── LuxuryHub.API/                 # Capa de presentación
│   ├── Controllers/               # Controladores REST
│   ├── Middleware/                # Middleware personalizado
│   ├── Extensions/                # Extensiones de configuración
│   └── Program.cs                 # Punto de entrada
├── LuxuryHub.Domain/              # Capa de dominio
│   ├── Entities/                  # Entidades del dominio
│   ├── Interfaces/                # Interfaces de repositorios
│   └── Exceptions/                # Excepciones del dominio
├── LuxuryHub.Application/         # Capa de aplicación
│   ├── DTOs/                      # Objetos de transferencia
│   ├── Services/                  # Servicios de aplicación
│   ├── Interfaces/                # Interfaces de servicios
│   ├── Requests/                  # Objetos de request
│   ├── Validators/                # Validadores
│   └── Mappings/                  # Configuración de AutoMapper
├── LuxuryHub.Infrastructure/      # Capa de infraestructura
│   ├── Data/                      # Contexto de datos
│   └── Repositories/              # Implementación de repositorios
└── LuxuryHub.Tests/               # Pruebas unitarias
    └── Application/               # Pruebas de servicios
```

## 🔒 Seguridad

- Validación de entrada en todos los endpoints
- Manejo seguro de excepciones
- Logging estructurado sin información sensible
- Preparado para autenticación JWT

## 📈 Monitoreo

- Logging estructurado con Serilog
- Métricas de rendimiento
- Health checks para MongoDB
- Preparado para APM (Application Performance Monitoring)

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para detalles.

## 🆘 Soporte

Para soporte técnico o preguntas, por favor contacta:
- Email: soporte@luxuryhub.com
- Issues: [GitHub Issues](https://github.com/tu-usuario/luxuryhub-back/issues)

---

**Desarrollado con ❤️ usando .NET 9 y Arquitectura Hexagonal**
