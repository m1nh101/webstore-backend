using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Enums;

namespace WebStore.Application.Common.Abstractions;

public interface IWebStoreContext
{
  DbSet<TEntity> GetTable<TEntity>() where TEntity : class;
  Task<int> Save(CancellationToken cancellationToken = default);
}