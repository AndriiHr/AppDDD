using System.Threading.Tasks;

namespace App.Domain.Users.Rules
{
    public interface IUserUniquenessChecker
    {
        Task<bool> IsUnique(string userEmail);
    }
}