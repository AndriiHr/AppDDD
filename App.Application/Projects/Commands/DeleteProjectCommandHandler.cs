using System;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;
using App.Domain.SeedWork;
using AutoMapper;
using MediatR;

namespace App.Application.Projects.Commands
{
    public class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.ProjectRepository.Delete(request.Id);
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