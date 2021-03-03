using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Queries;
using App.Domain.IRepositories;
using App.Domain.Projects;
using AutoMapper;

namespace App.Application.Projects.Queries
{
    public class GetProjectQueryHandler : IQueryHandler<GetProjectQuery, ProjectDto>
    {
        private readonly IRepository<Project> _repository;
        private readonly IMapper _mapper;

        public GetProjectQueryHandler(IRepository<Project> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetSingle(x => x.Id == request.Id, x => x.Feedbacks);
            var projectDto = _mapper.Map<ProjectDto>(project);

            return projectDto;
        }
    }
}