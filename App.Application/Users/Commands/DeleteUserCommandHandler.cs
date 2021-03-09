using System;
using App.Application.Configuration.Commands;
using App.Domain.SeedWork;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Users.Commands
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.UserRepository.Delete(request.Id);
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