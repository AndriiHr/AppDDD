using System;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;
using App.Domain.Feedbacks;
using App.Domain.SeedWork;
using AutoMapper;
using MediatR;

namespace App.Application.Feedbacks.Commands
{
    public class EditFeedbackCommandHandler : ICommandHandler<EditFeedbackCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditFeedbackCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(EditFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = _mapper.Map<Feedback>(request.Feedback);


            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                _unitOfWork.FeedbackRepository.Update(feedback);
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