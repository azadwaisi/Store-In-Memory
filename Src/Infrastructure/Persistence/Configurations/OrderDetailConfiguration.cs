using Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
	public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
	{
		public void Configure(EntityTypeBuilder<OrderDetail> builder)
		{
			builder.HasKey(x => new { x.Id , x.ProductId } );
			builder.Property(x => x.ProductId).IsRequired();
			builder.Property(x=>x.UnitPrice).HasColumnType("decimal(10, 2)").IsRequired();
			builder.Property(x=>x.Discount).HasColumnType("decimal(10, 2)").HasDefaultValue(0).IsRequired();
			builder.Property(x=>x.Quantity).IsRequired();

			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.Description).HasMaxLength(500);
			builder.Property(x => x.Summary).HasMaxLength(255);
			//
		}
	}
}
