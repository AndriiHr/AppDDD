using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class UpdateProjectCommandHandler : ICommandHandler<UpdateProjectCommand>
    {
        private readonly IRepository<Project> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProjectCommandHandler(IRepository<Project> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request.Project);
            _repository.Update(project);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}