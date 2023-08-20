namespace ITPLibrary.Data.Shared.Entities;

public class User
{
    public int Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
