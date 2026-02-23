namespace RedditApi.Models;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    
    public int PostId { get; set; }
    public Post Post { get; set; }
    
    // Relation til user af kommentaren
    public int UserId { get; set; }
    public User User { get; set; }
}