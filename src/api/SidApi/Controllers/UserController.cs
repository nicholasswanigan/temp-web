using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SidApi.Data;
using SidApi.Data.Entities;
using SidApi.Models;
using SidApi.Security;

namespace SidApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly SidContext _context;
    private static readonly HashSet<string> AllowedTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "Admin","EdRep","Accountant","Repair","Manager"
    };

    public UserController(SidContext context)=>_context=context;

[HttpPost]
public async Task<ActionResult<CreateUserResult>>CreateUser([FromBody] CreateUserRequest request)
    {
        if(string.IsNullOrWhiteSpace(request.Name)||
        string.IsNullOrWhiteSpace(request.Username)||
        string.IsNullOrWhiteSpace(request.Password)||
        string.IsNullOrWhiteSpace(request.Email))
        {
            return BadRequest("Missing Field(s)");
        }

        try
        {
            //Create a new user entity and save it to the database
            var user=new User
            {
                Type=request.UserType,
                Name=request.Name,
                FirstName=request.FirstName,
                LastName=request.LastName,
                Email=request.Email,
                Phone=request.Phone
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            //Create new login entity and save it to the database
            var login=new Login
            {
                UserId=user.Id,
                Username=request.Username,
                Password=PasswordHashing.Hash(request.Password)
            };
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();

            var result=new CreateUserResult
            {
                Status=true,
                StatusCode=201,
                UserId=user.Id,
                Message="New User Created"
                
            };
            return CreatedAtRoute("GetUsers",new{id=user.Id},result);
        }
        catch(Exception ex)
        {
            var result = new CreateUserResult
            {
                Status=false,
                StatusCode=500,
                Message="User Creation Failed: "+ex.Message
            };
            return StatusCode(500, result);
        }

    }
[HttpGet("GetUsers", Name = "GetUsers")]
public async Task<GetUsersResponseModel> GetAllUsers()
    {
        GetUsersResponseModel response = new GetUsersResponseModel();
        try
        {
            var userList= await _context.Users.ToListAsync();
            if (userList.Count != 0)
            {
                response.Status=true;
                response.StatusCode=200;
                response.Users=userList;
            }
            else
            {
                response.Status=false;
                response.Message="Get Users Failed";
                response.StatusCode=400;
            }
        }
        catch(Exception ex)
        {
            response.Status=false;
            response.Message="Get Users Failed: "+ex.Message;
            response.StatusCode=400;
        }
        return response;
    }
}