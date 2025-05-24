using Application.Contracts;
using Domain.Entities.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductBrands.Queries.GetAll
{
    public class GetAllProductBrandsQueryHandler : IRequestHandler<GetAllProductBrandsQuery, IEnumerable<ProductBrand>>
	{
		private readonly IUnitOfWork _unitOfWork;
        public GetAllProductBrandsQueryHandler(IUnitOfWork unitOfWork)
        {
				_unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<ProductBrand>> Handle(GetAllProductBrandsQuery request, CancellationToken cancellationToken)
		{
			return await _unitOfWork.Repository<ProductBrand>().GetAllAsync(cancellationToken);
		}
	}
}
