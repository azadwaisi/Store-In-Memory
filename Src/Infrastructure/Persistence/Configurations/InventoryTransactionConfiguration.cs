using Domain.Entities.Inventory;
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
	public class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
	{
		public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x=>x.ProductId).IsRequired();
			builder.Property(x=>x.TransactionType).HasColumnType("varchar(50)").IsRequired();
			builder.Property(x=>x.TransactionDate).IsRequired();
			builder.Property(x=>x.Quantity).IsRequired();
		}
	}
}
