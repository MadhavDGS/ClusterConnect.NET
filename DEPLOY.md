# Deploy to Render - Complete Guide

## Option 1: Using Render Dashboard (Easiest - 5 min)

1. **Go to render.com and login**

2. **Create Web Service:**
   - Click "New +" → "Web Service"
   - Connect GitHub → Select `ClusterConnect.NET`
   - **Name:** `clusterconnect-api`
   - **Environment:** Docker (it will detect render.yaml)
   - Click "Create Web Service"

3. **Render will auto-detect `render.yaml` and:**
   - Create PostgreSQL database
   - Set environment variables
   - Build and deploy your app

4. **Done!** Your API will be live at:
   ```
   https://clusterconnect-api.onrender.com
   ```

---

## Option 2: Using Render CLI (If you want)

### Step 1: Login
```bash
render login
```
This opens browser for OAuth login.

### Step 2: The `render.yaml` is already in your repo
It will auto-deploy when Render detects it.

### Step 3: Manual deploy (if needed)
```bash
# List services
render services list

# Trigger deploy
render deploys create --service clusterconnect-api
```

---

## What's Deployed:

✅ ASP.NET Core Web API  
✅ PostgreSQL database (auto-created)  
✅ Environment variables (auto-configured)  
✅ Docker container  

## Test Your API:

```bash
# Health check
curl https://clusterconnect-api.onrender.com/api/projects

# View Swagger docs
open https://clusterconnect-api.onrender.com/swagger
```

---

## For Your Resume/Application:

**Live Demo:** https://clusterconnect-api.onrender.com  
**GitHub:** https://github.com/MadhavDGS/ClusterConnect.NET  
**Tech:** ASP.NET Core, C#, EF Core, PostgreSQL, Redis, Docker

---

**Recommendation:** Use Dashboard (Option 1) - it's faster and easier.
