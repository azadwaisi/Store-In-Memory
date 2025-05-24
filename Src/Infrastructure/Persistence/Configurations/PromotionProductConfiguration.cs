using Domain.Entities.Orders;
using Domain.Entities.Promotions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
	public class PromotionProductConfiguration : IEntityTypeConfiguration<PromotionProduct>
	{
		public void Configure(EntityTypeBuilder<PromotionProduct> builder)
		{
			builder.HasKey(x => new { x.ProductId, x.PromotionId });
		}
	}
}
