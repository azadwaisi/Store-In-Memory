using Application.Dtos.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.AddProduct
{
	//public record AddProductCommand(AddProductDto Product) : IRequest<ProductDto>;
	public class AddProductCommand : IRequest<ProductDto>
	{
		public string ProductName { get; set; }
		public string Barcode { get; set; }
		public decimal UnitPrice { get; set; }
		public int UnitsInStock { get; set; }
		public int UnitsOnOrder { get; set; }
		public int ReorderLevel { get; set; }
		public bool Discontinued { get; set; }
		public string PictureUrl { get; set; }
		//
		public Guid ProductTypeId { get; set; }
		public Guid ProductBrandId { get; set; }
		public Guid UserId { get; set; }
		//
		public string Description { get; set; }
	}
}

//can use this
//public class AddProductCommand : IRequest<Guid> // شناسه محصول جدید را برمی‌گرداند
//{
//	public string Name { get; set; }
//	public string Description { get; set; }
//	public decimal Price { get; set; }
//	public Guid CategoryId { get; set; }
//	// ... سایر ویژگی‌ها
//}