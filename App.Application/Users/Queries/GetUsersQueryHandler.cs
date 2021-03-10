using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Application.Configuration.Queries;
using App.Domain.Enums;
using App.Domain.IRepositories;
using App.Domain.Users;
using AutoMapper;

namespace App.Application.Users.Queries
{
    internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var recordStatus = request.IsActive ? RecordStatus.Active : RecordStatus.Inactive;

            var users = await _repository.GetAll(x=>x.RecordStatus == recordStatus);
            var usersDto = users.Select(x => _mapper.Map<UserDto>(x)).ToList();
           
            return usersDto;
        }
    }
}