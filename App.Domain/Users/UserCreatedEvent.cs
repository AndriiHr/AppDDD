using App.Domain.SeedWork;

namespace App.Domain.Users
{
    public class UserCreatedEvent : DomainEventBase
    {
        public string Email { get; }

        public UserCreatedEvent(string email)
        {
            Email = email;
        }
    }
}