using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;
using App.Domain.Feedbacks;
using App.Domain.IRepositories;
using App.Domain.SeedWork;
using AutoMapper;
using MediatR;

namespace App.Application.Feedbacks.Commands
{
    public class DeleteFeedbackCommandHandler : ICommandHandler<DeleteFeedbackCommand>
    {
        private readonly IRepository<Feedback> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFeedbackCommandHandler(IRepository<Feedback> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
        {
            await _repository.Delete(request.Id);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}