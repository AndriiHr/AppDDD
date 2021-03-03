using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Domain.SeedWork;
using App.Domain.Specifications;

namespace App.Domain.IRepositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include);
        Task<IEnumerable<T>> GetAll(Specification<T> spec, params Expression<Func<T, object>>[] include);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include); 
        Task Insert(T entity);
        void Update(T entity);
        Task Delete(int id);
        Task Save();
    }
}