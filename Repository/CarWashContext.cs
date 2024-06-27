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

    public virtual DbSet<TbCash> TbCashes { get; set; }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbCompany> TbCompanies { get; set; }

    public virtual DbSet<TbCostofGood> TbCostofGoods { get; set; }

    public virtual DbSet<TbCustomer> TbCustomers { get; set; }

    public virtual DbSet<TbEmployee> TbEmployees { get; set; }

    public virtual DbSet<TbProduct> TbProducts { get; set; }

    public virtual DbSet<TbService> TbServices { get; set; }

    public virtual DbSet<TbVehicleType> TbVehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=CarWash;UID=sa;PWD=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbCash>(entity =>
        {
            entity.HasKey(e => e.IdCash).HasName("PK__tbCash__8BA3F09A449F9744");

            entity.ToTable("tbCash");

            entity.Property(e => e.IdCash).HasColumnName("idCash");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");
            entity.Property(e => e.IdService).HasColumnName("idService");
            entity.Property(e => e.IdVehicleType).HasColumnName("idVehicleType");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Transno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("transno");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.TbCashes)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("fk_tbCash_idCustomer");

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.TbCashes)
                .HasForeignKey(d => d.IdService)
                .HasConstraintName("fk_tbCash_idService");

            entity.HasOne(d => d.IdVehicleTypeNavigation).WithMany(p => p.TbCashes)
                .HasForeignKey(d => d.IdVehicleType)
                .HasConstraintName("fk_tbCash_idVehicleType");
        });

        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__tbCatego__79D361B6EF355897");

            entity.ToTable("tbCategory");

            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .HasColumnName("categoryName");
        });

        modelBuilder.Entity<TbCompany>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbCompany");

            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbCostofGood>(entity =>
        {
            entity.HasKey(e => e.IdCostofGood).HasName("PK__tbCostof__7A1DEA941F4E1AEC");

            entity.ToTable("tbCostofGood");

            entity.Property(e => e.IdCostofGood).HasColumnName("idCostofGood");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.Costname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("costname");
            entity.Property(e => e.Date).HasColumnName("date");
        });

        modelBuilder.Entity<TbCustomer>(entity =>
        {
            entity.HasKey(e => e.IdCustomer).HasName("PK__tbCustom__D0587686BD5659E4");

            entity.ToTable("tbCustomer");

            entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Camo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("camo");
            entity.Property(e => e.Camodel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("camodel");
            entity.Property(e => e.IdVehicleType).HasColumnName("idVehicleType");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Points).HasColumnName("points");

            entity.HasOne(d => d.IdVehicleTypeNavigation).WithMany(p => p.TbCustomers)
                .HasForeignKey(d => d.IdVehicleType)
                .HasConstraintName("fk_idVehicleType");
        });

        modelBuilder.Entity<TbEmployee>(entity =>
        {
            entity.HasKey(e => e.IdEmployee).HasName("PK__tbEmploy__227F26A5E27E0503");

            entity.ToTable("tbEmployee");

            entity.Property(e => e.IdEmployee).HasColumnName("idEmployee");
            entity.Property(e => e.Address)
                .HasColumnType("text")
                .HasColumnName("address");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("salary");
        });

        modelBuilder.Entity<TbProduct>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__tbProduc__5EEC79D1273AE31D");

            entity.ToTable("tbProduct");

            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .HasColumnName("category");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(200)
                .HasColumnName("productName");
            entity.Property(e => e.StockQuantity).HasColumnName("stockQuantity");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.TbProducts)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("fk_tbProduct_idCategory");
        });

        modelBuilder.Entity<TbService>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PK__tbServic__0E3EA45B8FEBB73E");

            entity.ToTable("tbService");

            entity.Property(e => e.IdService).HasColumnName("idService");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<TbVehicleType>(entity =>
        {
            entity.HasKey(e => e.IdVehicleType).HasName("PK__tbVehicl__EDA0D1434A32FF8E");

            entity.ToTable("tbVehicleType");

            entity.Property(e => e.IdVehicleType).HasColumnName("idVehicleType");
            entity.Property(e => e.Class).HasColumnName("class");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
