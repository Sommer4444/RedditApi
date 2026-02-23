using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using RedditApi.Models;
using RedditApi.Data;

var builder = WebApplication.CreateBuilder(args);

// --- 1. REGISTRER SERVICES (Altid FØR builder.Build()) ---

var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, policy => {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Tilføj DbContext
builder.Services.AddDbContext<RedditContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// --- 2. BYG APPEN (Låser for flere services) ---
var app = builder.Build();

// --- 3. MIDDLEWARE PIPELINE (Konfiguration af hvordan appen kører) ---

// VIGTIGT: Aktivér CORS policy
app.UseCors(AllowSomeStuff);

// Midlertidige data til GET endpointet
var users = new List<User>
{
    new User { UserId = 1, Username = "KodningsHej" },
    new User { UserId = 2, Username = "DotNetFan" }
};

var posts = new List<Post>
{
    new Post 
    { 
        PostId = 1, 
        Title = "Min første API", 
        Content = "Det her kører bare!", 
        Upvote = 10, 
        UserId = 1,
        User = users[0]
    }
};

// --- 4. ENDPOINTS ---

app.MapGet("/", () => "Morten er en fkn Nuddelspiser!");

app.MapGet("/api/posts", () =>
{
    return Results.Ok(posts);
});

// Post: Opret en ny post i databasen
app.MapPost("/api/posts", (RedditContext db, Post post) =>
{
    db.Posts.Add(post);
    db.SaveChanges();
    return Results.Created($"/api/posts/{post.PostId}", post);
});

app.MapPost("/api/posts/{id}/comments", (RedditContext db, int id, Comment comment) =>
{
    // Her kan Ali skrive sin kode
    return Results.Ok("Kommentar modtaget");
});

// --- 5. DATABASE INITIALISERING (Seed) ---

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RedditContext>();
    db.Database.EnsureCreated(); // Opretter databasen hvis den ikke findes
}

app.Run();