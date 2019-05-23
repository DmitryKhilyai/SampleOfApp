using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        public ApplicationContext()
        { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=EPBYMINW5668\\SQLEXPRESS;Database=Travix;Trusted_Connection=True;");
        }
    }
}
