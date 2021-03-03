using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Queries;
using App.Domain.Feedbacks;
using App.Domain.IRepositories;
using AutoMapper;

namespace App.Application.Feedbacks.Queries
{
    public class GetFeedbackQueryHandler : IQueryHandler<GetFeedbackQuery, FeedbackDto>
    {
        private readonly IRepository<Feedback> _repository;
        private readonly IMapper _mapper;

        public GetFeedbackQueryHandler(IRepository<Feedback> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FeedbackDto> Handle(GetFeedbackQuery request, CancellationToken cancellationToken)
        {
            var feedback = await _repository.GetSingle(x=>x.Id == request.Id);
            var feedbackDto = _mapper.Map<FeedbackDto>(feedback);

            return feedbackDto;
        }
    }
}