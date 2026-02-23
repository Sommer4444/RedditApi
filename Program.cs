using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;
using RedditApi.Models;
using RedditApi.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Sætter CORS så API'en kan bruges fra andre domæner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<RedditContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

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
        User = users[0],
        Comments = new List<Comment> { 
            new Comment { CommentId = 1, Text = "Godt gået!", Upvote = 2 } 
        }
    },
    new Post 
    { 
        PostId = 2, 
        Title = "Hvorfor bruge modeller?", 
        Content = "Fordi det giver struktur!", 
        Upvote = 50, 
        UserId = 2,
        User = users[1]
    }
};

app.MapGet("/api/posts", () =>
{
    return Results.Ok(posts);
});

app.MapGet("/", () => "Morten er en fkn Nuddelspiser!");

//Post: Opret en ny post på siden
app.MapPost("/api/posts", (RedditContext db, Post post) =>
{
    db.Posts.Add(post);
    db.SaveChanges();
});

app.MapPost("/api/posts/{id}/comments", (RedditContext db, int id, Comment comment) =>
{
    //Tilføjelse af resten af koden Ali!
});



//Seed Data

using (var scope = app.Services.CreateScope())
{
}

app.Run();
