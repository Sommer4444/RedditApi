using RedditApi.Models;
using RedditApi.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");



//Post: Opret en ny post på siden

app.MapGet("/api/posts", (RedditContext db, Post post) =>
{
    db.Posts.Add(post);
    db.SaveChanges();
});

//Seed Data

using (var scope = app.Services.CreateScope())
{
}

app.Run();
