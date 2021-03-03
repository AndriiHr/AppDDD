using App.Domain.Feedbacks;
using App.Domain.Projects;
using App.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Database
{
    public class AppContext: DbContext
    {
        public AppContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}