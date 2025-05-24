using Application.Contracts.Specification;
using Application.Features.Products.Queries.GetAll;
using Application.Wrappers;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductSpec : BaseSpecification<Product>
	{
		public GetAllProductSpec(GetAllProductsQuery specParams) : base(Expression.ExpressionSpec(specParams)) 
		{
			AddInclude(x => x.ProductBrand);
			AddInclude(x => x.ProductType);
			//if(specParams.Search != null)
			//	AddSearch(x=>x.Title.Contains(specParams.Search));
			if (specParams.TypeSort == TypeSort.Desc)
				switch (specParams.Sort)
				{
					case 1:
						AddOrderByDesc(x => x.ProductName);
						break;
					case 2:
						AddOrderByDesc(x => x.ProductType.Title);
						break;
					case 3:
						AddOrderByDesc(x => x.UnitPrice);
						break;
					default:
						AddOrderByDesc(x => x.ProductName);
						AddOrderByDesc(x => x.ProductName.Contains("AAAA") || x.PictureUrl.Contains("AAA"));
						break;
				}
			else
				switch (specParams.Sort)
				{
					case 1:
						AddOrderBy(x => x.ProductName);
						break;
					case 2:
						AddOrderBy(x => x.ProductType.Title);
						break;
					case 3:
						AddOrderBy(x => x.UnitPrice);
						break;
					default:
						AddOrderBy(x => x.ProductName);
						break;
				}
			ApplyPaging(specParams.PageSize, specParams.PageSize * (specParams.PageIndex - 1), true);
		}
		public GetAllProductSpec(Guid id) : base(x=> x.Id == id)
        {
			AddInclude(x => x.ProductBrand);
			AddInclude(x => x.ProductType);
		}
    }
}

public class ProductsCountSpec : BaseSpecification<Product>
{
	public ProductsCountSpec(GetAllProductsQuery specParams) : base(Expression.ExpressionSpec(specParams))
	{
		IsPagingEnabled = false;
	}
}

public class Expression
{
	public static Expression<Func<Product, bool>> ExpressionSpec(GetAllProductsQuery specParams)
	{
		return x =>
				String.IsNullOrEmpty(specParams.Search) || x.ProductName.ToLower().Contains(specParams.Search);
			//(string.IsNullOrEmpty(specParams.Search) || x.Title.ToLower().Contains(specParams.Search))
			//&&
			//(!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId.Value)
			//&&
			//(!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId.Value);
	}
}
