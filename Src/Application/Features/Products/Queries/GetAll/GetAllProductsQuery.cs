using Application.Contracts;
using Application.Dtos.Products;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
	public class GetAllProductsQuery : RequestParametersBasic , IRequest<PaginationResponse<ProductDto>>, ICacheQuery
	{
		public int HoursSaveData => 1; //1 Hour save data
	}
}
