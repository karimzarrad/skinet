using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
    {
        public void add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public async Task<int> CountAsync(Ispecifications<T> spec)
        {
            var query=context.Set<T>().AsQueryable();
            query= spec.ApplyCreteria(query);
            return await query.CountAsync();
        }

        public bool Exists(int id)
        {
            return context.Set<T>().Any(x=>x.Id==id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Ispecifications<T> spec)
        {
           return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<IReadOnlyList<TResult>> GetAllAsync<TResult>(Ispecifications<T, TResult> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetItemWithSpec( Ispecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<TResult?> GetItemWithSpec<TResult>(Ispecifications<T, TResult> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public void remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public async Task<bool> saveallasync()
        {
           return  await context.SaveChangesAsync()>0;
        }

        public void update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        private IQueryable<T>ApplySpecification(Ispecifications<T>spec)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(),spec);
        }
        private IQueryable<TResult> ApplySpecification<TResult>(Ispecifications<T,TResult> spec)
        {
            return SpecificationEvaluator<T>.GetQuery<T,TResult>(context.Set<T>().AsQueryable(), spec);
        }
    }
}
