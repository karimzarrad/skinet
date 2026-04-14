using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, Ispecifications<T> spec) {
            if(spec.Creteria!=null)
            {
                query=query.Where(spec.Creteria);
            }
            if (spec.Orderby != null)
            {
                query=query.OrderBy(spec.Orderby);

            }
            if (spec.OrderbyDescinding != null)
            {
                query = query.OrderByDescending(spec.OrderbyDescinding);

            }
            if(spec.IsDistinct)
            {
                query=query.Distinct();
            }
            if (spec.IsPagingEnabled)
            {
                query=query.Skip(spec.Skip).Take(spec.Take);    
            }
            return query;



        }
    
    
        public static IQueryable<TResult> GetQuery<Tspec,TResult>(IQueryable<T> query, Ispecifications<T, TResult> spec)
        {
            if (spec.Creteria != null)
            {
                query = query.Where(spec.Creteria);
            }
            if (spec.Orderby != null)
            {
                query = query.OrderBy(spec.Orderby);

            }
            if (spec.OrderbyDescinding != null)
            {
                query = query.OrderByDescending(spec.OrderbyDescinding);

            }
            var SelectQuery = query as IQueryable<TResult>;
            if (spec.Select != null)
            {

                SelectQuery = query.Select(spec.Select);
            }
            if (spec.IsDistinct)
            {
                SelectQuery = SelectQuery?.Distinct();
            }
            if (spec.IsPagingEnabled)
            {
                SelectQuery = SelectQuery?.Skip(spec.Skip).Take(spec.Take);
            }
            return SelectQuery ?? query.Cast<TResult>();



        }
    }

}
