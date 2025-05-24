using Application.Dtos.Products;
using Domain.Entities.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.UpdateProduct
{
	public class UpdateProductCommand : IRequest<ProductDto>
	{
		public Guid Id { get; set; }
		public string ProductName { get; set; }
		public string Barcode { get; set; }
		public decimal UnitPrice { get; set; }
		public bool Discontinued { get; set; }
		public string PictureUrl { get; set; }
		public Guid ProductTypeId { get; set; }
		public Guid ProductBrandId { get; set; }
		public Guid UserId { get; set; }
		public string Description { get; set; }
	}
}
