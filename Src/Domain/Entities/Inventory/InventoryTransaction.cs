using Domain.Entities.Base;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Inventory
{
	public class InventoryTransaction : BaseEntity
	{
        public Guid ProductId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Notes { get; set; }

        public Product Product { get; set; }
    }

    public enum TransactionType
    {
		Purchase,
		Sale,
		Adjustment,
		Return
	}
}
