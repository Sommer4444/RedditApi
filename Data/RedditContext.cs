using Microsoft.EntityFrameworkCore;
using RedditApi.Models;

namespace RedditApi.Data;

public class RedditContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
    
    public RedditContext(DbContextOptions<RedditContext> options)
        : base(options)
    {
        
    }
}