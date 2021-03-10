using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Queries;
using App.Domain.IRepositories;
using App.Domain.Users;
using AutoMapper;

namespace App.Application.Users.Queries
{
    internal sealed class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetSingle(x => x.Id == request.Id, x => x.Projects, x => x.Projects.Select(x => x.Feedbacks));
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}