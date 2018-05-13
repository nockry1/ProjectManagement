using System.Data.Entity;

namespace GanttApplication.Models
{
    public class GanttContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<User> Users { get; set; }
    }
}