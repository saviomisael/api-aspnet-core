namespace GamesWebApi.DTO;

public class LoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}