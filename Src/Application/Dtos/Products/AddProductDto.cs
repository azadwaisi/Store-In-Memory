using Application.Common.Mapping;
using Application.Common.Mapping.Resolver;
using AutoMapper;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Products
{
	public class AddProductDto : IMapFrom<Product>
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

		//public void Mapping(Profile profile)
		//{
		//}
	}
}
