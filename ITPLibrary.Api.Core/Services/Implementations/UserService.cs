using ITPLibrary.Data.Shared.Dtos.UserDtos;

namespace ITPLibrary.Api.Core.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserManagementDataProvider _userManagementDataProvider;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public UserService(IUserManagementDataProvider userManagementDataProvider, IConfiguration configuration, IMapper mapper)
    {
        _userManagementDataProvider = userManagementDataProvider;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<bool> Register(UserDto userDto)
    {
        var user = await _userManagementDataProvider.GetUser(userDto.UserEmail);

        if (user == null)
        {
            user = _mapper.Map(userDto, new User());
            await _userManagementDataProvider.Register(user);

            return true;
        }

        return false;
    }

    public async Task<string?> Login(UserDto userDto)
    {
        var user = _mapper.Map(userDto, new User());

        return await _userManagementDataProvider.Login(user) ? GetAccessToken(user.UserEmail) : null;
    }

    private string GetAccessToken(string email)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: DateTime.Now.AddHours(8),
            claims: new List<Claim> { new Claim(ClaimTypes.Name, email) },
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> RecoverPassword(string email)
    {
        var user = await _userManagementDataProvider.GetUser(email);

        if (user != null)
        {
            await SendEmailForPasswordRecovery(user);

            return true;
        }

        return false;
    }

    private async Task SendEmailForPasswordRecovery(User user)
    {
        var client = new SmtpClient(_configuration[Constants.MailboxHost], Convert.ToInt32(_configuration[Constants.MailboxPort]))
        {
            Credentials = new NetworkCredential(_configuration[Constants.MailboxEmail], _configuration[Constants.MailboxAppPassword]),
            EnableSsl = true
        };

        await client.SendMailAsync(_configuration[Constants.MailboxEmail], user.UserEmail, "Password recovery for your account", $"Your password is {user.Password}");
    }
}
