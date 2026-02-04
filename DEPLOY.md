# Quick Deployment Guide - Render

## Step 1: Prerequisites
- GitHub account
- Render account (free tier)
- Code pushed to GitHub

## Step 2: Deploy on Render

1. Go to [render.com](https://render.com)
2. Click "New +" → "Web Service"
3. Connect your GitHub account
4. Select `ClusterConnect.NET` repository
5. Configure:
   - **Name:** `clusterconnect-api`
   - **Environment:** `.NET`
   - **Build Command:** `dotnet publish -c Release -o out`
   - **Start Command:** `dotnet out/ClusterConnect.dll`

## Step 3: Add PostgreSQL Database

1. In Render dashboard, click "New +" → "PostgreSQL"
2. Name it `clusterconnect-db`
3. Select Free tier
4. Copy the **Internal Database URL**

## Step 4: Set Environment Variables

In your web service → Environment:

```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:8080
ConnectionStrings__DefaultConnection=<paste-internal-database-url>
Jwt__Key=YourProductionSecretKey12345678901234567890CHANGETHIS
```

## Step 5: Auto-run Migrations

Add this to `Program.cs` before `app.Run()`:

```csharp
// Auto-migrate database on startup (Render deployment)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
```

## Step 6: Deploy!

Click "Manual Deploy" → "Deploy latest commit"

Render will:
- Build your .NET app
- Run migrations automatically
- Deploy to live URL

## Your Live URL

```
https://clusterconnect-api.onrender.com
```

Test it:
```bash
curl https://clusterconnect-api.onrender.com/api/projects
```

View Swagger docs:
```
https://clusterconnect-api.onrender.com/swagger
```

---

**Total Time:** 5-10 minutes  
**Cost:** $0 (Render free tier)

**Note:** Free tier spins down after inactivity. First request may take 30s to wake up.
