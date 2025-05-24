using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Category
{
	public class Category : BaseAuditableEntity, ICommands
	{
        public string CategoryName { get; set; }
        public string Description { get; set; }
		public bool IsActive { get; set; }
		public string Summary { get; set; }
	}
}
