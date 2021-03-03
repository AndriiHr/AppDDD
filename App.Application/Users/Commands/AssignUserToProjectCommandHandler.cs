using App.Application.Configuration.Commands;
using App.Domain.IRepositories;
using App.Domain.Projects;
using App.Domain.SeedWork;
using App.Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Users.Commands
{
    public class AssignUserToProjectCommandHandler : ICommandHandler<AssignUserToProjectCommand>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignUserToProjectCommandHandler(
            IRepository<User> userRepository,
            IRepository<Project> projectRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AssignUserToProjectCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetSingle(x => x.Id == request.UserId);
            var project = await _projectRepository.GetSingle(x => x.Id == request.ProjectId);

            if (user is null || project is null)
            {
                return Unit.Value;
            }

            user.AssignProjectToUser(project);
            _userRepository.Update(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
