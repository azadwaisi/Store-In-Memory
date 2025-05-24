using Application.Contracts.Specification;
using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(Guid Id,CancellationToken cancellationToken);
		Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
		Task<T> AddAsync(T entity, CancellationToken cancellationToken);
		Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);	
		Task Delete(T entity,CancellationToken cancellationToken);
		Task<bool> AnyAsync(Expression<Func<T,bool>> expression,CancellationToken cancellationToken);
		Task<bool> AnyAsync(CancellationToken cancellationToken);
		Task<T> GetEntityWithSpec(ISpecification<T> spec,CancellationToken cancellationToken);
		Task<IReadOnlyList<T>> ListAsyncSpec(ISpecification<T> spec,CancellationToken cancellationToken);
		Task<int> CountAsyncSpec(ISpecification<T> spec, CancellationToken cancellationToken);
	}
}


//pagination => count , get all, take , skip
//sort => name , title , price
//order => desc , asc 
//pagination => true , false
