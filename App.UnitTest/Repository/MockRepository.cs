using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Domain.IRepositories;
using App.Domain.SeedWork;
using App.Domain.Specifications;

namespace App.UnitTest.Repository
{
    public class MockRepository<T> : IRepository<T> where T : Entity
    {
        public List<T> _context;

        public MockRepository(List<T> ctx)
        {
            _context = ctx;
        }
        
        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] include)
        {
            return await Task.FromResult(_context.ToList());
        }

        public async Task<IEnumerable<T>> GetAll(Specification<T> spec, params Expression<Func<T, object>>[] include)
        {
            return await Task.FromResult(_context.ToList());
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include)
        {
            var func = predicate.Compile();
            return await Task.FromResult(_context.FirstOrDefault(func));
        }

        public Task Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}