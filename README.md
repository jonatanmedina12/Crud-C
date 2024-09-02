# CRUD en .NET C# con Autenticación JWT

Este proyecto es una API RESTful que implementa operaciones CRUD (Crear, Leer, Actualizar, Eliminar) utilizando .NET C# y autenticación mediante JSON Web Tokens (JWT).

## Características

- Operaciones CRUD completas
- Autenticación de usuarios mediante JWT
- Autorización basada en roles
- Swagger UI para documentación y pruebas de API
- Entity Framework Core para la persistencia de datos
- Manejo de excepciones personalizado

## Requisitos Previos

- .NET 7.0 SDK o superior
- SQL Server (o cualquier base de datos compatible con Entity Framework Core)
- Visual Studio 2022 o Visual Studio Code

## Configuración

1. Clonar el repositorio:
   ```
   git clone https://github.com/jonatanmeidna12/Project-App-.net-Crud
   ```

2. Navegar al directorio del proyecto:
   ```
   cd /Project-App-.net-Crud
   ```

3. Restaurar las dependencias:
   ```
   dotnet restore
   ```

4. Actualizar la cadena de conexión en `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=tu_servidor;Database=tu_base_de_datos;User Id=tu_usuario;Password=tu_contraseña;"
   }
   ```

5. Aplicar las migraciones para crear la base de datos:
   ```
   dotnet ef database update
   ```

## Ejecución

Para ejecutar el proyecto:

```
dotnet run
```

La API estará disponible en `https://localhost:5001` (o el puerto que hayas configurado).

## Uso de la API

### Autenticación

Para obtener un token JWT:

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "usuario",
  "password": "contraseña"
}
```

### Operaciones CRUD

Todas las operaciones CRUD requieren un token JWT válido en el encabezado de la solicitud:

```
Authorization: Bearer tu_token_jwt
```

Ejemplos de endpoints:

- `GET /api/items`: Obtener todos los items
- `GET /api/items/{id}`: Obtener un item específico
- `POST /api/items`: Crear un nuevo item
- `PUT /api/items/{id}`: Actualizar un item existente
- `DELETE /api/items/{id}`: Eliminar un item

## Documentación de la API

La documentación completa de la API está disponible a través de Swagger UI en:

```
https://localhost:5001/swagger
```

## Seguridad

- Los tokens JWT expiran después de un tiempo definido (configurable).
- Las contraseñas se almacenan hasheadas en la base de datos.
- Se implementa HTTPS para todas las comunicaciones.

## Pruebas

Para ejecutar las pruebas unitarias:

```
dotnet test
```

## Contribuir

Las contribuciones son bienvenidas. Por favor, abre un issue para discutir cambios mayores antes de crear un pull request.

## Licencia

[MIT License](https://opensource.org/licenses/MIT)
