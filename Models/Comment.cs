namespace RedditApi.Models;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
}