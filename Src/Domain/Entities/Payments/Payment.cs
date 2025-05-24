using Domain.Entities.Base;
using Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Payments
{
	public class Payment : BaseEntity, ICommands
	{
        public Guid OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymenMethod PaymentMethod { get; set; }
		public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public PaymenStatus Status { get; set; }
        public string Description { get; set; }
		public bool IsActive { get; set; }
		public string Summary { get; set; }

        public Order Order { get; set; }
    }
	public enum PaymenMethod
	{
		CreditCard,
		Cash,
		PayPal
	}
	public enum PaymenStatus
	{
		Pending,
		Completed,
		Failed
	}
}
