using Domain.Entities.Base;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reviews
{
	public class Review : BaseEntity, ICommands
	{
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }

        public string Description { get; set; }
		public bool IsActive { get; set; }
		public string Summary { get; set; }

        public Product Product { get; set; }
    }
}
