using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Queries;
using App.Domain.Enums;
using App.Domain.IRepositories;
using App.Domain.Projects;
using AutoMapper;

namespace App.Application.Projects.Queries
{
    public class GetProjectsQueryHandler : IQueryHandler<GetProjectsQuery, List<ProjectDto>>
    {
        private readonly IRepository<Project> _repository;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(IRepository<Project> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var recordStatus = request.IsActive ? RecordStatus.Active : RecordStatus.Inactive;

            var projects = await _repository.GetAll(x => x.RecordStatus == recordStatus);
            var projectsDto = projects.Select(x => _mapper.Map<ProjectDto>(x)).ToList();

            return projectsDto;
        }
    }
}
