namespace SidApi.Models;
public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public UserInfo User { get; set; } = new();
}