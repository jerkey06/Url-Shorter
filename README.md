# URL Shortener

Este proyecto es un servicio de acortamiento de URLs desarrollado en C# utilizando ASP.NET Core, Entity Framework Core y Redis.

## Requisitos

- .NET 6.0 o superior
- SQL Server
- Redis

## Configuración

### Base de Datos

Asegúrate de tener una instancia de SQL Server en ejecución y configura la cadena de conexión en el archivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
  }
}
