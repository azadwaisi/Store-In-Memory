using Domain.Entities.Promotions;
using Domain.Entities.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
	public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
	{
		public void Configure(EntityTypeBuilder<Promotion> builder)
		{
			builder.HasKey(p => p.Id);
			builder.Property(x=>x.PromotionName).HasMaxLength(255).IsRequired();
			builder.Property(x=>x.DiscountType).HasColumnType("varchar(50)").IsRequired();
			builder.Property(x=>x.DiscountValue).HasColumnType("decimal(10, 2)").IsRequired();
			builder.Property(x=>x.StartDate).IsRequired();
			builder.Property(x=>x.EndDate).IsRequired();
			builder.Property(x=>x.IsActive).IsRequired();
			builder.Property(x=>x.MinimumOrderValue).HasColumnType("decimal(10, 2)");
			builder.Property(x=>x.MaximumDiscountAmount).HasColumnType("decimal(10, 2)");
			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.Description).HasMaxLength(500);
			builder.Property(x => x.Summary).HasMaxLength(255);

			//
		}
	}
}
