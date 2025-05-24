using Domain.Entities.Orders;
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
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x=>x.UserId).IsRequired();
			builder.Property(x=>x.OrderDate).IsRequired();
			builder.Property(x=>x.Freight).HasColumnType("decimal(10, 2)");
			builder.Property(x=>x.ShipName).HasMaxLength(255);
			builder.Property(x=>x.ShipAddress).HasMaxLength(255);
			builder.Property(x=>x.ShipCity).HasMaxLength(255);
			builder.Property(x=>x.ShipRegion).HasMaxLength(255);
			builder.Property(x=>x.ShipPostalCode).HasMaxLength(20);
			builder.Property(x=>x.ShipCountry).HasMaxLength(100);

			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.Description).HasMaxLength(500);
			builder.Property(x => x.Summary).HasMaxLength(255);
			//
		}
	}
}
