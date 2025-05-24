using Application.Dtos.Products;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.Get
{
	public class GetProductQuery:IRequest<ProductDto>
	{
        public Guid Id { get; set; }
        public GetProductQuery(Guid id)
        {
            Id = id;
        }
    }
}
