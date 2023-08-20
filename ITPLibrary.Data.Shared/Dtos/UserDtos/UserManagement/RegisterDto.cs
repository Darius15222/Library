namespace ITPLibrary.Data.Shared.Dtos.UserDtos.UserManagement;

public class RegisterDto
{
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public string UserEmail { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string ConfirmedPassword { get; set; } = string.Empty;
    [JsonIgnore]
    public UserLoginStatus UserLoginStatus { get; set; }
}
