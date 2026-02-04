using ClusterConnect.Data;
using ClusterConnect.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Redis Configuration (Optional)
var redisConnection = builder.Configuration.GetConnectionString("RedisConnection");
if (!string.IsNullOrEmpty(redisConnection))
{
    try
    {
        builder.Services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(redisConnection + ",abortConnect=false"));
        builder.Services.AddScoped<ICacheService, RedisCacheService>();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Redis connection failed (will run without cache): {ex.Message}");
    }
}

// JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "YourSuperSecretKeyHere123456789012";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "ClusterConnect";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "ClusterConnectUsers";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline
// Enable Swagger in all environments (including Production for demo)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClusterConnect API v1");
    c.RoutePrefix = "swagger"; // Access via /swagger
});

app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable static file serving (wwwroot)
app.UseDefaultFiles(); // Serve index.html by default
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// Health check endpoint
app.MapGet("/health", () => new
{
    status = "healthy",
    service = "ClusterConnect API",
    version = "1.0.0",
    endpoints = new[]
    {
        "GET / - Frontend UI",
        "GET /api/projects",
        "GET /api/projects/{id}",
        "GET /api/projects/status/{status}",
        "POST /api/projects",
        "PUT /api/projects/{id}",
        "DELETE /api/projects/{id}",
        "GET /swagger - API Documentation"
    }
});

app.MapControllers();

app.Run();
