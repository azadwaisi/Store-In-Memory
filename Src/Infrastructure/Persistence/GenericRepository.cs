using Application.Contracts;
using Application.Contracts.Specification;
using Domain.Entities;
using Domain.Entities.Base;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity 
	{
		//singlton => request 1 , request 2 => 1 new
		//transient => request 1 => new , request 2 => new
		//scoped => request 1 , request 2 => 2 new

		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;
		public GenericRepository( ApplicationDbContext dbContext )
        {
			_context = dbContext;
			_dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
		{
			await _dbSet.AddAsync( entity, cancellationToken ).ConfigureAwait( false);
			return await Task.FromResult( entity );

		}

		public async Task<bool> AnyAsync(Expression<Func<T,bool>> expression,CancellationToken cancellationToken)
		{
			return await _dbSet.AnyAsync(expression,cancellationToken);
		}

		public async Task<bool> AnyAsync(CancellationToken cancellationToken)
		{
			return await _dbSet.AnyAsync(cancellationToken);
		}

		public async Task Delete(T entity,CancellationToken cancellationToken)
		{
			//_context.Entry(entity).State = EntityState.Deleted;
			//return Task.FromResult( entity );
			//2 - _dbSet.Remove(entity);
			var res = await GetByIdAsync(entity.Id, cancellationToken);
			res.IsDelete = true;
			await UpdateAsync(entity,cancellationToken);
		}

		public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _dbSet.ToListAsync(cancellationToken);
		}

		public async Task<T> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
		{
			return await _dbSet.FindAsync(Id,cancellationToken);
		}

		public async Task<T> GetEntityWithSpec(ISpecification<T> spec,CancellationToken cancellationToken)
		{
			return await ApplySpecification(spec).FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<IReadOnlyList<T>> ListAsyncSpec(ISpecification<T> spec,CancellationToken cancellationToken)
		{
			return await ApplySpecification(spec).ToListAsync(cancellationToken);
		}
		public async Task<int> CountAsyncSpec(ISpecification<T> spec, CancellationToken cancellationToken)
		{
			return await ApplySpecification(spec).CountAsync(cancellationToken);
		}

		public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
		{
			_context.Entry(entity).State = EntityState.Modified;
			return await Task.FromResult( entity );
		}
		private IQueryable<T> ApplySpecification(ISpecification<T> spec)
		{
			var aaa = _dbSet.AsQueryable();
			return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(),spec);
		}
	}
}
