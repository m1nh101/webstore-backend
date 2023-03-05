using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Application.Common.Abstractions;
using WebStore.Application.Common.Contracts;
using WebStore.Application.Services.Users;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
  private readonly IUserService _service;

  public AuthController(IUserService service)
  {
    _service = service;
  }

  [Route("login")]
  [HttpPost]
  public async Task<IActionResult> Authenticate([FromBody] UserCredential credential)
  {
    var response = await _service.Authenticate(credential);

    if(response.StatusCode != System.Net.HttpStatusCode.OK)
      return Unauthorized(response);
    
    var data = response.Data as UserAuthenticationResponse;
    Response.Cookies.Append("token", data!.Token);

    return Ok(response);
  }

  [Route("register")]
  [HttpPost]
  public async Task<IActionResult> Register([FromBody] UserRegistrationCredential credential)
  {
    var response = await _service.AddNew(credential);

    if(response.StatusCode != System.Net.HttpStatusCode.Created)
      return BadRequest(response);

    var data = response.Data as UserAuthenticationResponse;
    Response.Cookies.Append("token", data!.Token);

    return Ok(response);
  }

  [HttpDelete]
  [Route("logout")]
  [Authorize]
  public IActionResult Logout()
  {
    Response.Cookies.Delete("token");
    return NoContent();
  }

  [HttpPost]
  [Authorize]
  [Route("verify-token/{role}")]
  public IActionResult VerifyToken([FromServices] IExtractDataFromToken extractor,
    [FromRoute] string role)
  {
    var roleVerify = HttpContext.User.IsInRole(role);
    
    if(roleVerify)
    {
      var data = extractor.ExtractInfo();
      return Ok(data);
    }

    return Unauthorized();
  }
}
