using WebStore.Application.Services.Users;

namespace WebStore.Application.Common.Abstractions;

public interface IExtractDataFromToken
{
    UserData ExtractInfo();
}