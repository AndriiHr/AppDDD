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
    public class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
    {
        private readonly IRepository<Project> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProjectCommandHandler(IRepository<Project> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            await _repository.Delete(request.Id);
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return  Unit.Value;
        }
    }
}