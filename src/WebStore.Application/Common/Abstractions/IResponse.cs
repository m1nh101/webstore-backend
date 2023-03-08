using System.Net;

namespace WebStore.Application.Common.Abstractions;

public interface IResponse
{
    object? Data { get; }
}
