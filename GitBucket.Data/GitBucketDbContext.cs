using GitBucket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace GitBucket.Data
{
    public class GitBucketDbContext : IdentityDbContext
    {
        public GitBucketDbContext(DbContextOptions<GitBucketDbContext> options) :base(options) { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Commit> Commits { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<PullRequest> PullRequests { get; set; }
        public DbSet<Repository> Repository { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<Comment>()
            .HasOne(e => e.User)
            .WithMany(r => r.Comments)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
            .Entity<Comment>()
            .HasOne(e => e.PullRequest)
            .WithMany(r => r.Comments)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
            .Entity<Issue>()
            .HasOne(e => e.Repository)
            .WithMany(r => r.Issues)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
            .Entity<Issue>()
            .HasOne(e => e.User)
            .WithMany(r => r.Issues)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
            .Entity<PullRequest>()
            .HasOne(e => e.User)
            .WithMany(r => r.PullRequests)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
            .Entity<PullRequest>()
            .HasOne(e => e.User)
            .WithMany(r => r.PullRequests)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
            .Entity<Repository>()
            .HasOne(e => e.User)
            .WithMany(r => r.Repositories)
            .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder); 
        }
    }
}
