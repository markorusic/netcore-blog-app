using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.InteropServices;

namespace Dao
{
    public class AppDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                optionsBuilder.UseInMemoryDatabase("BlogApp");
            }
            else
            {
                optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=WebStoreApp;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Rate>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rates)
                .OnDelete(DeleteBehavior.Restrict);
        }


        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Photo> PostPhotos { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Rate> Rates { get; set; }
    }
}
