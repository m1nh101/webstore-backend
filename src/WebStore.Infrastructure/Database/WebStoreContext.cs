using Microsoft.EntityFrameworkCore;
using WebStore.Application.Common.Abstractions;
using WebStore.Domain.Common;
using WebStore.Domain.Enums;

namespace WebStore.Infrastructure.Database;

public class WebStoreContext : DbContext, IWebStoreContext
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
    }

    return base.SaveChangesAsync(cancellationToken);
  }

  #endregion

  public IQueryable<TEntity> GetTable<TEntity>(QueryTracking tracking = QueryTracking.NoTracking)
    where TEntity : class
  {
    return tracking == QueryTracking.Tracking 
      ? Set<TEntity>().AsQueryable()
      : Set<TEntity>().AsNoTracking();
  }

  public Task<int> Save(CancellationToken cancellationToken = default) => SaveChangesAsync(cancellationToken);
}