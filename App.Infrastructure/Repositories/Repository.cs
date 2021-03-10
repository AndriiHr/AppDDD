using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Domain.Enums;
using App.Domain.IRepositories;
using App.Domain.SeedWork;
using App.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using AppContext = App.Infrastructure.Database.AppContext;

namespace App.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly AppContext _context;
        private DbSet<T> _entities;

        public Repository(AppContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include)
        {
            var dbSet = _entities.AsNoTracking();

            var query = include.Aggregate(dbSet, (current, item) => EvaluateInclude(current, item));
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Specification<T> spec, params Expression<Func<T, object>>[] include)
        {
            var dbSet = _entities.AsNoTracking();
            var query = include.Aggregate(dbSet, (current, item) => EvaluateInclude(current, item));

            return await query.Where(spec.ToExpression()).ToListAsync();
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] include)
        {
            var dbSet = _entities.AsNoTracking();

            var query = include.Aggregate(dbSet, (current, item) => EvaluateInclude(current, item));
            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task Insert(T entity)
        {
            entity.SetRecordStatus(RecordStatus.Active);
            await _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var entity = await GetSingle(x => x.Id == id);
            entity.SetRecordStatus(RecordStatus.Inactive);
            Update(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


        private IQueryable<T> EvaluateInclude(IQueryable<T> current, Expression<Func<T, object>> item)
        {
            if (item.Body is MethodCallExpression)
            {
                var arguments = ((MethodCallExpression)item.Body).Arguments;
                if (arguments.Count > 1)
                {
                    var navigationPath = string.Empty;
                    for (var i = 0; i < arguments.Count; i++)
                    {
                        var arg = arguments[i];
                        var path = arg.ToString().Substring(arg.ToString().IndexOf('.') + 1);

                        navigationPath += (i > 0 ? "." : string.Empty) + path;
                    }
                    return current.Include(navigationPath);
                }
            }

            return current.Include(item);
        }

    }
}