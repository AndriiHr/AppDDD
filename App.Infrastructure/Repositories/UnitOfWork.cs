using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using App.Domain.SeedWork;
using App.Infrastructure.Database;
using App.Infrastructure.Processing;

namespace App.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppContext _context;
        private readonly IDomainEventsDispatcher _dispatcher;

        public UnitOfWork(AppContext context, IDomainEventsDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            await _dispatcher.DispatchEventsAsync();

            return result;
        }
    }
}