using System.Threading;
using System.Threading.Tasks;
using App.Domain.Feedbacks;
using App.Domain.IRepositories;
using App.Domain.Projects;
using App.Domain.Users;
using Microsoft.EntityFrameworkCore.Storage;

namespace App.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; set; }
        IRepository<Project> ProjectRepository { get; set; }
        IRepository<Feedback> FeedbackRepository { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}