using Microsoft.EntityFrameworkCore;
using RedditApi.Data;
using RedditApi.Models;

namespace RedditApi.Service;

public class RedditService
{
    private readonly RedditContext db;

    public RedditService(RedditContext db)
    {
        this.db = db;
    }

    public List<Post> GetPosts()    //Hent alle posts

    {
        return db.Posts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .ToList();
    }
    
    public void CreatePost(Post post)   //Opret en post
    {
        db.Posts.Add(post);
        db.SaveChanges();
    }


    public string CreateComment(int postId, Comment comment)
    {
        var post = db.Posts.Include(p => p.Comments).FirstOrDefault(p => p.PostId == postId);
        if (post == null) return "Post not found";
        post.Comments.Add(comment);
        db.SaveChanges();
        return "Comment added";
    }
    
}