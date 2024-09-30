using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


  public DbSet<User> Users { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Rating> Ratings { get; set; }


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


    modelBuilder.Entity<Rating>(entity =>{




      entity.HasKey(r => r.RatingId);
      entity.Property(r => r.RatingId).HasDefaultValueSql("uuid_generate_v4()");
      entity.Property(r => r.FeedBack).HasMaxLength(150);
      entity.Property(r => r.RatingScore);
      entity.Property(r => r.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");



    });






  }







}
