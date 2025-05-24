using Application.Dtos.Products;
using AutoMapper;
using Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping.Resolver
{
    public class ProductImageUrlResolver : IValueResolver<Product, ProductDto, string>
	{
		private readonly IConfiguration _configuration;
        public ProductImageUrlResolver(IConfiguration configuration)
        {
				_configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
		{
			if(!string.IsNullOrEmpty(source.PictureUrl))
				return _configuration["BackendUrl"] + _configuration["LocationImages:ProductsImageLocation"] + source.PictureUrl;

			return null;
		}
	}
}
