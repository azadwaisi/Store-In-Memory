using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Promotions
{
	public class Promotion : BaseAuditableEntity, ICommands
	{
        public string PromotionName { get; set; }
		public DiscountType DiscountType { get; set; }
		public decimal DiscountValue { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public decimal? MinimumOrderValue { get; set; }
		public decimal? MaximumDiscountAmount { get; set; }

		public string Description { get; set; }
		public bool IsActive { get; set; }
		public string Summary { get; set; }

		public ICollection<PromotionProduct> PromotionProducts { get; set; }
	}

	// Define an enum for DiscountType
	public enum DiscountType
	{
		Percentage,
		FixedAmount
	}
}
