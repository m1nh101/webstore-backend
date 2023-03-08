using System.Net;
using WebStore.Application.Common.Abstractions;

namespace WebStore.Application.Common.Helpers;

public class ResponseFactory : IResponse
{
  public object? Data { get; private set; }
  public object? Errors { get; private set; }

  public static ResponseFactory Create(object? data = null, object? errors = null)
    
  {
    return new()
    {
      Data = data,
      Errors = errors
    };
  }
}