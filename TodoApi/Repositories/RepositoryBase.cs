using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TodoApi.Contracts.Repositories;
using TodoApi.Data;

namespace TodoApi.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly TodoContext _context;

        public RepositoryBase(TodoContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll() => _context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            _context.Set<T>().Where(expression).AsNoTracking();

        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
    }
}
