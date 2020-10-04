using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Persistence.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationContext Context { get; set; }
        public GenericRepository(ApplicationContext context)
        {
            Context = context;
        }
        public IQueryable<T> FindAll()
        {
            return Context.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
    }
}
