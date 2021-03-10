using App.Application.Configuration.Commands;

namespace App.Application.Projects.Commands
{
    public class CreateProjectCommand : CommandBase
    {
        public ProjectDto Project { get; }

        public CreateProjectCommand(ProjectDto project)
        {
            Project = project;
        }
    }
}