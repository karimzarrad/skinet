using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IGenericRepository<T>where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetItemWithSpec(Ispecifications<T> spec);

        Task<IReadOnlyList<T>> GetAllAsync(Ispecifications<T>spec);
        Task<TResult?> GetItemWithSpec<TResult>(Ispecifications<T,TResult> spec);

        Task<IReadOnlyList<TResult>> GetAllAsync<TResult>(Ispecifications<T,TResult> spec);
        void add(T entity);
        void remove(T entity);
        void update(T entity);
        bool Exists(int id);
        Task<bool> saveallasync();
        Task<int>CountAsync(Ispecifications<T> spec);
    }
}
