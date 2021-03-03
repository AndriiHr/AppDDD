using App.Domain.SeedWork;

namespace App.Domain.Projects
{
    public class ProjectCreatedEvent: DomainEventBase
    {
        public string ProjectName { get; }

        public ProjectCreatedEvent(string projectName)
        {
            ProjectName = projectName;
        }
    }
}