using Domain.Entities.Inventory;
using Domain.Entities.Orders;
using Domain.Entities.Payments;
using Domain.Entities.Products;
using Domain.Entities.Promotions;
using Domain.Entities.Reviews;
using Domain.Entities.Shippers;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,string> //DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; } //=>Set<Product>();
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionProduct> PromotionProducts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());// new ProductTypeConfiguration()
			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<Role>().ToTable("Roles");
			modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
			modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
			modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
			modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
			modelBuilder.Entity<Product>().Property(x => x.ProductTypeId).IsRequired();
            modelBuilder.Entity<Product>().HasQueryFilter(x => x.IsDelete == false);
			modelBuilder.Entity<ProductType>().HasQueryFilter(x => x.IsDelete == false);
			modelBuilder.Entity<ProductBrand>().HasQueryFilter(x => x.IsDelete == false);
		}
    }
}
