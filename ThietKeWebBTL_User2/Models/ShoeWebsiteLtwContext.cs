using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ThietKeWebBTL_User2.Models;

public partial class ShoeWebsiteLtwContext : DbContext
{
    public ShoeWebsiteLtwContext()
    {
    }

    public ShoeWebsiteLtwContext(DbContextOptions<ShoeWebsiteLtwContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<CreditCard> CreditCards { get; set; }

    public virtual DbSet<CustomerMessage> CustomerMessages { get; set; }

    public virtual DbSet<DeliveryCompany> DeliveryCompanies { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<GenderCategory> GenderCategories { get; set; }

    public virtual DbSet<GenderProduct> GenderProducts { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<ImageProductDetail> ImageProductDetails { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductDetail> ProductDetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Slide> Slides { get; set; }

    public virtual DbSet<TbOrder> TbOrders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(" Data Source=LAPTOP-LG0JK76J\\MSSQLSERVER01;Initial Catalog=ShoeWebsiteLTW;Integrated Security=True;MultipleActiveResultSets=true;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Account__A9D10535404D2FD0");

            entity.ToTable("Account");

            entity.Property(e => e.Email).HasMaxLength(400);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.Image).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.Password).HasMaxLength(400);
            entity.Property(e => e.PhoneNumber).HasMaxLength(400);
            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("RoleID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Gender).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK__Account__GenderI__17036CC0");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Account__RoleID__797309D9");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__091C2A1B5A24BE12");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.Note).HasMaxLength(400);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK__Address__Email__7A672E12");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2BEAF1F0C5");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedBy).HasMaxLength(400);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedBy).HasMaxLength(400);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.SeoTitle).HasMaxLength(400);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.ColorId).HasName("PK__Color__8DA7676D6AF31B9D");

            entity.ToTable("Color");

            entity.Property(e => e.ColorId).HasColumnName("ColorID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<CreditCard>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__CreditCa__55FECD8EDACCFFD6");

            entity.ToTable("CreditCard");

            entity.Property(e => e.CardId)
                .HasMaxLength(400)
                .HasColumnName("CardID");
            entity.Property(e => e.CardNumber).HasMaxLength(400);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Cvv)
                .HasMaxLength(400)
                .HasColumnName("CVV");
            entity.Property(e => e.Email).HasMaxLength(400);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.NameOnCard).HasMaxLength(400);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.CreditCards)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK__CreditCar__Email__7B5B524B");
        });

        modelBuilder.Entity<CustomerMessage>(entity =>
        {
            entity.HasKey(e => e.CustomerMessageId).HasName("PK__Customer__AA5CD7C567D60E91");

            entity.ToTable("CustomerMessage");

            entity.Property(e => e.CustomerMessageId).HasColumnName("CustomerMessageID");
            entity.Property(e => e.ContactEmail).HasMaxLength(400);
            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(400);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.CustomerMessages)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK__CustomerM__Email__7C4F7684");
        });

        modelBuilder.Entity<DeliveryCompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3214EC27ED4DC1A8");

            entity.ToTable("DeliveryCompany");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy).HasMaxLength(400);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(400);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__Gender__4E24E81792F81B41");

            entity.ToTable("Gender");

            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<GenderCategory>(entity =>
        {
            entity.HasKey(e => new { e.GenderId, e.CategoryId }).HasName("PK__Gender_C__EFB47BB55154B9B8");

            entity.ToTable("Gender_Category");

            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.GenderCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gender_Ca__Categ__7D439ABD");

            entity.HasOne(d => d.Gender).WithMany(p => p.GenderCategories)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gender_Ca__Gende__7E37BEF6");
        });

        modelBuilder.Entity<GenderProduct>(entity =>
        {
            entity.HasKey(e => new { e.GenderId, e.ProductId }).HasName("PK__Gender_P__85642479833104A3");

            entity.ToTable("Gender_Product");

            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Gender).WithMany(p => p.GenderProducts)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gender_Pr__Gende__7F2BE32F");

            entity.HasOne(d => d.Product).WithMany(p => p.GenderProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Gender_Pr__Produ__00200768");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Image__7516F4EC56469EC4");

            entity.ToTable("Image");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.FileName).HasMaxLength(400);
        });

        modelBuilder.Entity<ImageProductDetail>(entity =>
        {
            entity.HasKey(e => new { e.ImageId, e.ProductDetailId }).HasName("PK__Image_Pr__26DE2985F14CE365");

            entity.ToTable("Image_ProductDetail");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ProductDetailId).HasColumnName("ProductDetailID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Image).WithMany(p => p.ImageProductDetails)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Image_Pro__Image__01142BA1");

            entity.HasOne(d => d.ProductDetail).WithMany(p => p.ImageProductDetails)
                .HasForeignKey(d => d.ProductDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Image_Pro__Produ__02084FDA");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductDetailId }).HasName("PK__OrderDet__905886C6FCCE73F4");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductDetailId).HasColumnName("ProductDetailID");
            entity.Property(e => e.UnitSellingPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__02FC7413");

            entity.HasOne(d => d.ProductDetail).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Produ__03F0984C");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A58BA13215E");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6EDF190A0F4");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CreatedBy).HasMaxLength(400);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.ModifiedBy).HasMaxLength(400);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(400);
            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
            entity.Property(e => e.SeoTitle).HasMaxLength(400);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => new { e.CategoryId, e.ProductId }).HasName("PK__Product___D249F6453BE0D357");

            entity.ToTable("Product_Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product_C__Categ__04E4BC85");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product_C__Produ__05D8E0BE");
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.ProductDetailId).HasName("PK__ProductD__3C8DD694FF9E933B");

            entity.ToTable("ProductDetail");

            entity.Property(e => e.ProductDetailId).HasColumnName("ProductDetailID");
            entity.Property(e => e.ColorId).HasColumnName("ColorID");
            entity.Property(e => e.CreatedBy).HasMaxLength(400);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(400);
            entity.Property(e => e.ModifiedBy).HasMaxLength(400);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SizeId).HasColumnName("SizeID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.UnitSellingPrice).HasColumnType("money");
            entity.Property(e => e.UnitSupplierPrice).HasColumnType("money");

            entity.HasOne(d => d.Color).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.ColorId)
                .HasConstraintName("FK__ProductDe__Color__06CD04F7");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductDe__Produ__07C12930");

            entity.HasOne(d => d.Size).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__ProductDe__SizeI__08B54D69");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A21DEBA4C");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__Size__83BD095A4E54F6FB");

            entity.ToTable("Size");

            entity.Property(e => e.SizeId).HasColumnName("SizeID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<Slide>(entity =>
        {
            entity.HasKey(e => e.SlideId).HasName("PK__Slide__9E7CB670013FEB28");

            entity.ToTable("Slide");

            entity.Property(e => e.SlideId).HasColumnName("SlideID");
            entity.Property(e => e.CreatedBy).HasMaxLength(400);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");
            entity.Property(e => e.Image).HasMaxLength(400);
            entity.Property(e => e.Link).HasMaxLength(400);
            entity.Property(e => e.ModifiedBy).HasMaxLength(400);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.Text1).HasMaxLength(400);
            entity.Property(e => e.Text2).HasMaxLength(400);
        });

        modelBuilder.Entity<TbOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__tbOrder__C3905BAF409C9B15");

            entity.ToTable("tbOrder");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.DeliveryFee).HasColumnType("money");
            entity.Property(e => e.Email).HasMaxLength(400);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("PaymentID");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.TbOrders)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK__tbOrder__Email__09A971A2");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.TbOrders)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("FK__tbOrder__ID__0A9D95DB");

            entity.HasOne(d => d.Payment).WithMany(p => p.TbOrders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__tbOrder__Payment__0B91BA14");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
