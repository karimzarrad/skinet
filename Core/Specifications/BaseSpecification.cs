using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Security;
using System.Text;

namespace Core.Specifications
{
    public class BaseSpecification<T>(Expression<Func<T, bool>>? creteria) : Ispecifications<T>
    {
        public BaseSpecification() : this(null)
        {

        }
        public Expression<Func<T, bool>>? Creteria => creteria;

        public Expression<Func<T, object>>? Orderby { get; private set; }
        public Expression<Func<T, object>>? OrderbyDescinding { get; private set; }

        public bool IsDistinct { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void Addorderby(Expression<Func<T, object>>? orderby)
        {
            Orderby = orderby;
        }
        protected void AddorderbyDescinding(Expression<Func<T, object>>? orderbydescinding)
        {
            OrderbyDescinding = orderbydescinding;
        }
        protected void AddDistinct()
        {
            IsDistinct = true;
        }
        protected void ApplyPaging(int skip,int take)
        {
            Skip = skip;
            Take = take;
            
            IsPagingEnabled = true;
        }

        public IQueryable<T> ApplyCreteria(IQueryable<T> query)
        {
            if(Creteria!=null)
            {
                query=query.Where(Creteria);
            }
            return query;   
        }
    }
    public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? creteria) : BaseSpecification<T>(creteria), Ispecifications<T, TResult>
    {
        public BaseSpecification() : this(null)
        {

        }
        public Expression<Func<T, TResult>>? Select { get; private set; }

        protected void addselect(Expression<Func<T, TResult>>? select)
        {
            Select = select;
        }
    }
}

