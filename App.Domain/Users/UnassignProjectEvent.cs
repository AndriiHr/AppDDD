using App.Domain.SeedWork;

namespace App.Domain.Users
{
    public class UnassignProjectEvent : DomainEventBase
    {
        public int UserId { get; }
        public int ProjectId { get; }

        public UnassignProjectEvent(int userId, int projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }
    }
}