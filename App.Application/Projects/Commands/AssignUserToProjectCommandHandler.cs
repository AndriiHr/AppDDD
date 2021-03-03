using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;
using App.Domain.IRepositories;
using App.Domain.Projects;
using App.Domain.SeedWork;
using App.Domain.Users;
using AutoMapper;
using MediatR;

namespace App.Application.Projects.Commands
{
    public class AssignUserToProjectCommandHandler : ICommandHandler<AssignUserToProjectCommand>
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssignUserToProjectCommandHandler(IRepository<Project> projectRepository,
            IRepository<User> userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignUserToProjectCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetSingle(x => x.Id == request.UserId);
            var project = await _projectRepository.GetSingle(x => x.Id == request.ProjectId);

            if (user is null || project is null)
            {
                return Unit.Value;
            }

            project.AssignUserToProject(user);
            _projectRepository.Update(project);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}