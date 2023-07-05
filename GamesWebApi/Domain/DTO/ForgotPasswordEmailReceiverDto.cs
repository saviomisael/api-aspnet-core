namespace Domain.DTO;

public class ForgotPasswordEmailReceiverDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string RandomPassword { get; set; }
}