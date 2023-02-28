using System.Net;
using WebStore.Application.Common.Abstractions;

namespace WebStore.Application.Common.Helpers;

public class ResponseFactory : IResponse
{
  public HttpStatusCode StatusCode { get; private set; }
  public object? Data { get; private set; }
  public object? Errors { get; private set; }

  public static ResponseFactory Create(HttpStatusCode statusCode, object? data = null, object? errors = null)
    
  {
    return new()
    {
      StatusCode = statusCode,
      Data = data,
      Errors = errors
    };
  }
}