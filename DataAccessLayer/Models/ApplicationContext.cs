﻿using Microsoft.EntityFrameworkCore;

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
            optionsBuilder.UseSqlServer("Server=DIMA-PC\\SQLEXPRESS;Database=Travix;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(i => i.Post)
                .WithMany(c => c.Comments)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
