using System;
using App.Application.Configuration.Commands;
using App.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Users.Commands
{
    public class AssignUserToProjectCommandHandler : ICommandHandler<AssignUserToProjectCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssignUserToProjectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AssignUserToProjectCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(x => x.Id == request.UserId);
            var project = await _unitOfWork.ProjectRepository.GetSingle(x => x.Id == request.ProjectId);

            if (user is null || project is null)
            {
                return Unit.Value;
            }

            user.AssignProjectToUser(project);

            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.UserRepository.Update(user);
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