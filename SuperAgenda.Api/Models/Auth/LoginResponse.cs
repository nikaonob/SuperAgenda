namespace SuperAgenda.Api.Models.Auth;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
