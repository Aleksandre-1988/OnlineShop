using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OnlineShop.Domain.Model;

namespace OnlineShop.DAL
{
    public partial class MainContext : DbContext
    {
        public MainContext()
        {
        }

        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductDescription> ProductDescriptions { get; set; } = null!;
        public virtual DbSet<ProductModel> ProductModels { get; set; } = null!;
        public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; } = null!;
        public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; } = null!;
        public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=AdventureWorksLT2019;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasComment("Street address information for customers.");

                entity.Property(e => e.AddressID).HasComment("Primary key for Address records.");

                entity.Property(e => e.AddressLine1).HasComment("First street address line.");

                entity.Property(e => e.AddressLine2).HasComment("Second street address line.");

                entity.Property(e => e.City).HasComment("Name of the city.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.PostalCode).HasComment("Postal code for the street address.");

                entity.Property(e => e.StateProvince).HasComment("Name of state or province.");

                entity.Property(e => e.rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasComment("Customer information.");

                entity.Property(e => e.CustomerID).HasComment("Primary key for Customer records.");

                entity.Property(e => e.CompanyName).HasComment("The customer's organization.");

                entity.Property(e => e.EmailAddress).HasComment("E-mail address for the person.");

                entity.Property(e => e.FirstName).HasComment("First name of the person.");

                entity.Property(e => e.LastName).HasComment("Last name of the person.");

                entity.Property(e => e.MiddleName).HasComment("Middle name or middle initial of the person.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.NameStyle).HasComment("0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.");

                entity.Property(e => e.PasswordHash).HasComment("Password for the e-mail account.");

                entity.Property(e => e.PasswordSalt).HasComment("Random value concatenated with the password string before the password is hashed.");

                entity.Property(e => e.Phone).HasComment("Phone number associated with the person.");

                entity.Property(e => e.SalesPerson).HasComment("The customer's sales person, an employee of AdventureWorks Cycles.");

                entity.Property(e => e.Suffix).HasComment("Surname suffix. For example, Sr. or Jr.");

                entity.Property(e => e.Title).HasComment("A courtesy title. For example, Mr. or Ms.");

                entity.Property(e => e.rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasKey(e => new { e.CustomerID, e.AddressID })
                    .HasName("PK_CustomerAddress_CustomerID_AddressID");

                entity.HasComment("Cross-reference table mapping customers to their address(es).");

                entity.Property(e => e.CustomerID).HasComment("Primary key. Foreign key to Customer.CustomerID.");

                entity.Property(e => e.AddressID).HasComment("Primary key. Foreign key to Address.AddressID.");

                entity.Property(e => e.AddressType).HasComment("The kind of Address. One of: Archive, Billing, Home, Main Office, Primary, Shipping");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.AddressID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasComment("Products sold or used in the manfacturing of sold products.");

                entity.Property(e => e.ProductID).HasComment("Primary key for Product records.");

                entity.Property(e => e.Color).HasComment("Product color.");

                entity.Property(e => e.DiscontinuedDate).HasComment("Date the product was discontinued.");

                entity.Property(e => e.ListPrice).HasComment("Selling price.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Name).HasComment("Name of the product.");

                entity.Property(e => e.ProductCategoryID).HasComment("Product is a member of this product category. Foreign key to ProductCategory.ProductCategoryID. ");

                entity.Property(e => e.ProductModelID).HasComment("Product is a member of this product model. Foreign key to ProductModel.ProductModelID.");

                entity.Property(e => e.ProductNumber).HasComment("Unique product identification number.");

                entity.Property(e => e.SellEndDate).HasComment("Date the product was no longer available for sale.");

                entity.Property(e => e.SellStartDate).HasComment("Date the product was available for sale.");

                entity.Property(e => e.Size).HasComment("Product size.");

                entity.Property(e => e.StandardCost).HasComment("Standard cost of the product.");

                entity.Property(e => e.ThumbNailPhoto).HasComment("Small image of the product.");

                entity.Property(e => e.ThumbnailPhotoFileName).HasComment("Small image file name.");

                entity.Property(e => e.Weight).HasComment("Product weight.");

                entity.Property(e => e.rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasComment("High-level product categorization.");

                entity.Property(e => e.ProductCategoryID).HasComment("Primary key for ProductCategory records.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.Name).HasComment("Category description.");

                entity.Property(e => e.ParentProductCategoryID).HasComment("Product category identification number of immediate ancestor category. Foreign key to ProductCategory.ProductCategoryID.");

                entity.Property(e => e.rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

            });

            modelBuilder.Entity<ProductDescription>(entity =>
            {
                entity.HasComment("Product descriptions in several languages.");

                entity.Property(e => e.ProductDescriptionID).HasComment("Primary key for ProductDescription records.");

                entity.Property(e => e.Description).HasComment("Description of the product.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.rowguid).HasDefaultValueSql("(newid())");

            });

            modelBuilder.Entity<ProductModelProductDescription>(entity =>
            {
                entity.HasKey(e => new { e.ProductModelID, e.ProductDescriptionID, e.Culture })
                    .HasName("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture");

                entity.HasComment("Cross-reference table mapping product descriptions and the language the description is written in.");

                entity.Property(e => e.ProductModelID).HasComment("Primary key. Foreign key to ProductModel.ProductModelID.");

                entity.Property(e => e.ProductDescriptionID).HasComment("Primary key. Foreign key to ProductDescription.ProductDescriptionID.");

                entity.Property(e => e.Culture)
                    .IsFixedLength()
                    .HasComment("The culture for which the description is written");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.rowguid).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ProductDescription)
                    .WithMany(p => p.ProductModelProductDescriptions)
                    .HasForeignKey(d => d.ProductDescriptionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ProductModel)
                    .WithMany(p => p.ProductModelProductDescriptions)
                    .HasForeignKey(d => d.ProductModelID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SalesOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.SalesOrderID, e.SalesOrderDetailID })
                    .HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

                entity.HasComment("Individual products associated with a specific sales order. See SalesOrderHeader.");

                entity.Property(e => e.SalesOrderID).HasComment("Primary key. Foreign key to SalesOrderHeader.SalesOrderID.");

                entity.Property(e => e.SalesOrderDetailID)
                    .ValueGeneratedOnAdd()
                    .HasComment("Primary key. One incremental unique number per product sold.");

                entity.Property(e => e.LineTotal)
                    .HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))", false)
                    .HasComment("Per product subtotal. Computed as UnitPrice * (1 - UnitPriceDiscount) * OrderQty.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.OrderQty).HasComment("Quantity ordered per product.");

                entity.Property(e => e.ProductID).HasComment("Product sold to customer. Foreign key to Product.ProductID.");

                entity.Property(e => e.UnitPrice).HasComment("Selling price of a single product.");

                entity.Property(e => e.UnitPriceDiscount).HasComment("Discount amount.");

                entity.Property(e => e.rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.SalesOrderDetails)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SalesOrderHeader>(entity =>
            {
                entity.HasKey(e => e.SalesOrderID)
                    .HasName("PK_SalesOrderHeader_SalesOrderID");

                entity.HasComment("General sales order information.");

                entity.Property(e => e.SalesOrderID).HasComment("Primary key.");

                entity.Property(e => e.AccountNumber).HasComment("Financial accounting number reference.");

                entity.Property(e => e.BillToAddressID).HasComment("The ID of the location to send invoices.  Foreign key to the Address table.");

                entity.Property(e => e.Comment).HasComment("Sales representative comments.");

                entity.Property(e => e.CreditCardApprovalCode).HasComment("Approval code provided by the credit card company.");

                entity.Property(e => e.CustomerID).HasComment("Customer identification number. Foreign key to Customer.CustomerID.");

                entity.Property(e => e.DueDate).HasComment("Date the order is due to the customer.");

                entity.Property(e => e.Freight)
                    .HasDefaultValueSql("((0.00))")
                    .HasComment("Shipping cost.");

                entity.Property(e => e.ModifiedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Date and time the record was last updated.");

                entity.Property(e => e.OnlineOrderFlag)
                    .HasDefaultValueSql("((1))")
                    .HasComment("0 = Order placed by sales person. 1 = Order placed online by customer.");

                entity.Property(e => e.OrderDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Dates the sales order was created.");

                entity.Property(e => e.PurchaseOrderNumber).HasComment("Customer purchase order number reference. ");

                entity.Property(e => e.RevisionNumber).HasComment("Incremental number to track changes to the sales order over time.");

                entity.Property(e => e.SalesOrderNumber)
                    .HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID]),N'*** ERROR ***'))", false)
                    .HasComment("Unique sales order identification number.");

                entity.Property(e => e.ShipDate).HasComment("Date the order was shipped to the customer.");

                entity.Property(e => e.ShipMethod).HasComment("Shipping method. Foreign key to ShipMethod.ShipMethodID.");

                entity.Property(e => e.ShipToAddressID).HasComment("The ID of the location to send goods.  Foreign key to the Address table.");

                entity.Property(e => e.Status)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Order current status. 1 = In process; 2 = Approved; 3 = Backordered; 4 = Rejected; 5 = Shipped; 6 = Cancelled");

                entity.Property(e => e.SubTotal)
                    .HasDefaultValueSql("((0.00))")
                    .HasComment("Sales subtotal. Computed as SUM(SalesOrderDetail.LineTotal)for the appropriate SalesOrderID.");

                entity.Property(e => e.TaxAmt)
                    .HasDefaultValueSql("((0.00))")
                    .HasComment("Tax amount.");

                entity.Property(e => e.TotalDue)
                    .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", false)
                    .HasComment("Total due from customer. Computed as Subtotal + TaxAmt + Freight.");

                entity.Property(e => e.rowguid)
                    .HasDefaultValueSql("(newid())")
                    .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

                entity.HasOne(d => d.BillToAddress)
                    .WithMany(p => p.SalesOrderHeaderBillToAddresses)
                    .HasForeignKey(d => d.BillToAddressID)
                    .HasConstraintName("FK_SalesOrderHeader_Address_BillTo_AddressID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesOrderHeaders)
                    .HasForeignKey(d => d.CustomerID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ShipToAddress)
                    .WithMany(p => p.SalesOrderHeaderShipToAddresses)
                    .HasForeignKey(d => d.ShipToAddressID)
                    .HasConstraintName("FK_SalesOrderHeader_Address_ShipTo_AddressID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
