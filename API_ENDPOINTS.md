# LuxuryHub API - Endpoints POST

Esta documentación describe los endpoints POST disponibles para crear las diferentes entidades del sistema.

## 🔗 Base URL
```
http://localhost:5000/api
```

## 📋 Endpoints Disponibles

### 1. **POST /api/owners** - Crear Propietario

Crea un nuevo propietario en el sistema.

**Request Body:**
```json
{
  "name": "Juan Pérez",
  "address": "Calle Principal 123, Ciudad",
  "photo": "https://example.com/photos/juan-perez.jpg",
  "birthday": "1985-03-15T00:00:00Z"
}
```

**Response (201 Created):**
```json
{
  "id": "507f1f77bcf86cd799439011",
  "name": "Juan Pérez",
  "address": "Calle Principal 123, Ciudad",
  "photo": "https://example.com/photos/juan-perez.jpg",
  "birthday": "1985-03-15T00:00:00Z",
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

### 2. **POST /api/properties** - Crear Propiedad

Crea una nueva propiedad inmobiliaria.

**Request Body:**
```json
{
  "name": "Villa de Lujo en la Playa",
  "address": "Avenida del Mar 456, Costa Azul",
  "price": 750000.00,
  "codeInternal": "VL-2024-001",
  "year": 2020,
  "idOwner": "507f1f77bcf86cd799439011"
}
```

**Response (201 Created):**
```json
{
  "id": "507f1f77bcf86cd799439012",
  "name": "Villa de Lujo en la Playa",
  "address": "Avenida del Mar 456, Costa Azul",
  "price": 750000.00,
  "codeInternal": "VL-2024-001",
  "year": 2020,
  "idOwner": "507f1f77bcf86cd799439011",
  "owner": {
    "id": "507f1f77bcf86cd799439011",
    "name": "Juan Pérez",
    "address": "Calle Principal 123, Ciudad",
    "photo": "https://example.com/photos/juan-perez.jpg",
    "birthday": "1985-03-15T00:00:00Z"
  },
  "createdAt": "2024-01-15T10:30:00Z",
  "updatedAt": "2024-01-15T10:30:00Z"
}
```

### 3. **POST /api/propertyimages** - Crear Imagen de Propiedad

Crea una nueva imagen asociada a una propiedad.

**Request Body:**
```json
{
  "idProperty": "507f1f77bcf86cd799439012",
  "file": "https://example.com/images/villa-exterior.jpg",
  "enabled": true
}
```

**Response (201 Created):**
```json
{
  "id": "507f1f77bcf86cd799439013",
  "idProperty": "507f1f77bcf86cd799439012",
  "file": "https://example.com/images/villa-exterior.jpg",
  "enabled": true,
  "createdAt": "2024-01-15T10:30:00Z"
}
```

### 4. **POST /api/propertytraces** - Crear Traza de Propiedad

Crea un nuevo registro de transacción/historial de una propiedad.

**Request Body:**
```json
{
  "dateSale": "2024-01-10T14:30:00Z",
  "name": "Venta a María González",
  "value": 750000.00,
  "tax": 37500.00,
  "idProperty": "507f1f77bcf86cd799439012"
}
```

**Response (201 Created):**
```json
{
  "id": "507f1f77bcf86cd799439014",
  "dateSale": "2024-01-10T14:30:00Z",
  "name": "Venta a María González",
  "value": 750000.00,
  "tax": 37500.00,
  "idProperty": "507f1f77bcf86cd799439012",
  "createdAt": "2024-01-15T10:30:00Z"
}
```

## 🔍 Endpoints GET Adicionales

### Propietarios
- `GET /api/owners` - Listar propietarios con paginación
- `GET /api/owners/{id}` - Obtener propietario por ID

### Propiedades
- `GET /api/properties` - Listar propiedades con filtros y paginación
- `GET /api/properties/{id}` - Obtener propiedad por ID
- `GET /api/properties/{id}/images` - Obtener imágenes de una propiedad
- `GET /api/properties/{id}/traces` - Obtener trazas de una propiedad

### Imágenes de Propiedades
- `GET /api/propertyimages` - Listar imágenes con paginación
- `GET /api/propertyimages/{id}` - Obtener imagen por ID

### Trazas de Propiedades
- `GET /api/propertytraces` - Listar trazas con paginación
- `GET /api/propertytraces/{id}` - Obtener traza por ID

## ⚠️ Validaciones

Todos los endpoints incluyen validaciones automáticas:

### Propietario
- **name**: Requerido, máximo 100 caracteres
- **address**: Requerido, máximo 200 caracteres
- **photo**: Requerido, máximo 500 caracteres
- **birthday**: Requerido, debe ser en el pasado

### Propiedad
- **name**: Requerido, máximo 100 caracteres
- **address**: Requerido, máximo 200 caracteres
- **price**: Requerido, mayor o igual a 0
- **codeInternal**: Requerido, máximo 50 caracteres
- **year**: Requerido, entre 1900 y 2100
- **idOwner**: Requerido, debe existir en la base de datos

### Imagen de Propiedad
- **idProperty**: Requerido, debe existir en la base de datos
- **file**: Requerido, máximo 500 caracteres
- **enabled**: Opcional, por defecto true

### Traza de Propiedad
- **dateSale**: Requerido, no puede ser en el futuro
- **name**: Requerido, máximo 100 caracteres
- **value**: Requerido, mayor o igual a 0
- **tax**: Requerido, mayor o igual a 0
- **idProperty**: Requerido, debe existir en la base de datos

## 🚀 Ejemplo de Flujo Completo

1. **Crear Propietario:**
   ```bash
   curl -X POST http://localhost:5000/api/owners \
     -H "Content-Type: application/json" \
     -d '{
       "name": "Juan Pérez",
       "address": "Calle Principal 123, Ciudad",
       "photo": "https://example.com/photos/juan-perez.jpg",
       "birthday": "1985-03-15T00:00:00Z"
     }'
   ```

2. **Crear Propiedad:**
   ```bash
   curl -X POST http://localhost:5000/api/properties \
     -H "Content-Type: application/json" \
     -d '{
       "name": "Villa de Lujo en la Playa",
       "address": "Avenida del Mar 456, Costa Azul",
       "price": 750000.00,
       "codeInternal": "VL-2024-001",
       "year": 2020,
       "idOwner": "507f1f77bcf86cd799439011"
     }'
   ```

3. **Crear Imagen:**
   ```bash
   curl -X POST http://localhost:5000/api/propertyimages \
     -H "Content-Type: application/json" \
     -d '{
       "idProperty": "507f1f77bcf86cd799439012",
       "file": "https://example.com/images/villa-exterior.jpg",
       "enabled": true
     }'
   ```

4. **Crear Traza:**
   ```bash
   curl -X POST http://localhost:5000/api/propertytraces \
     -H "Content-Type: application/json" \
     -d '{
       "dateSale": "2024-01-10T14:30:00Z",
       "name": "Venta a María González",
       "value": 750000.00,
       "tax": 37500.00,
       "idProperty": "507f1f77bcf86cd799439012"
     }'
   ```

## 📝 Códigos de Respuesta

- **201 Created**: Recurso creado exitosamente
- **400 Bad Request**: Datos de entrada inválidos
- **404 Not Found**: Recurso no encontrado
- **500 Internal Server Error**: Error interno del servidor

## 🔧 Configuración

Asegúrate de que MongoDB esté ejecutándose y la conexión esté configurada correctamente en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017/RealEstateDB"
  }
}
```
