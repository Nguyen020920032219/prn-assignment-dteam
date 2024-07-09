using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Repository;

public partial class CarWashContext : DbContext
{
    public CarWashContext()
    {
    }

    public CarWashContext(DbContextOptions<CarWashContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

<<<<<<< HEAD
    public virtual DbSet<CostOfGood> CostOfGoods { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<OrderService> OrderServices { get; set; }

=======
    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<OrderService> OrderServices { get; set; }

>>>>>>> develop
    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
<<<<<<< HEAD
        => optionsBuilder.UseSqlServer("Server=(local);Database=CarWash;UID=sa;PWD=12345;TrustServerCertificate=True");
=======
        => optionsBuilder.UseSqlServer("Server=(local);Database=CarWash;UID=sa;PWD=12345;TrustServerCertificate=True;Encrypt=True");
>>>>>>> develop

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA5A6CE00D500");
=======
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA5A629F1AEE7");
>>>>>>> develop

            entity.ToTable("Account");

            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Account__Employe__3E52440B");
        });

        modelBuilder.Entity<CostOfGood>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.CostOfGoodId).HasName("PK__CostOfGo__82B32628F90EC7FB");
=======
            entity.HasKey(e => e.CostOfGoodId).HasName("PK__CostOfGo__82B32628001F3D42");
>>>>>>> develop

            entity.ToTable("CostOfGood");

            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.CostOfGoods)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__CostOfGoo__Produ__5441852A");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D88A385A53");
=======
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8D643D8EB");
>>>>>>> develop

            entity.ToTable("Customer");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F117C3EEBF4");
=======
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F11E787E013");
>>>>>>> develop

            entity.ToTable("Employee");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFED5FA175");

            entity.HasIndex(e => e.TransactionNo, "UQ__Orders__554342D974BFFC57").IsUnique();
=======
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFC00BE4DA");

            entity.HasIndex(e => e.TransactionNo, "UQ__Orders__554342D920EF5CAE").IsUnique();
>>>>>>> develop

            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TransactionNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__48CFD27E");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Orders__Employee__49C3F6B7");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Orders)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Orders__VehicleI__47DBAE45");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.OrderProductsId).HasName("PK__OrderPro__E3B9B33972091B2A");
=======
            entity.HasKey(e => e.OrderProductsId).HasName("PK__OrderPro__E3B9B339ADDFDBD6");
>>>>>>> develop

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderProd__Order__4CA06362");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderProd__Produ__4D94879B");
        });

        modelBuilder.Entity<OrderService>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.OrderServiceId).HasName("PK__OrderSer__F065F7EB90A6744F");
=======
            entity.HasKey(e => e.OrderServiceId).HasName("PK__OrderSer__F065F7EB974513EF");
>>>>>>> develop

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderServ__Order__5070F446");

            entity.HasOne(d => d.Service).WithMany(p => p.OrderServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__OrderServ__Servi__5165187F");
        });

        modelBuilder.Entity<Product>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CDC7F51118");
=======
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD5F2655C4");
>>>>>>> develop

            entity.ToTable("Product");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Service>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB00A72C64640");
=======
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB00A6C52DFAB");
>>>>>>> develop

            entity.ToTable("Service");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicle__476B54920B8AD096");
=======
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicle__476B5492E86CEBE0");
>>>>>>> develop

            entity.ToTable("Vehicle");

            entity.Property(e => e.LicensePlate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Make)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Vehicle__Custome__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
