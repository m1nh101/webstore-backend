namespace WebStore.Application.Common.Abstractions;

public interface IDeleteCommand<TPayload>
{
  Task<IResponse> Delete(TPayload payload);
}