# ClusterConnect .NET API

ASP.NET Core Web API for student collaboration and project management platform.

## 🛠️ Tech Stack

- **.NET 8.0** - Latest LTS framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core** - ORM for database access
- **PostgreSQL** - Primary database
- **Redis** - Caching layer
- **JWT** - Authentication
- **Swagger** - API documentation

## 📦 Project Structure

```
ClusterConnect.NET/
├── Controllers/
│   └── ProjectsController.cs    # REST API endpoints
├── Models/
│   └── Project.cs                # Data models
├── Data/
│   └── ApplicationDbContext.cs   # EF Core context
├── Services/
│   ├── ICacheService.cs          # Cache interface
│   └── RedisCacheService.cs      # Redis implementation
├── Program.cs                    # Application entry point
├── appsettings.json              # Configuration
├── ClusterConnect.csproj         # Project file
└── Dockerfile                    # Docker deployment
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL 14+
- Redis (optional, for caching)

### Installation

1. **Clone the repository**
```bash
git clone <your-repo-url>
cd ClusterConnect.NET
```

2. **Update connection strings**

Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=clusterconnect;Username=YOUR_USER;Password=YOUR_PASSWORD",
    "RedisConnection": "localhost:6379"
  }
}
```

3. **Create database**
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. **Run the application**
```bash
dotnet run
```

The API will be available at `https://localhost:7001` and `http://localhost:5001`

### Using Docker

```bash
docker build -t clusterconnect-api .
docker run -p 8080:8080 clusterconnect-api
```

## 📡 API Endpoints

### Projects

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/projects` | Get all projects |
| GET | `/api/projects/{id}` | Get project by ID |
| GET | `/api/projects/status/{status}` | Get projects by status |
| POST | `/api/projects` | Create new project |
| PUT | `/api/projects/{id}` | Update project |
| DELETE | `/api/projects/{id}` | Delete project |

### Example Request (Create Project)

```bash
curl -X POST https://localhost:7001/api/projects \
  -H "Content-Type: application/json" \
  -d '{
    "title": "AI Chatbot",
    "description": "Building an AI-powered chatbot using NLP",
    "status": "ACTIVE",
    "techStack": "Python,FastAPI,OpenAI", "teamSize": 4,
    "category": "AI/ML",
    "isPublic": true
  }'
```

### Example Response

```json
{
  "id": 1,
  "title": "AI Chatbot",
  "description": "Building an AI-powered chatbot using NLP",
  "status": "ACTIVE",
  "createdAt": "2026-02-04T14:30:00Z",
  "updatedAt": null,
  "techStack": "Python,FastAPI,OpenAI",
  "teamSize": 4,
  "category": "AI/ML",
  "isPublic": true
}
```

## 🔐 Authentication

JWT authentication is configured. To use protected endpoints:

1. Obtain a JWT token (authentication endpoints to be implemented)
2. Include in requests:
```bash
curl -H "Authorization: Bearer YOUR_JWT_TOKEN" https://localhost:7001/api/projects
```

## 📊 Database Migrations

```bash
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## 🧪 Testing

Run unit tests:
```bash
dotnet test
```

## 🚢 Deployment

### Railway

1. Create new project on Railway
2. Add PostgreSQL service
3. Connect GitHub repository
4. Railway will auto-detect .NET and deploy

### Azure

```bash
az webapp up --name clusterconnect-api --resource-group MyResourceGroup
```

## 🔧 Configuration

### Environment Variables

```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=<your-postgres-url>
ConnectionStrings__RedisConnection=<your-redis-url>
Jwt__Key=<your-secret-key>
```

## 🎯 Key Features

- ✅ Full CRUD operations for projects
- ✅ PostgreSQL database with EF Core
- ✅ Redis caching for performance
- ✅ JWT authentication ready
- ✅ Swagger API documentation
- ✅ Docker deployment
- ✅ Structured logging
- ✅ Error handling
- ✅ CORS enabled

## 📚 Learning Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [C# Programming Guide](https://docs.microsoft.com/dotnet/csharp)

## 📄 License

MIT

## 👤 Author

**Sree Madhav Pelli**
- GitHub: [@MadhavDGS](https://github.com/MadhavDGS)
- LinkedIn: [sree-madhav-pelli](https://www.linkedin.com/in/sree-madhav-pelli-b2a015329/)

---

Built with ❤️ using ASP.NET Core
