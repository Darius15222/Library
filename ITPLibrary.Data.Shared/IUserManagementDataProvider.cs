namespace ITPLibrary.Data.Shared;

public interface IUserManagementDataProvider
{
    Task Register(User user);

    Task<bool> Login(User user);

    Task<User> GetUser(string email);
}
