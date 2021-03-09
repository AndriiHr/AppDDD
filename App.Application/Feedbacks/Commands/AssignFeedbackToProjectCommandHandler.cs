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
    internal class AssignFeedbackToProjectCommandHandler : ICommandHandler<AssignFeedbackToProjectCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AssignFeedbackToProjectCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AssignFeedbackToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetSingle(x => x.Id == request.ProjectId);
            var feedback = _mapper.Map<Feedback>(request.Feedback);

            project.AddFeedbackToProject(feedback.Text);

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