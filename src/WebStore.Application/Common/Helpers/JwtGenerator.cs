using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebStore.Application.Common.Abstractions;
using WebStore.Domain.Entities;

namespace WebStore.Application.Common.Helpers;

public class JwtGenerator : IJwtGenerator
{
  private readonly IConfiguration _configuration;

  public JwtGenerator(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  private Claim GetUsernameClaim(User user) => new(ClaimTypes.Name, user.UserName!);
  private Claim GetEmailClaim(User user) => new(ClaimTypes.Email, user.Email!);
  private Claim GetNameClaim(User user) => new(ClaimTypes.GivenName, user.FullName);
  private Claim GetUserIdClaim(User user) => new(ClaimTypes.NameIdentifier, user.Id);
  private IEnumerable<Claim> GetRoleClaim(IEnumerable<string> roles)
  {
    return roles.Select(e => new Claim(ClaimTypes.Role, e))
      .AsEnumerable();
  }

  private ClaimsIdentity GetClaimsIdentity(User user, IEnumerable<string> roles)
  {
    var claims = new List<Claim>()
    {
      GetEmailClaim(user),
      GetUserIdClaim(user),
      GetUsernameClaim(user),
      GetNameClaim(user)
    }.Union(GetRoleClaim(roles));

    return new ClaimsIdentity(claims);
  }

  public string GenerateToken(User user, IEnumerable<string> roles)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var secretTokenKey = Encoding.UTF8.GetBytes(_configuration["JWT_SECRET"]!);
    var tokenEffectiveDays = Convert.ToInt32(_configuration["JWT_EFFECTIVE_DAYS"]);
    var symmetricKey = new SymmetricSecurityKey(secretTokenKey);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = GetClaimsIdentity(user, roles),
      Expires = DateTime.UtcNow.AddDays(tokenEffectiveDays),
      SigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    
    return tokenHandler.WriteToken(token);
  }
}