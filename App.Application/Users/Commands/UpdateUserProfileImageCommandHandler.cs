using System;
using App.Application.Configuration.Commands;
using App.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Users.Commands
{
    internal class UpdateUserProfileImageCommandHandler : ICommandHandler<UpdateUserProfileImageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserProfileImageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserProfileImageCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetSingle(x => x.Id == request.Id);
            user.ImageProfile = request.ImageProfile;

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

            await transaction.RollbackAsync(cancellationToken);

            return Unit.Value;
        }
    }
}