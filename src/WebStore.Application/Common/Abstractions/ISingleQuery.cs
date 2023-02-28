using System.Linq.Expressions;

namespace WebStore.Application.Common.Abstractions;

public interface ISingleQuery<TEntity, TResponse>
{
  Task<TResponse> Find(Expression<Func<TEntity, bool>> criteria);
}
