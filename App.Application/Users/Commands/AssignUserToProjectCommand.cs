using App.Application.Configuration.Commands;

namespace App.Application.Users.Commands
{
    public class AssignUserToProjectCommand : CommandBase
    {
        public int UserId { get; }
        public int ProjectId { get; }

        public AssignUserToProjectCommand(int userId, int projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }
    }
}
