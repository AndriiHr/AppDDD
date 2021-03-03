using MediatR;
using System;

namespace App.Domain.SeedWork
{
    public interface IDomainEvent: INotification
    {
        DateTime OccurredOn { get; }
    }
}
