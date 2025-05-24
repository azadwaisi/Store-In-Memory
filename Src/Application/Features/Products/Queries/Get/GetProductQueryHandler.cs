using Application.Contracts;
using Application.Dtos.Products;
using Application.Features.Products.Queries.GetAll;
using AutoMapper;
using Domain.Entities.Products;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.Get
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public GetProductQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id,cancellationToken);
			if (result == null) throw new NotFoundEntityException();
			
			return _mapper.Map<ProductDto>(result);
		}
	}
}
