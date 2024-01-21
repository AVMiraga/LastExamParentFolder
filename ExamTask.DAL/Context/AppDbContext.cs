using ExamTask.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ExamTask.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Setting>()
                .HasData(
                new { Id = 1, Key = "Brand", Value = "", IsDeleted = false },
                new { Id = 2, Key = "Linkedin", Value = "", IsDeleted = false },
                new { Id = 3, Key = "Facebook", Value = "", IsDeleted = false },
                new { Id = 4, Key = "Twitter", Value = "", IsDeleted = false });

            base.OnModelCreating(builder);
        }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
