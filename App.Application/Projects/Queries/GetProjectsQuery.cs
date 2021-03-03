using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Application.Configuration.Queries;

namespace App.Application.Projects.Queries
{
    public class GetProjectsQuery: IQuery<List<ProjectDto>>
    {
        public bool IsActive { get;}
        public GetProjectsQuery(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
