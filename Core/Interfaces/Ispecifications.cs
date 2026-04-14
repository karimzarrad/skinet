using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Interfaces
{
    public interface Ispecifications<T>
    {
        Expression<Func<T, bool>>? Creteria {  get; }
        Expression<Func<T, Object>>? Orderby { get; }
        Expression<Func<T, Object>>? OrderbyDescinding { get;}
        bool IsDistinct {  get; }
         int Take { get;}
        int Skip {  get;}
        bool IsPagingEnabled {  get; }
        IQueryable<T>ApplyCreteria(IQueryable<T> query);
    }
    public interface Ispecifications<T,TResult>:Ispecifications<T>
    {
       public Expression <Func<T, TResult>> ?Select {  get; }
    }
}
