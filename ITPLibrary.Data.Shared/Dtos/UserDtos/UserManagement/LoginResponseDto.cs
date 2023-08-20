namespace ITPLibrary.Data.Shared.Dtos.UserDtos.UserManagement;

public class LoginResponseDto
{
    [JsonIgnore]
    public int Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string JWT { get; set; } = string.Empty;
    [JsonIgnore]
    public UserLoginStatus UserLoginStatus { get; set; }
}
