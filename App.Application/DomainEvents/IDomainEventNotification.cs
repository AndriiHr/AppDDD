using MediatR;

namespace App.Application.DomainEvents
{
    public interface IDomainEventNotification : INotification
    {
        int Id { get; }
    }
}