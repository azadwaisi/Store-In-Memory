using Domain.Entities.Base;
using Domain.Entities.Promotions;
using Domain.Entities.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Products
{
    public class Product : BaseAuditableEntity, ICommands
    {
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public string PictureUrl { get; set; }
        //
        public Guid ProductTypeId { get; set; }
        public Guid ProductBrandId { get; set; }
        //
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Summary { get; set; }


        public ProductType ProductType { get; set; }
        public ProductBrand ProductBrand { get; set; }

		public ICollection<PromotionProduct> PromotionProducts { get; set; }
        public ICollection<Review> Reviews { get; set; }
	}
}
