using App.Domain.SeedWork;

namespace App.Domain.Projects
{
    public class UnassignUserEvent: DomainEventBase
    {
        public int UserId { get; }
        public int ProjectId { get; }

        public UnassignUserEvent(int userId, int projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }
    }
}