using RedditApi.Models;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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

app.Run();
