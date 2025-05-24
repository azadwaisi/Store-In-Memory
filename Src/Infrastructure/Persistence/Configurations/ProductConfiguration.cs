using Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.PictureUrl).HasMaxLength(255);
			builder.Property(x => x.ProductName).HasMaxLength(255).IsRequired();
			builder.Property(x => x.UnitPrice).HasColumnType("decimal(10, 2)").IsRequired();
			builder.Property(x => x.UnitsInStock).HasDefaultValue(0).IsRequired();
			builder.Property(x => x.UnitsOnOrder).HasDefaultValue(0).IsRequired();
			builder.Property(x => x.ReorderLevel).HasDefaultValue(0).IsRequired();
			builder.Property(x => x.Discontinued).HasDefaultValue(0).IsRequired();

			builder.HasIndex(p => p.ProductBrandId).IsUnique(false);
			builder.HasIndex(p => p.ProductTypeId).IsUnique(false);

			//builder.HasOne(x=>x.ProductType).WithMany().HasForeignKey(x => x.ProductTypeId);
			//builder.HasOne(x=>x.ProductBrand).WithMany().HasForeignKey(x => x.ProductBrandId);
			builder.Property(x => x.Barcode).HasMaxLength(100);
			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.Description).HasMaxLength(500);
			builder.Property(x=>x.Summary).HasMaxLength(255);
		}
	}
}
