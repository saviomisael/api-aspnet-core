namespace GamesWebApi.DTO;

public class CreateAccountDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
}