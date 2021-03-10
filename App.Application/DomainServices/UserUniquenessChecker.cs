using App.Domain.IRepositories;
using App.Domain.Users;
using App.Domain.Users.Rules;
using System.Threading.Tasks;

namespace App.Application.DomainServices
{
    public class UserUniquenessChecker : IUserUniquenessChecker
    {
        private readonly IRepository<User> _repository;

        public UserUniquenessChecker(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsUnique(string userEmail)
        {
            var user = await _repository.GetSingle(x => x.Email == userEmail);
            return user is null;
        }
    }
}
