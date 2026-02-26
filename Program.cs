using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using RedditApi.Models;
using RedditApi.Data;
using RedditApi.Service;

var builder = WebApplication.CreateBuilder(args);

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
    
builder.Services.AddScoped<RedditService>();

var app = builder.Build();


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

//Opret Post
app.MapPost("/api/posts", (RedditService service, Post post) =>
{
    service.CreatePost(post);
    return Results.Created($"/api/posts/{post.PostId}", post);});


//Opret Kommentar
app.MapPost("/api/posts/{id}/comments", (RedditService service, int id, Comment comment) =>
{
    var result = service.CreateComment(id, comment);
    return result == "Post not found" ? Results.NotFound(result) : Results.Created("", comment);
});

// --- 5. DATABASE INITIALISERING (Seed) ---

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RedditContext>();
    db.Database.EnsureCreated(); // Opretter databasen hvis den ikke findes
}

app.Run();