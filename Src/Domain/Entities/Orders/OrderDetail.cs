using Domain.Entities.Base;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Orders
{
	public class OrderDetail : BaseEntity, ICommands
	{
		public Guid OrederId { get; set; }
		public Guid ProductId { get; set; }
		public decimal UnitPrice { get; set; }
		public int Quantity { get; set; }
		public decimal Discount { get; set; }

		//
		public Product Product { get; set; }
		public Order Order { get; set; }
		//
		public string Description { get; set; }
		public bool IsActive { get; set; }
		public string Summary { get; set; }
	}
}
