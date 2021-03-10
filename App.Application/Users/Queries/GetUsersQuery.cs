using System.Collections.Generic;
using App.Application.Configuration.Queries;

namespace App.Application.Users.Queries
{
    public class GetUsersQuery : IQuery<List<UserDto>>
    {
        public bool IsActive { get; }

        public GetUsersQuery(bool isActive)
        {
            IsActive = isActive;
        }
    }
}