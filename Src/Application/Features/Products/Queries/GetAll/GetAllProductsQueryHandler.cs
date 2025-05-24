using Application.Contracts;
using Application.Dtos.Products;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginationResponse<ProductDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public GetAllProductsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PaginationResponse<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
		{
			var spec = new GetAllProductSpec(request);
			var result = await _unitOfWork.Repository<Product>().ListAsyncSpec(spec, cancellationToken);
			var count = await _unitOfWork.Repository<Product>().CountAsyncSpec(new ProductsCountSpec(request),cancellationToken);
			var res = _mapper.Map<IEnumerable<ProductDto>>(result);
			return new PaginationResponse<ProductDto>(request.PageIndex, request.PageSize, count, res);
			//return await _unitOfWork.Repository<Product>().GetAllAsync(cancellationToken);
		}
	}
}
