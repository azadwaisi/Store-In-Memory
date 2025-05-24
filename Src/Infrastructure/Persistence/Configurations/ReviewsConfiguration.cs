using Domain.Entities.Orders;
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
	public class ReviewsConfiguration : IEntityTypeConfiguration<Review>
	{
		public void Configure(EntityTypeBuilder<Review> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x=>x.ProductId).IsRequired();
			builder.Property(x=>x.CustomerId).IsRequired();
			builder.Property(x => x.Rating).IsRequired();
			builder.Property(x => x.ReviewDate).IsRequired();
			builder.HasCheckConstraint("CK_Review_Rating", "Rating BETWEEN 1 AND 5");

			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.Description).HasMaxLength(500);
			builder.Property(x => x.Summary).HasMaxLength(255);

			//
		}
	}
}
