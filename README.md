# Url-Shorter - URL Shortening API

## Project Overview

**Url-Shorter** is a RESTful API built with **.NET 9** that offers efficient shortening and redirection for URLs. It supports creation, retrieval, and redirection of short URLs and includes Swagger documentation for easy API exploration.

---

## Technologies Used

- .NET 9  
- ASP.NET Core  
- Entity Framework Core  
- SQL Server  
- Swagger/OpenAPI  

---

## Key Features

- Shorten long URLs to short, shareable links  
- Redirect to original URLs from short codes  
- Retrieve all shortened URLs *(if implemented)*  
- Database migrations with EF Core  
- Configurable environments via appsettings  
- Interactive API documentation via Swagger  

---

## Prerequisites

- .NET 9 SDK  
- SQL Server or compatible database  
- Visual Studio 2022 or Visual Studio Code  

---

## Installation

### Cloning the Repository

```bash
git clone [<repository-url>](https://github.com/jerkey06/Url-Shorter)
cd Url-Shorter
```

### Restoring Dependencies

```bash
dotnet restore
```

### Applying Database Migrations

```bash
dotnet ef database update
```

### Running the Application

```bash
dotnet run
```

---

## API Endpoints

| Method | Endpoint         | Description                          |
|--------|------------------|--------------------------------------|
| POST   | `/shorten`       | Shortens a given URL                 |
| GET    | `/{shortCode}`   | Redirects to the original URL        |
| GET    | `/api/urls`      | Retrieves all shortened URLs *(opt)* |

---

## URL Model

```csharp
public class ShortUrl
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; }
    public string ShortCode { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

---

## Swagger Access

Once the application is running, navigate to:

```
https://localhost:<port>/swagger
```

to access the interactive API documentation.

---

## Project Structure

- `Program.cs` – App entry point and service configuration  
- `Models/` – Contains DTOs and URL logic  
- `Migrations/` – EF Core database schema migrations  
- `appsettings.json` – General configuration  
- `appsettings.Development.json` – Development-specific settings  
- `launchSettings.json` – Local run profiles  
- `Urlshort.csproj` – Project build configuration  

---

## API Usage Examples

### Shorten a URL

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

---

### Redirect to Original URL

```bash
curl -v "https://localhost:<port>/abc123"
```

**Response:** HTTP 302 Redirect to https://www.example.com

---

### List All Shortened URLs

```bash
curl "https://localhost:<port>/api/urls"
```

**Response:**

```json
[
  {
    "shortUrl": "https://localhost:<port>/abc123",
    "originalUrl": "https://www.example.com"
  }
]
```

---

## Future Enhancements

- Expiration dates for shortened URLs  
- User authentication and rate limiting  
- Click tracking and analytics  
- Custom alias support  
- UI for URL management  

---

## Contact

For questions or support, please contact the project maintainer.
