using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Shippers
{
	public class Shipper : BaseAuditableEntity, ICommands
	{
        public string ShipperName { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
		public bool IsActive { get; set; }
		public string Summary { get; set; }
	}
}
