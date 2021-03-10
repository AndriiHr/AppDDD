using App.Domain.SeedWork;

namespace App.Domain.Projects
{
    public class AssignUserEvent : DomainEventBase
    {
        public int UserId { get; }
        public int ProjectId { get; }

        public AssignUserEvent(int userId, int projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }
    }
}