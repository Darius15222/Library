using ITPLibrary.Data.Shared.Dtos.UserDtos;

namespace ITPLibrary.Api.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<string?> Login(UserDto userDto);
        Task<bool> Register(UserDto userDto);
        Task<bool> RecoverPassword(string email);
    }
}
