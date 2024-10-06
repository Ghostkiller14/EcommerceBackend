using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


  public DbSet<Shipping> shippings { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Rating> Ratings { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Order> Orders { get; set; }

  public DbSet<OrderProduct> OrderProducts { get; set; }



  protected override void OnModelCreating(ModelBuilder modelBuilder){

    modelBuilder.Entity<User>(entity => {
      entity.HasKey(e => e.UserId);
      entity.Property(e => e.UserId).HasDefaultValueSql("uuid_generate_v4()");
      entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
      entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
      entity.HasIndex(e => e.Email).IsUnique();
      entity.Property(e => e.Age);
      entity.Property(e => e.Password).IsRequired();
      entity.Property(e => e.Address).HasMaxLength(250);
      entity.Property(e => e.IsAdmin).HasDefaultValue(false);
      entity.Property(e => e.IsBanned).HasDefaultValue(false);
      entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    });

    modelBuilder.Entity<Order>(entity => {
      entity.HasKey(o =>o.OrderId );
      entity.Property(o =>o.OrderId).HasDefaultValueSql("uuid_generate_v4()");
      entity.Property(o => o.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    });

    modelBuilder.Entity<Shipping>(entity => {
      entity.HasKey(u => u.ShippingId);
      entity.Property(u => u.ShippingId).HasDefaultValueSql("uuid_generate_v4()");
      entity.Property(u => u.OrderId).IsRequired();
      entity.Property(u => u.Status).IsRequired();
      entity.HasIndex(u => u.TrackingNumber).IsUnique();
      entity.Property(u=> u.ShippingDetails).HasMaxLength(255);
     });

    modelBuilder.Entity<Product>(entity => {
      entity.HasKey(e => e.ProductId);
      entity.Property(e => e.ProductId).HasDefaultValueSql("uuid_generate_v4()");
      entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
      entity.Property(e => e.Price).IsRequired();
      entity.Property(e => e.Quantity).IsRequired();
      entity.Property(e => e.Description).HasMaxLength(500);
      entity.Property(e => e.ImageIDs).IsRequired();
      entity.Property(e => e.ReturnByDaysAfterOrder).HasDefaultValue(2);
      entity.Property(e => e.IsOnSale).HasDefaultValue(false);
      entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    });

    modelBuilder.Entity<Category>(entity => {
      entity.HasKey(category => category.CategoryId);
      entity.Property(category => category.CategoryId).HasDefaultValueSql("uuid_generate_v4()");
      entity.Property(category => category.Name).IsRequired().HasMaxLength(100);
      entity.HasIndex(category => category.Name).IsUnique();
      entity.Property(category => category.Slug).IsRequired().HasMaxLength(100);
      entity.Property(category => category.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    });

    modelBuilder.Entity<Rating>(entity =>{


      entity.HasKey(r => r.RatingId);
      entity.Property(r => r.RatingId).HasDefaultValueSql("uuid_generate_v4()");
      entity.Property(r => r.FeedBack).HasMaxLength(150);
      entity.Property(r => r.RatingScore);
      entity.Property(r => r.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");


    });



               modelBuilder.Entity<OrderProduct>()
               .HasKey(op => new { op.OrderId, op.ProductId });

               modelBuilder.Entity<OrderProduct>()
               .HasOne(op => op.Order)
               .WithMany(o => o.OrderProducts)
               .HasForeignKey(op => op.OrderId);

               modelBuilder.Entity<OrderProduct>()
               .HasOne(op => op.Product)
               .WithMany(p => p.OrderProducts)
               .HasForeignKey(op => op.ProductId);


                modelBuilder.Entity<Category>()
                  .HasMany(c => c.Products)
                  .WithOne(p => p.Category)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);


  }
}
