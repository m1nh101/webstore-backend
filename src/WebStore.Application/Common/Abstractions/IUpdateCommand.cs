namespace WebStore.Application.Common.Abstractions;

public interface IUpdateCommand<TPayload>
{
  Task<IResponse> Update(TPayload payload);
}
