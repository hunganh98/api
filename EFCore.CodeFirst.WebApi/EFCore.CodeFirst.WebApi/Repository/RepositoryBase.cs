using EFCore.CodeFirst.WebApi.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Repository
{

    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public ApplicationDbContext applicationDbContext { get; set; }
        public RepositoryBase(ApplicationDbContext repositoryContext)
        {
            this.applicationDbContext = repositoryContext;
        }
        public IQueryable<T> FindAll()
        {
            return this.applicationDbContext.Set<T>().AsNoTracking();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.applicationDbContext.Set<T>().Where(expression).AsNoTracking();
        }
        public T Create(T entity)
        {
           applicationDbContext.Set<T>().Add(entity);
           applicationDbContext.Entry<T>(entity).State = EntityState.Added;
           applicationDbContext.Save();
           return entity;

        }
        public T Update(T entity)
        {
            applicationDbContext.Set<T>().Attach(entity);
            applicationDbContext.Entry<T>(entity).State = EntityState.Modified;
            applicationDbContext.Save();
            return entity;
        }
        public void Delete(T entity)
        {
            applicationDbContext.Set<T>().Remove(entity);
            applicationDbContext.Entry<T>(entity).State = EntityState.Deleted;
            applicationDbContext.Save();
            return;
        }
    }

}
