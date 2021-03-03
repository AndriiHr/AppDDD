using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;
using App.Domain.Feedbacks;
using App.Domain.IRepositories;
using App.Domain.Projects;
using App.Domain.SeedWork;
using AutoMapper;
using MediatR;

namespace App.Application.Feedbacks.Commands
{
    internal class AssignFeedbackToProjectCommandHandler : ICommandHandler<AssignFeedbackToProjectCommand>
    {
        private readonly IRepository<Project> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AssignFeedbackToProjectCommandHandler(IRepository<Project> repository, IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AssignFeedbackToProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetSingle(x=>x.Id == request.ProjectId);
            var feedback = _mapper.Map<Feedback>(request.Feedback);
            
            project.AddFeedbackToProject(feedback.Text);
            
            _repository.Update(project);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}