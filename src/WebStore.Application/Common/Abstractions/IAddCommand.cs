namespace WebStore.Application.Common.Abstractions;

public interface IAddCommand<TPayload>
{
  Task<IResponse> AddNew(TPayload payload);
}