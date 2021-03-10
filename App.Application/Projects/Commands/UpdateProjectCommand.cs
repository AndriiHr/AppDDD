using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;

namespace App.Application.Projects.Commands
{
    public class UpdateProjectCommand : CommandBase
    {
        public ProjectDto Project { get; }

        public UpdateProjectCommand(ProjectDto project)
        {
            Project = project;
        }
    }
}