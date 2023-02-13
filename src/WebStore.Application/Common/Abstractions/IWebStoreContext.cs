using WebStore.Domain.Enums;

namespace WebStore.Application.Common.Abstractions;

public interface IWebStoreContext
{
  IQueryable<TEntity> GetTable<TEntity>(QueryTracking tracking = QueryTracking.NoTracking) where TEntity : class;
  Task<int> Save(CancellationToken cancellationToken = default);
}