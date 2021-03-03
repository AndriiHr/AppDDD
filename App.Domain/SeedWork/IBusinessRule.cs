using System.Threading.Tasks;

namespace App.Domain.SeedWork
{
    public interface IBusinessRule
    {
        Task<bool> IsBroken();
        string Message { get; }
    }
}
