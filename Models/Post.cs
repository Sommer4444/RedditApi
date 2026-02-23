namespace RedditApi.Models;

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    
    // Relation til forfatteren
    public int UserId { get; set; }
    public User User { get; set; }
    
    // Relation til kommentarer
    public List<Comment> Comments { get; set; } = new();
}