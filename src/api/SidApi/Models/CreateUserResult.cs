namespace SidApi.Models;
public class CreateUserResult
{
    public bool Status { get; set; }
    public int StatusCode { get; set; }
    public int UserId { get; set; }
    public string Message { get; set; }
}