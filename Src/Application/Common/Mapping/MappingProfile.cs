using Application.Common.Mapping.Resolver;
using Application.Dtos.Products;
using Application.Features.Products.Commands.AddProduct;
using AutoMapper;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Common.Mapping
{
    public class MappingProfile : Profile
	{
        public MappingProfile()
        {
			CreateMap<Product, ProductDto>()
				.ForMember(x=>x.PictureUrl,c=>c.MapFrom<ProductImageUrlResolver>())
				.ForMember(x => x.ProductTypeName, c => c.MapFrom(d => d.ProductType.Title))
				.ForMember(x => x.ProductBrandName, c => c.MapFrom(d => d.ProductBrand.Title));
			CreateMap<ProductDto, Product>();
			CreateMap<Product, AddProductDto>();
			CreateMap<AddProductDto, Product>();
			CreateMap<AddProductCommand, Product>();
		}
    }
}
