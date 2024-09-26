using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



public class AppDbContext: DbContext{

      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

      public DbSet<User> Users { get; set;}



 protected override void OnModelCreating(ModelBuilder modelBuilder){

  modelBuilder.Entity<User>(entity => {

    entity.HasKey(e=>e.UserId);
    entity.Property(e => e.UserId).HasDefaultValueSql("uuid_generate_v4()");
    entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
    entity.Property(e =>e.Email).IsRequired().HasMaxLength(100);
    entity.HasIndex(e => e.Email).IsUnique();
    entity.Property(e =>e.Age);
    entity.Property(e => e.Password).IsRequired();
    entity.Property(e => e.Address).HasMaxLength(250);
    entity.Property(e => e.IsAdmin).HasDefaultValue(false);
    entity.Property(e => e.IsBanned).HasDefaultValue(false);
    entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

  });





 }


}



