namespace WebStore.Domain.Common;

public abstract class AuditEntity
{
  public string CreatedBy { get; set; } = null!;
  public string UpdatedBy { get; set; } = null!;
  public DateTime UpdatedAt { get; set; }
  public DateTime CreatedAt { get; set; }
}
