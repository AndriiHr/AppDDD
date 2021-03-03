using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;
using App.Domain.IRepositories;
using App.Domain.Projects;
using App.Domain.SeedWork;
using AutoMapper;
using MediatR;

namespace App.Application.Projects.Commands
{
    public class CreateProjectCommandHandler: ICommandHandler<CreateProjectCommand>
    {
        private readonly IRepository<Project> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IRepository<Project> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request.Project);
            await _repository.Insert(project);
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}