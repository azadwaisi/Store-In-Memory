using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
	public class BaseAuditableEntity : BaseEntity
	{
		public DateTime? CreatedDate { get; set; }
		public Guid? CreateUserId { get; set; }
		public DateTime? ModifiedDate { get; set; }
		public Guid? ModifiedUserId { get; set; }
	}
}
