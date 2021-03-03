using App.Domain.Users;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<User> GetById(int id);
    }
}
