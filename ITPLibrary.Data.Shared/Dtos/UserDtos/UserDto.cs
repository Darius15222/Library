namespace ITPLibrary.Data.Shared.Dtos.UserDtos;

public class UserDto
{
    [JsonIgnore]
    public int Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
