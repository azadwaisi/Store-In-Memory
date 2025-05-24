using Domain.Entities.Promotions;
using Domain.Entities.Shippers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
	public class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
	{
		public void Configure(EntityTypeBuilder<Shipper> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.ShipperName).HasMaxLength(255).IsRequired();
			builder.Property(x => x.Phone).HasMaxLength(50);
			builder.Property(x => x.IsActive).HasDefaultValue(true);
			builder.Property(x => x.Description).HasMaxLength(500);
			builder.Property(x => x.Summary).HasMaxLength(255);

		}
	}
}
