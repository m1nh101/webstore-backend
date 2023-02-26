using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Application.Common.Abstractions;
using WebStore.Domain.Common;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Database.Configs;

namespace WebStore.Infrastructure.Database;

public class WebStoreContext : IdentityDbContext<User> , IWebStoreContext
{
  private readonly IUserSession _session;

  public WebStoreContext(DbContextOptions<WebStoreContext> options, IUserSession session)
    :base(options)
  {
    _session = session;
  }

  #region Override DbContext

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    foreach(var entry in ChangeTracker.Entries<AuditEntity>())
    {
      if(entry.State == EntityState.Added)
      {
        entry.Entity.CreatedAt = DateTime.Now;
        entry.Entity.CreatedBy = _session.UserId;
        entry.Entity.UpdatedAt = entry.Entity.CreatedAt;
        entry.Entity.UpdatedBy = _session.UserId;
      }

      if(entry.State == EntityState.Modified)
      {
        entry.Entity.UpdatedAt = DateTime.Now;
        entry.Entity.UpdatedBy = _session.UserId;
      }

      if(entry.State == EntityState.Deleted)
        entry.Entity.IsDeleted = true;
    }

    return base.SaveChangesAsync(cancellationToken);
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
    modelBuilder.ApplyConfiguration(new SaleEntityConfiguration());
    modelBuilder.ApplyConfiguration(new ProductVariantEntityConfiguration());
    modelBuilder.ApplyConfiguration(new ImageEntityConfiguration());
    modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
    modelBuilder.ApplyConfiguration(new OrderDetailEntityConfiguration());
    modelBuilder.ApplyConfiguration(new BillEntityConfiguration());
    modelBuilder.ApplyConfiguration(new BillDetailEntityConfiguration());

    //rename asp identity table name
    modelBuilder.Entity<User>().ToTable("Users").HasData(SeedData.CreateAdminUser());
    modelBuilder.Entity<IdentityRole>().ToTable("Roles").HasData(SeedData.CreateRoles());
    modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
    modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
    modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
    modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
    modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
  }

  #endregion

  public virtual DbSet<TEntity> GetTable<TEntity>()
    where TEntity : class
  {
    return Set<TEntity>();
  }

  public Task<int> Save(CancellationToken cancellationToken = default) => SaveChangesAsync(cancellationToken);
}