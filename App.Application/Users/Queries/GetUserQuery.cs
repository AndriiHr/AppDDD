using App.Application.Configuration.Queries;

namespace App.Application.Users.Queries
{
    public class GetUserQuery : IQuery<UserDto>
    {
        public int Id { get; }

        public GetUserQuery(int id)
        {
            Id = id;
        }
    }
}