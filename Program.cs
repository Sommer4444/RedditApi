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


app.MapGet("/", () => "Hello World!");





//Post: Opret en ny post på siden

app.MapPost("/api/posts", (RedditContext db, Post post) =>
{
    db.Posts.Add(post);
    db.SaveChanges();
});

//Seed Data

using (var scope = app.Services.CreateScope())
{
}

app.Run();
