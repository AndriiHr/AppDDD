using App.Domain.SeedWork;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using AppContext = App.Infrastructure.Database.AppContext;

namespace App.Infrastructure.Processing
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly AppContext _context;

        public DomainEventsDispatcher(IMediator mediator, AppContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = this._context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
             .SelectMany(x => x.Entity.DomainEvents)
             .ToList();

            domainEntities
          .ForEach(entity => entity.Entity.ClearDomainEvent());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _mediator.Publish(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
