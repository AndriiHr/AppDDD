using App.Application.Configuration.Commands;
using App.Application.Configuration.Queries;

namespace App.Application.Projects.Queries
{
    public class GetProjectQuery : IQuery<ProjectDto>
    {
        public int Id { get; }

        public GetProjectQuery(int id)
        {
            Id = id;
        }
    }
}
