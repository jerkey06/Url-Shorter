# Url-Shorter

## Overview
Url-Shorter is a web application built with ASP.NET Core 9.0 that provides URL shortening services. The project leverages Entity Framework Core for data persistence and includes API documentation via Swagger.

## Features
- Shorten long URLs to compact, shareable links
- Retrieve original URLs from shortened codes
- API endpoints for URL management
- Database migrations managed with Entity Framework Core
- Environment-based configuration support
- Integrated Swagger UI for API exploration

## Project Structure
- `Program.cs`: Application entry point and configuration
- `Models/`: Contains data transfer objects (DTOs) and URL management logic
- `Migrations/`: Entity Framework Core migration files for database schema management
- `Properties/launchSettings.json`: Local development launch profiles
- `appsettings.json` & `appsettings.Development.json`: Application configuration files
- `Urlshort.csproj`: Project file with dependencies and build configuration

## Getting Started
### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server (or compatible database, as configured in `appsettings.json`)

### Setup
1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd Url-Shorter
   ```
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the application:
   ```sh
   dotnet run
   ```
5. Access the Swagger UI at `https://localhost:<port>/swagger` for API documentation and testing.

## API Endpoints
- **POST /shorten**: Shorten a given URL
- **GET /{shortCode}**: Redirect to the original URL
- **GET /api/urls**: List all shortened URLs (if implemented)

Refer to the Swagger UI for detailed request/response schemas.

## API Usage Examples

### Shorten a URL
**Request:**
```bash
curl -X POST "https://localhost:<port>/shorten" -H "Content-Type: application/json" -d '{
  "originalUrl": "https://www.example.com"
}'
```
**Response:**
```json
{
  "shortUrl": "https://localhost:<port>/abc123",
  "originalUrl": "https://www.example.com"
}
```

### Redirect to Original URL
**Request:**
```bash
curl -v "https://localhost:<port>/abc123"
```
**Response:**
HTTP 302 Redirect to https://www.example.com

### List All Shortened URLs (if implemented)
**Request:**
```bash
curl "https://localhost:<port>/api/urls"
```
**Response:**
```json
[
  {
    "shortUrl": "https://localhost:<port>/abc123",
    "originalUrl": "https://www.example.com"
  },
  // ...more entries
]
```

## Configuration
- Update `appsettings.json` and `appsettings.Development.json` for database connection strings and environment-specific settings.

## Development
- Use the `Migrations/` folder to manage database schema changes with Entity Framework Core.
- DTOs and business logic are located in the `Models/` directory.

## Contact
For questions or support, please contact the project maintainer.

