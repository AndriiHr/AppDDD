using System;
using System.Threading;
using System.Threading.Tasks;
using App.Domain.Feedbacks;
using App.Domain.IRepositories;
using App.Domain.Projects;
using App.Domain.SeedWork;
using App.Domain.Users;
using App.Infrastructure.Processing;
using Microsoft.EntityFrameworkCore.Storage;
using AppContext = App.Infrastructure.Database.AppContext;

namespace App.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;
        private readonly AppContext _context;
        private readonly IDomainEventsDispatcher _dispatcher;
        public IRepository<User> UserRepository { get; set; }
        public IRepository<Project> ProjectRepository { get; set; }
        public IRepository<Feedback> FeedbackRepository { get; set; }

        public UnitOfWork(AppContext context,
            IDomainEventsDispatcher dispatcher,
            IRepository<User> userRepository,
            IRepository<Project> projectRepository,
            IRepository<Feedback> feedbackRepository
        )
        {
            _context = context;
            _dispatcher = dispatcher;
            UserRepository = userRepository;
            ProjectRepository = projectRepository;
            FeedbackRepository = feedbackRepository;
        }
        
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            await _dispatcher.DispatchEventsAsync();

            return result;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}