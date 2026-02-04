# C# Crash Course for Java Developers

**Goal:** Learn enough C# in 2 days to ace the Park Place interview

## Day 1: Language Basics (4 hours)

### Hour 1: Syntax Comparison

| Java | C# | Notes |
|------|-----|-------|
| `System.out.println()` | `Console.WriteLine()` | Same concept |
| `public static void main()` | `public static void Main()` | Capital M |
| `String` | `string` | Lowercase in C# |
| `int`, `double` | `int`, `double` | Same primitives |
| `List<String>` | `List<string>` | Same generics |
| `package` | `namespace` | Different keyword |
| `import` | `using` | Different keyword |

### Hour 2: Key Differences

**Properties (C# specific):**
```csharp
// Java - getters/setters
private String name;
public String getName() { return name; }
public void setName(String name) { this.name = name; }

// C# - auto-properties
public string Name { get; set; }
```

**LINQ (C# killer feature):**
```csharp
// Filter list in Java
List<Integer> numbers = Arrays.asList(1,2,3,4,5);
List<Integer> evens = numbers.stream()
    .filter(n -> n % 2 == 0)
    .collect(Collectors.toList());

// C# - much cleaner
var numbers = new List<int> { 1, 2, 3, 4, 5 };
var evens = numbers.Where(n => n % 2 == 0).ToList();
```

### Hour 3: ASP.NET Core Basics

**Controller Structure:**
```csharp
[ApiController]  // Attribute (like @RestController in Spring)
[Route("api/[controller]")]  // Route mapping
public class ProjectsController : ControllerBase
{
    [HttpGet]  // Like @GetMapping
    public ActionResult<List<Project>> GetAll()
    {
        return Ok(projects);  // Returns 200 OK
    }
    
    [HttpPost]  // Like @PostMapping
    public ActionResult<Project> Create([FromBody] Project project)
    {
        return CreatedAtAction(nameof(GetAll), project);  // 201 Created
    }
}
```

### Hour 4: Entity Framework (EF Core)

**Compare to JPA:**
```csharp
// Java JPA
@Entity
@Table(name = "projects")
public class Project {
    @Id
    @GeneratedValue
    private Long id;
    
    @Column(name = "title")
    private String title;
}

// C# EF Core
public class Project
{
    [Key]
    public int Id { get; set; }
    
    [Column("title")]
    public string Title { get; set; }
}
```

---

## Day 2: Practice & Project (4 hours)

### Hour 1: Run ClusterConnect Locally

1. Install .NET SDK 8.0
2. Run `dotnet run` in project folder
3. Open https://localhost:7001/swagger
4. Test all endpoints

### Hour 2: Understand the Code

Read through each file:
- `Program.cs` - App configuration
- `ProjectsController.cs` - REST endpoints
- `ApplicationDbContext.cs` - Database context
- `Project.cs` - Entity model

**Practice:** Explain out loud what each method does

### Hour 3: Interview Prep

**Common Questions:**

**Q: What's the difference between `var` and explicit types?**
> "var is type inference. The compiler determines type at compile-time. It's not dynamic typing - once assigned, type is fixed."

**Q: What is `async/await`?**
> "It's C#'s way of handling asynchronous operations without blocking threads. Similar to promises in JavaScript or CompletableFuture in Java."

**Q: What's dependency injection in .NET?**
> "Built into ASP.NET Core. I register services in Program.cs using `builder.Services.AddScoped()` and inject via constructor."

**Q: How do you handle errors in Web API?**
> "Try-catch blocks, return appropriate status codes (404, 500), use middleware for global error handling."

### Hour 4: Mock Interview Practice

**Run through this flow:**

1. Interviewer: "Walk me through your ClusterConnect project"
   
> "I built a REST API using ASP.NET Core with Entity Framework for PostgreSQL. It has full CRUD operations for project management, uses Redis for caching, and supports JWT authentication. I deployed it on Railway."

2. Interviewer: "How did you implement caching?"

> "I created an ICacheService interface and RedisCacheService implementation. In my controller, I check cache first, if miss, query database and cache the result with a TTL of 5-10 minutes."

3. Interviewer: "What's the difference between IEnumerable and IList?"

> "IEnumerable is read-only forward iteration, IList supports indexing and modifications. IEnumerable is better for LINQ queries on large datasets since it's lazy evaluated."

---

## Quick Reference

### Most Used Methods

```csharp
// List operations
var list = new List<int> { 1, 2, 3 };
list.Add(4);
list.Remove(2);
var first = list.First();
var filtered = list.Where(x => x > 2).ToList();

// LINQ queries
var result = dbContext.Projects
    .Where(p => p.Status == "ACTIVE")
    .OrderByDescending(p => p.CreatedAt)
    .ToListAsync();  // Async DB call

// Return types in controllers
return Ok(data);           // 200
return Created();          // 201
return NoContent();        // 204
return BadRequest();       // 400
return NotFound();         // 404
return StatusCode(500);    // 500
```

### Interview Buzzwords

- "Dependency Injection" - built-in DI container
- "Middleware Pipeline" - request/response processing
- "Entity Framework Core" - ORM for .NET
- "async/await" - asynchronous programming
- "LINQ" - Language Integrated Query
- "Action Results" - controller return types

---

## Resources (If you have extra time)

1. **Microsoft Learn** (Free):
   - https://learn.microsoft.com/dotnet/csharp/
   - Do "C# for Java developers" path

2. **YouTube** (Quick):
   - "C# in 100 Seconds" - Fireship
   - "ASP.NET Core Crash Course" - FreeCodeCamp

3. **Practice**:
   - Modify ClusterConnect - add a new endpoint
   - Write a simple console app in C#

---

## Pre-Interview Checklist

✅ Can explain what ASP.NET Core is  
✅ Can describe Entity Framework  
✅ Know difference between `string` and `String`  
✅ Can write a basic controller method  
✅ Understand `async/await`  
✅ Know what LINQ is  
✅ Can explain dependency injection  
✅ Ran ClusterConnect locally  
✅ Can explain each file's purpose  

**You don't need to be an expert. Show you can learn fast.**

---

**Total Study Time:** 8 hours over 2 days  
**Readiness Level:** Interview-ready for intern position

