using App.Domain.SeedWork;
using System.Threading.Tasks;

namespace App.Domain.Users.Rules
{
    public class UserEmailUniqueRule : IBusinessRule
    {
        private readonly IUserUniquenessChecker _userUniquenessChecker;
        private string _email;

        public string Message => "Email already taken";

        public UserEmailUniqueRule(IUserUniquenessChecker userUniquenessChecker)
        {
            _userUniquenessChecker = userUniquenessChecker;
        }

        public UserEmailUniqueRule SetEmail(string email)
        {
            _email = email;
            return this;
        }

        public async Task<bool> IsBroken()
        {
            return !(await _userUniquenessChecker.IsUnique(_email));
        }
    }
}