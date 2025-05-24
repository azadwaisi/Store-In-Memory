using Application.Contracts;
using Domain.Entities.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductBrands.Queries.Get
{
    public class GetProductBrandQueryHandler : IRequestHandler<GetProductBrandQuery, ProductBrand>
	{
		private readonly IUnitOfWork _unitOfWork;
        public GetProductBrandQueryHandler(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
        }
        public async Task<ProductBrand> Handle(GetProductBrandQuery request, CancellationToken cancellationToken)
		{
			return await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(request.Id,cancellationToken);
		}
	}
}
