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
    public class EditFeedbackCommandHandler : ICommandHandler<EditFeedbackCommand>
    {
        private readonly IRepository<Feedback> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EditFeedbackCommandHandler(IRepository<Feedback> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(EditFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedback = _mapper.Map<Feedback>(request.Feedback);
          
            _repository.Update(feedback);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}