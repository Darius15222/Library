namespace ITPLibrary.Data.DataProvider.DataProviders;

public class UserManagementDataProvider : IUserManagementDataProvider
{

    private readonly SqlConnector _sqlConnector;


    public UserManagementDataProvider(IConfiguration configuration)
    {
        _sqlConnector = new SqlConnector(configuration[Constants.DatabaseConnectionString]);
    }


    public async Task Register(User user)
    {
        const string sql = @"
                INSERT INTO Users (UserEmail, Password)
                VALUES (@UserEmail, @Password)";

        var parameters = new
        {
            user.UserEmail,
            user.Password
        };

        using (var connection = _sqlConnector.CreateConnection())
        {
            await connection.ExecuteAsync(sql, parameters);
        }
    }

    public async Task<bool> Login(User user)
    {
        const string sql = @"
                SELECT	TOP 1 1
                FROM	Users (NOLOCK)
                WHERE   UserEmail = @UserEmail
                AND     Password = @Password";

        var parameters = new
        {
            user.UserEmail,
            user.Password
        };

        using (var connection = _sqlConnector.CreateConnection())
        {
            //return await connection.QuerySingleOrDefaultAsync<bool?>(sql, parameters) ?? false;

            try
            {
                var result = await connection.QuerySingleOrDefaultAsync<int?>(sql, parameters);
                return result != null && result.Value == 1;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Login failed. Please try again later.", ex);
            }
        }
    }

    public async Task<User> GetUser(string email)
    {
        const string sql = @"SELECT	Id, UserEmail, Password FROM Users WHERE UserEmail = @email";

        
        var parameters = new
        {
            email
        };

        using (var connection = _sqlConnector.CreateConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<User>(sql, parameters);
        }
    }
}

