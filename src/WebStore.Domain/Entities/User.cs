using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities;

public class User : IdentityUser
{
  public string FullName { get; set; } = string.Empty;
  public string Avatar { get; set; } = string.Empty;
  public bool IsDeleted { get; set; }

  public virtual IList<Order> Orders { get; private set; } = new List<Order>();
  public virtual IList<Bill> Bills { get; private set; } = new List<Bill>();
}
