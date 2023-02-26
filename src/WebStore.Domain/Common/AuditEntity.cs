namespace WebStore.Domain.Common;

public abstract class AuditEntity
{
  public int Id { get; private set; }
  public string CreatedBy { get; set; } = null!;
  public string UpdatedBy { get; set; } = null!;
  public DateTime UpdatedAt { get; set; }
  public DateTime CreatedAt { get; set; }
  public bool IsDeleted { get; set; }

  public virtual TDestination To<TDestination>(Func<object, TDestination> converter)
  {
    return converter.Invoke(this);
  }
}