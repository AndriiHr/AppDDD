using App.Application.Configuration.Commands;

namespace App.Application.Projects.Commands
{
    public class AssignUserToProjectCommand : CommandBase
    {
        public int ProjectId { get; }
        public int UserId { get; }

        public AssignUserToProjectCommand(int projectId, int userId)
        {
            ProjectId = projectId;
            UserId = userId;
        }
    }
}