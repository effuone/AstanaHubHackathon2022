using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using QMarket.Api.DbModels;

namespace QMarket.Api.Data
{
    public partial class QMarketDbContext : DbContext
    {
        public QMarketDbContext()
        {
        }

        public QMarketDbContext(DbContextOptions<QMarketDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Courier> Couriers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderedLocation> OrderedLocations { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:QMarketDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories", "production");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<Courier>(entity =>
            {
                entity.ToTable("couriers", "sales");

                entity.Property(e => e.CourierId).HasColumnName("courier_id");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("company_name");

                entity.Property(e => e.ImageName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image_name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers", "sales");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.ImageName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("locations", "map");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("location_name");

                entity.Property(e => e.Rsid).HasColumnName("rsid");

                entity.Property(e => e.XCord)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("x_cord");

                entity.Property(e => e.YCord)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("y_cord");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.ToTable("offers", "sales");

                entity.Property(e => e.OfferId).HasColumnName("offer_id");

                entity.Property(e => e.CourierId).HasColumnName("courier_id");

                entity.Property(e => e.ExpectedDeliveryDate)
                    .HasColumnType("date")
                    .HasColumnName("expected_delivery_date");

                entity.Property(e => e.ExpectedDeliveryPrice)
                    .HasColumnType("money")
                    .HasColumnName("expected_delivery_price");

                entity.Property(e => e.OfferDate)
                    .HasColumnType("date")
                    .HasColumnName("offer_date");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.HasOne(d => d.Courier)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.CourierId)
                    .HasConstraintName("FK__offers__courier___3F466844");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__offers__order_id__3E52440B");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders", "sales");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.ExpectedDate)
                    .HasColumnType("date")
                    .HasColumnName("expected_date");

                entity.Property(e => e.ExpectedDeliveryPrice)
                    .HasColumnType("money")
                    .HasColumnName("expected_delivery_price");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("order_date");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.ShippedDate)
                    .HasColumnType("date")
                    .HasColumnName("shipped_date");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__orders__customer__30F848ED");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__orders__store_id__31EC6D26");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ItemId })
                    .HasName("PK__order_it__837942D451F4CA8A");

                entity.ToTable("order_items", "sales");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("discount");

                entity.Property(e => e.ListPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("list_price");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__order_ite__order__3A81B327");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__order_ite__produ__3B75D760");
            });

            modelBuilder.Entity<OrderedLocation>(entity =>
            {
                entity.ToTable("ordered_locations", "map");

                entity.Property(e => e.OrderedLocationId).HasColumnName("ordered_location_id");

                entity.Property(e => e.LocationIdFrom).HasColumnName("location_id_from");

                entity.Property(e => e.LocationIdTo).HasColumnName("location_id_to");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.HasOne(d => d.LocationIdFromNavigation)
                    .WithMany(p => p.OrderedLocationLocationIdFromNavigations)
                    .HasForeignKey(d => d.LocationIdFrom)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ordered_l__locat__34C8D9D1");

                entity.HasOne(d => d.LocationIdToNavigation)
                    .WithMany(p => p.OrderedLocationLocationIdToNavigations)
                    .HasForeignKey(d => d.LocationIdTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ordered_l__locat__35BCFE0A");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderedLocations)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ordered_l__order__36B12243");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products", "production");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image_name");

                entity.Property(e => e.ListPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("list_price");

                entity.Property(e => e.ModelYear).HasColumnName("model_year");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("product_name");

                entity.Property(e => e.Weigth)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("weigth");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__products__catego__276EDEB3");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ProductId })
                    .HasName("PK__stocks__E68284D3BFAEAF35");

                entity.ToTable("stocks", "production");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__stocks__product___4316F928");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__stocks__store_id__4222D4EF");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("stores", "sales");

                entity.Property(e => e.StoreId).HasColumnName("store_id");

                entity.Property(e => e.ImageName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image_name");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("store_name");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stores_To_Locations");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
