using MediatR;

namespace App.Application.Configuration.Queries
{
    public interface IQuery<out TResult>: IRequest<TResult>
    {
        
    }
}