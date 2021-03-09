using System;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;
using App.Domain.IRepositories;
using App.Domain.Projects;
using App.Domain.SeedWork;
using App.Domain.Users;
using AutoMapper;
using MediatR;

namespace App.Application.Projects.Commands
{
    public class AssignUserToProjectCommandHandler : ICommandHandler<AssignUserToProjectCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssignUserToProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignUserToProjectCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(x => x.Id == request.UserId);
            var project = await _unitOfWork.ProjectRepository.GetSingle(x => x.Id == request.ProjectId);

            if (user is null || project is null)
            {
                return Unit.Value;
            }

            project.AssignUserToProject(user);

            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.ProjectRepository.Update(project);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
            }

            await transaction.DisposeAsync();

            return Unit.Value;
        }
    }
}