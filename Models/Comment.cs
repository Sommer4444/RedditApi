namespace RedditApi.Models;

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public int Upvote { get; set; }
    public int Downvote { get; set; }
    
<<<<<<< HEAD
    //Så vi ved hvem der kommenterer
=======
    // Relation tilbage til tråden (Post)
    public int PostId { get; set; }
    public Post Post { get; set; }
    
    // Relation til user af kommentaren
    public int UserId { get; set; }
>>>>>>> e9f0732efe1b08cacd7850c57f65c09a736e7fff
    public User User { get; set; }
}