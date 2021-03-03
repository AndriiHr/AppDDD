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
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IRepository<User> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _repository.Delete(request.Id);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
