using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SidApi.Models;
using SidApi.Data;
using SidApi.Security;
 
namespace SidApi.Controllers;
 
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SidContext _context;
    public AuthController(SidContext context) => _context = context;
 
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] Models.LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "Username and password are required." });
        }
 
        var login = await _context.Logins
            .Include(l => l.User)
            .FirstOrDefaultAsync(l => l.Username == request.Username);
 
        if (login is null || login.User is null)
            return Unauthorized(new { message = "Invalid username or password." });
 
        if (!PasswordHashing.Verify(request.Password, login.Password))
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }
 
        // Validate user's active flag
        if (!login.User.Active)
        {
            return Unauthorized(new { message = "Account is inactive. Please contact your administrator." });
        }
 
        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        var user = login.User;
 
        return Ok(new LoginResponse
        {
            Token = token,
            ExpiresAtUtc = DateTime.UtcNow.AddHours(1),
            User = new UserInfo
            {
                UserId = user.Id,
                UserType = user.Type,
                Name = user.Name,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone
            }
        });
    }
}


