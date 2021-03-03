using App.Application.Configuration.Commands;
using App.Domain.IRepositories;
using App.Domain.SeedWork;
using App.Domain.Users;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Users.Commands
{
    internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IRepository<User> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.User);

            _repository.Update(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
