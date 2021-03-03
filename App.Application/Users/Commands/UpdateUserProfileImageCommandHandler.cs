using App.Application.Configuration.Commands;
using App.Domain.IRepositories;
using App.Domain.SeedWork;
using App.Domain.Users;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Users.Commands
{
    internal class UpdateUserProfileImageCommandHandler : ICommandHandler<UpdateUserProfileImageCommand>
    {
        private readonly IRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;


        public UpdateUserProfileImageCommandHandler(IRepository<User> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserProfileImageCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetSingle(x => x.Id == request.Id);
            user.ImageProfile = request.ImageProfile;

            _repository.Update(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
