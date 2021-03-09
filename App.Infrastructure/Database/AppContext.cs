using App.Domain.Feedbacks;
using App.Domain.Projects;
using App.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace App.Infrastructure.Database
{
    public class AppContext : DbContext, IAppContext
    {
        public AppContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }


        private IDbContextTransaction _transaction;

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
            Database.BeginTransactionAsync();
        }

        public void Commit()
        {
            try
            {
                SaveChangesAsync();
                _transaction.CommitAsync();
            }
            finally
            {
                _transaction.DisposeAsync();
            }
        }

        public void Rollback()
        {
            _transaction.RollbackAsync();
            _transaction.DisposeAsync();
        }
    }
}