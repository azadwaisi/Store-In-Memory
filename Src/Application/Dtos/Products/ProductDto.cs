using Application.Common.Mapping;
using Application.Dtos.Common;
using AutoMapper;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Products
{
    public class ProductDto : CommonDto , IMapFrom<Product>
	{
		public Guid Id { get; set; }
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

		public string ProductTypeName { get; set; }
		public string ProductBrandName { get; set; }
		public string UserName { get; set; }

		public DateTime CreatedDate { get; set; }
		public Guid CreateUserId { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public Guid? ModifiedUserId { get; set; }
		//public void Mapping(Profile profile)
		//{
		//	profile.CreateMap<Product, ProductDto>()
		//		.ForMember(x => x.ProductTypeName, c => c.MapFrom(d => d.ProductType.Title))
		//		.ForMember(x => x.ProductBrandName, c => c.MapFrom(d => d.ProductBrand.Title));
		//}
	}
}
