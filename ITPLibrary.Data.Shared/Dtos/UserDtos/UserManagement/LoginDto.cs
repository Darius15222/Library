namespace ITPLibrary.Data.Shared.Dtos.UserDtos.UserManagement
{
    public class LoginDto
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        [JsonIgnore]
        public UserLoginStatus UserLoginStatus { get; set; }

        public static explicit operator LoginResponseDto?(LoginDto dto)
        {
            if (dto is null)
                return null;

            return new LoginResponseDto
            {
                UserEmail = dto.UserEmail
            };
        }
        public static explicit operator LoginDto?(RecoverPasswordDto dto)
        {
            if (dto is null)
                return null;

            return new LoginDto
            {
                UserEmail = dto.UserEmail,
                //Password = dto.Password
            };
        }
    }
}
