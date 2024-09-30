using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  public DbSet<Shipping> shippings { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Shipping>(entity =>
    {
      entity.HasKey(u => u.ShippingId);
      entity.Property(u => u.ShippingId).HasDefaultValueSql("uuid_generate_v4()");
      entity.Property(u => u.OrderId).IsRequired();
      entity.Property(u => u.Status).IsRequired();
      entity.HasIndex(u => u.TrackingNumber).IsUnique();
      entity.Property(u=> u.ShippingDetails).HasMaxLength(255);
    });
  }
}