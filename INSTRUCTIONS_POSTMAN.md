# 🚀 Instrucciones para Insertar Datos de Prueba en LuxuryHub

## 📋 Pasos a Seguir

### Paso 1: Crear Propietarios (Owners)

**Endpoint:** `POST http://localhost:5000/api/owners`

Ejecuta estos 10 requests en orden:

#### Owner 1 - Carlos Rodríguez
```json
{
  "name": "Carlos Rodríguez",
  "address": "Avenida Libertador 1234, Caracas, Venezuela",
  "photo": "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=400&h=400&fit=crop&crop=face",
  "birthday": "1975-06-15T00:00:00Z"
}
```

#### Owner 2 - María González
```json
{
  "name": "María González",
  "address": "Calle Principal 567, Bogotá, Colombia",
  "photo": "https://images.unsplash.com/photo-1494790108755-2616b612b786?w=400&h=400&fit=crop&crop=face",
  "birthday": "1982-03-22T00:00:00Z"
}
```

#### Owner 3 - Roberto Silva
```json
{
  "name": "Roberto Silva",
  "address": "Rua das Flores 890, São Paulo, Brasil",
  "photo": "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=400&h=400&fit=crop&crop=face",
  "birthday": "1968-11-08T00:00:00Z"
}
```

#### Owner 4 - Ana Martínez
```json
{
  "name": "Ana Martínez",
  "address": "Calle Mayor 234, Madrid, España",
  "photo": "https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=400&h=400&fit=crop&crop=face",
  "birthday": "1990-09-14T00:00:00Z"
}
```

#### Owner 5 - Luis Fernández
```json
{
  "name": "Luis Fernández",
  "address": "Avenida Reforma 456, Ciudad de México, México",
  "photo": "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=400&h=400&fit=crop&crop=face",
  "birthday": "1973-12-03T00:00:00Z"
}
```

#### Owner 6 - Carmen López
```json
{
  "name": "Carmen López",
  "address": "Calle San Martín 789, Buenos Aires, Argentina",
  "photo": "https://images.unsplash.com/photo-1544005313-94ddf0286df2?w=400&h=400&fit=crop&crop=face",
  "birthday": "1987-07-19T00:00:00Z"
}
```

#### Owner 7 - Diego Morales
```json
{
  "name": "Diego Morales",
  "address": "Avenida Providencia 321, Santiago, Chile",
  "photo": "https://images.unsplash.com/photo-1506794778202-cad84cf45f1d?w=400&h=400&fit=crop&crop=face",
  "birthday": "1981-04-25T00:00:00Z"
}
```

#### Owner 8 - Patricia Ruiz
```json
{
  "name": "Patricia Ruiz",
  "address": "Calle 72 654, Lima, Perú",
  "photo": "https://images.unsplash.com/photo-1534528741775-53994a69daeb?w=400&h=400&fit=crop&crop=face",
  "birthday": "1979-01-30T00:00:00Z"
}
```

#### Owner 9 - Fernando Castro
```json
{
  "name": "Fernando Castro",
  "address": "Avenida 9 de Julio 987, Montevideo, Uruguay",
  "photo": "https://images.unsplash.com/photo-1519085360753-af0119f7cbe7?w=400&h=400&fit=crop&crop=face",
  "birthday": "1965-08-12T00:00:00Z"
}
```

#### Owner 10 - Isabel Torres
```json
{
  "name": "Isabel Torres",
  "address": "Calle 15 432, Quito, Ecuador",
  "photo": "https://images.unsplash.com/photo-1487412720507-e7ab37603c6f?w=400&h=400&fit=crop&crop=face",
  "birthday": "1992-05-07T00:00:00Z"
}
```

### Paso 2: Crear Propiedades (Properties)

**Endpoint:** `POST http://localhost:5000/api/properties`

⚠️ **IMPORTANTE:** Reemplaza los `OWNER_ID_X` con los IDs reales devueltos en el Paso 1.

#### Property 1 - Villa de Lujo en la Playa
```json
{
  "name": "Villa de Lujo en la Playa",
  "address": "Avenida del Mar 123, Costa Azul, Venezuela",
  "price": 850000.00,
  "codeInternal": "VL-2024-001",
  "year": 2020,
  "idOwner": "REEMPLAZAR_CON_OWNER_ID_1"
}
```

#### Property 2 - Apartamento Premium en Zona Rosa
```json
{
  "name": "Apartamento Premium en Zona Rosa",
  "address": "Calle 85 45-67, Zona Rosa, Bogotá, Colombia",
  "price": 450000.00,
  "codeInternal": "AP-2024-002",
  "year": 2018,
  "idOwner": "REEMPLAZAR_CON_OWNER_ID_2"
}
```

#### Property 3 - Casa Colonial en Barrio Histórico
```json
{
  "name": "Casa Colonial en Barrio Histórico",
  "address": "Rua do Comércio 234, Centro Histórico, São Paulo, Brasil",
  "price": 1200000.00,
  "codeInternal": "CC-2024-003",
  "year": 2015,
  "idOwner": "REEMPLAZAR_CON_OWNER_ID_3"
}
```

#### Property 4 - Penthouse con Vista Panorámica
```json
{
  "name": "Penthouse con Vista Panorámica",
  "address": "Calle Gran Vía 567, Madrid, España",
  "price": 1800000.00,
  "codeInternal": "PH-2024-004",
  "year": 2022,
  "idOwner": "REEMPLAZAR_CON_OWNER_ID_4"
}
```

#### Property 5 - Casa Moderna en Polanco
```json
{
  "name": "Casa Moderna en Polanco",
  "address": "Avenida Presidente Masaryk 890, Polanco, Ciudad de México",
  "price": 950000.00,
  "codeInternal": "CM-2024-005",
  "year": 2019,
  "idOwner": "REEMPLAZAR_CON_OWNER_ID_5"
}
```

#### Property 6 - Loft Industrial en Palermo
```json
{
  "name": "Loft Industrial en Palermo",
  "address": "Calle Honduras 321, Palermo, Buenos Aires, Argentina",
  "price": 380000.00,
  "codeInternal": "LI-2024-006",
  "year": 2021,
  "idOwner": "REEMPLAZAR_CON_OWNER_ID_6"
}
```

#### Property 7 - Casa de Campo en Las Condes
```json
{
  "name": "Casa de Campo en Las Condes",
  "address": "Avenida Las Condes 654, Las Condes, Santiago, Chile",
  "price": 750000.00,
  "codeInternal": "CC-2024-007",
  "year": 2017,
  "idOwner": "REEMPLAZAR_CON_OWNER_ID_7"
}
```

#### Property 8 - Apartamento en Miraflores
```json
{
  "name": "Apartamento en Miraflores",
  "address": "Avenida Larco 789, Miraflores, Lima, Perú",
  "price": 420000.00,
  "codeInternal": "AM-2024-008",
  "year": 2020,
  "idOwner": "REEMPLAZAR_CON_OWNER_ID_8"
}
```

#### Property 9 - Casa Quinta en Punta Carretas
```json
{
  "name": "Casa Quinta en Punta Carretas",
  "address": "Calle Ellauri 123, Punta Carretas, Montevideo, Uruguay",
  "price": 680000.00,
  "codeInternal": "CQ-2024-009",
  "year": 2016,
  "idOwner": "REEMPLAZAR_CON_OWNER_ID_9"
}
```

#### Property 10 - Villa en Cumbayá
```json
{
  "name": "Villa en Cumbayá",
  "address": "Avenida Interoceánica 456, Cumbayá, Quito, Ecuador",
  "price": 520000.00,
  "codeInternal": "VC-2024-010",
  "year": 2021,
  "idOwner": "REEMPLAZAR_CON_OWNER_ID_10"
}
```

### Paso 3: Crear Imágenes de Propiedades (PropertyImages)

**Endpoint:** `POST http://localhost:5000/api/propertyimages`

⚠️ **IMPORTANTE:** Reemplaza los `PROPERTY_ID_X` con los IDs reales devueltos en el Paso 2.

#### Imágenes para Property 1 (3 imágenes)
```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_1",
  "file": "https://images.unsplash.com/photo-1613490493576-7fde63acd811?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_1",
  "file": "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_1",
  "file": "https://images.unsplash.com/photo-1570129477492-45c003edd2be?w=800&h=600&fit=crop",
  "enabled": true
}
```

#### Imágenes para Property 2 (2 imágenes)
```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_2",
  "file": "https://images.unsplash.com/photo-1545324418-cc1a3fa10c00?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_2",
  "file": "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688?w=800&h=600&fit=crop",
  "enabled": true
}
```

#### Imágenes para Property 3 (2 imágenes)
```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_3",
  "file": "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_3",
  "file": "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800&h=600&fit=crop",
  "enabled": true
}
```

#### Imágenes para Property 4 (3 imágenes)
```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_4",
  "file": "https://images.unsplash.com/photo-1600607687920-4e2a09cf159d?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_4",
  "file": "https://images.unsplash.com/photo-1600607687644-c7171b42498b?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_4",
  "file": "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800&h=600&fit=crop",
  "enabled": true
}
```

#### Imágenes para Property 5 (2 imágenes)
```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_5",
  "file": "https://images.unsplash.com/photo-1600607687920-4e2a09cf159d?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_5",
  "file": "https://images.unsplash.com/photo-1600607687644-c7171b42498b?w=800&h=600&fit=crop",
  "enabled": true
}
```

#### Imágenes para Property 6 (2 imágenes)
```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_6",
  "file": "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_6",
  "file": "https://images.unsplash.com/photo-1600607687920-4e2a09cf159d?w=800&h=600&fit=crop",
  "enabled": true
}
```

#### Imágenes para Property 7 (2 imágenes)
```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_7",
  "file": "https://images.unsplash.com/photo-1600607687644-c7171b42498b?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_7",
  "file": "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800&h=600&fit=crop",
  "enabled": true
}
```

#### Imágenes para Property 8 (2 imágenes)
```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_8",
  "file": "https://images.unsplash.com/photo-1600607687920-4e2a09cf159d?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_8",
  "file": "https://images.unsplash.com/photo-1600607687644-c7171b42498b?w=800&h=600&fit=crop",
  "enabled": true
}
```

#### Imágenes para Property 9 (2 imágenes)
```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_9",
  "file": "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_9",
  "file": "https://images.unsplash.com/photo-1600607687920-4e2a09cf159d?w=800&h=600&fit=crop",
  "enabled": true
}
```

#### Imágenes para Property 10 (3 imágenes)
```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_10",
  "file": "https://images.unsplash.com/photo-1600607687644-c7171b42498b?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_10",
  "file": "https://images.unsplash.com/photo-1600607687939-ce8a6c25118c?w=800&h=600&fit=crop",
  "enabled": true
}
```

```json
{
  "idProperty": "REEMPLAZAR_CON_PROPERTY_ID_10",
  "file": "https://images.unsplash.com/photo-1600607687920-4e2a09cf159d?w=800&h=600&fit=crop",
  "enabled": true
}
```

## 📊 Resumen de Datos

- **10 Propietarios** de diferentes países latinoamericanos y España
- **10 Propiedades** con diferentes tipos (villas, apartamentos, casas) y precios variados
- **25 Imágenes** distribuidas entre las propiedades (algunas tienen 2, otras 3 imágenes)

## 🔗 Relaciones

1. **Carlos Rodríguez** → **Villa de Lujo en la Playa** (3 imágenes)
2. **María González** → **Apartamento Premium en Zona Rosa** (2 imágenes)
3. **Roberto Silva** → **Casa Colonial en Barrio Histórico** (2 imágenes)
4. **Ana Martínez** → **Penthouse con Vista Panorámica** (3 imágenes)
5. **Luis Fernández** → **Casa Moderna en Polanco** (2 imágenes)
6. **Carmen López** → **Loft Industrial en Palermo** (2 imágenes)
7. **Diego Morales** → **Casa de Campo en Las Condes** (2 imágenes)
8. **Patricia Ruiz** → **Apartamento en Miraflores** (2 imágenes)
9. **Fernando Castro** → **Casa Quinta en Punta Carretas** (2 imágenes)
10. **Isabel Torres** → **Villa en Cumbayá** (3 imágenes)

## ✅ Verificación

Después de completar todos los inserts, puedes verificar los datos usando:

- `GET /api/owners` - Listar todos los propietarios
- `GET /api/properties` - Listar todas las propiedades
- `GET /api/propertyimages` - Listar todas las imágenes
- `GET /api/properties/{id}/images` - Ver imágenes de una propiedad específica
