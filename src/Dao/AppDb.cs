using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Runtime.InteropServices;

namespace Dao
{
    public class AppDb : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                optionsBuilder.UseInMemoryDatabase(_configuration["Database:Name"]);
            }
            else
            {
                optionsBuilder.UseSqlServer(_configuration["Database:ConnectionString"]);
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
