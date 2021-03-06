﻿using DevCommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevCommerce.DataAccess.Concrete.EntityFramework
{
    public partial class DevCommerceContext : DbContext
    {
        public DevCommerceContext(DbContextOptions options)
            : base(options)
        {

        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(table => new {
                table.OrderId,
                table.ProductId
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
