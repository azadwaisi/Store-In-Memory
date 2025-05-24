using Domain.Entities.Base;
using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Promotions
{
	public class PromotionProduct
	{
        public Guid PromotionId { get; set; }
        public Guid ProductId { get; set; }

        public Product Product { get; set; }
        public Promotion Promotion { get; set; }
    }
}
