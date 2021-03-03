using App.Domain.SeedWork;

namespace App.Domain.Users
{
    public class AssignProjectEvent : DomainEventBase
    {
        public int UserId { get; }
        public int ProjectId { get; }

        public AssignProjectEvent( int userId, int projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }
    }
}