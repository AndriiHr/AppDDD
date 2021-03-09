using App.Domain.Feedbacks;
using App.Domain.Projects;
using App.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Database
{
    public interface IAppContext
    {
         DbSet<User> Users { get; set; }
         DbSet<Project> Projects { get; set; }
         DbSet<Feedback> Feedbacks { get; set; }

         void BeginTransaction();
         void Commit();
         void Rollback();
    }
}