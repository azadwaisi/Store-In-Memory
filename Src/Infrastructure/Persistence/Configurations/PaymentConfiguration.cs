using Domain.Entities.Orders;
using Domain.Entities.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
	public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
	{
		public void Configure(EntityTypeBuilder<Payment> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.OrderId).IsRequired();
			builder.Property(x => x.PaymentMethod).HasColumnType("varchar(50)").IsRequired();
			builder.Property(x => x.Status).HasColumnType("varchar(50)").IsRequired();
			builder.Property(p => p.TransactionId).HasMaxLength(100);
			builder.Property(p => p.Amount).HasColumnType("decimal(10, 2)").IsRequired();
			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.Description).HasMaxLength(500);
			builder.Property(x => x.Summary).HasMaxLength(255);

		}
	}
}
