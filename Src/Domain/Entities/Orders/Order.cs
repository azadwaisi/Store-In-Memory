using Domain.Entities.Base;
using Domain.Entities.Payments;
using Domain.Entities.Shippers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Orders
{
	public class Order : BaseEntity, ICommands
	{
		public Guid UserId { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime ShippedDate { get; set; }
		public int ShipVia { get; set; } // Foreign Key to Shippers (optional)
		public decimal Freight { get; set; } //shipping cost
		public string ShipName { get; set; } // shiping receiver name
		public string ShipAddress { get; set; }
		public string ShipCity { get; set; }
		public string ShipRegion { get; set; }
		public string ShipPostalCode { get; set; }
		public string ShipCountry { get; set; }
		public string Description { get; set; }
		public bool IsActive { get; set; }
		public string Summary { get; set; }

		public Shipper Shipper { get; set; }
		public ICollection<OrderDetail> OrderDetails { get; set; }
		public ICollection<Payment> Payments { get; set; }
	}
}
