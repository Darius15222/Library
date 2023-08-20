namespace ITPLibrary.Data.Shared.Dtos.UserDtos.UserManagement;

public class RecoverPasswordDto
{
    public string UserEmail { get; set; } = string.Empty;
    [JsonIgnore]
    public UserLoginStatus UserLoginStatus { get; set; }
}
