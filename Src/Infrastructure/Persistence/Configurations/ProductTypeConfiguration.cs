using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
	{
		public void Configure(EntityTypeBuilder<ProductType> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Description).HasMaxLength(500);
			builder.Property(x => x.Title).HasMaxLength(100);
			builder.Property(x => x.Summary).HasMaxLength(255);

			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.Description).HasMaxLength(500);
			builder.Property(x => x.Summary).HasMaxLength(255);
		}
	}
}
