using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Specification
{
	public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
	{
		//x=>x.Title == title && x.CreateDate > DateTime.Now || &&
		public Expression<Func<T, bool>> Predicate { get; } 

		public List<Expression<Func<T, object>>> Includes { get; } = new();
		public Expression<Func<T, object>> OrderBy { get; private set; }

		public Expression<Func<T, object>> OrderByDesc { get; private set; }
		public Expression<Func<T, bool>> Search { get; private set; }

		public int Take { get; set; }

		public int Skip { get; set; }

		public bool IsPagingEnabled { get; set; } = true;

		

		public BaseSpecification()
        {
            
        }

        public BaseSpecification(Expression<Func<T, bool>> expression)
        {
			Predicate = expression;
        }
        protected void AddInclude(Expression<Func<T, object>> expression)
        {
            Includes.Add(expression);
        }
		protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}
		protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescByExpression)
		{
			OrderByDesc = orderByDescByExpression;
		}
		protected void AddSearch(Expression<Func<T, bool>> search)
		{
			Search = search;
		}
		protected void ApplyPaging(int take , int skip , bool isPagingEnabled =true)
		{
			Take = take;
			Skip = skip;
			IsPagingEnabled = isPagingEnabled;
		}
	}
}
