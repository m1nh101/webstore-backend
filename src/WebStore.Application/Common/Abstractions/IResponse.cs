using System.Net;

namespace WebStore.Application.Common.Abstractions;

public interface IResponse
{
  HttpStatusCode StatusCode { get; }
  object? Data { get; }
}
