using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Commands;
using App.Domain.IRepositories;
using App.Domain.SeedWork;
using App.Domain.Users;
using App.Domain.Users.Rules;
using AutoMapper;
using MediatR;

namespace App.Application.Users.Commands
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IRepository<User> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserUniquenessChecker _uniquenessChecker;
        private readonly IBusinessRule _businessRule;

        public RegisterUserCommandHandler(
            IRepository<User> repository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IUserUniquenessChecker uniquenessChecker,
            IBusinessRule businessRule)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _businessRule = businessRule;
            _uniquenessChecker = uniquenessChecker;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.User);
            await user.CheckRule(new UserEmailUniqueRule(_uniquenessChecker).SetEmail(user.Email));

            await _repository.Insert(user);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}