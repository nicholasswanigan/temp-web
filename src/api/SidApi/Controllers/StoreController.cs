using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SidApi.Data;
using SidApi.Data.Entities;
using SidApi.Models;
 
namespace SidApi.Controllers;
 
[ApiController]
[Route("api/[controller]")]
public class StoreController : ControllerBase
{
    private readonly SidContext _context;
 
    public StoreController(SidContext context) => _context = context;
 
    [HttpPost]
    public async Task<ActionResult<CreateStoreResult>> CreateStore([FromBody] CreateStoreRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new CreateStoreResult
            {
                Status = false,
                StatusCode = 400,
                Message = "Store name is required."
            });
        }
 
        // Verify the creating user exists
        var creatingUser = await _context.Users.FindAsync(request.CreatedBy);
        if (creatingUser is null)
        {
            return BadRequest(new CreateStoreResult
            {
                Status = false,
                StatusCode = 400,
                Message = $"User with ID {request.CreatedBy} not found."
            });
        }
 
        try
        {
            var store = new Store
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Zip = request.Zip,
                Phone = request.Phone,
                CreatedBy = request.CreatedBy
            };
 
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
 
            return CreatedAtRoute("GetStores", new { id = store.Id }, new CreateStoreResult
            {
                Status = true,
                StatusCode = 201,
                StoreId = store.Id,
                Message = "Store created successfully."
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new CreateStoreResult
            {
                Status = false,
                StatusCode = 500,
                Message = "Store creation failed: " + ex.Message
            });
        }
    }
 
    [HttpGet(Name = "GetStores")]
    public async Task<ActionResult> GetAllStores()
    {
        try
        {
            var stores = await _context.Stores.ToListAsync();
            return Ok(new
            {
                Status = true,
                StatusCode = 200,
                Stores = stores
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Status = false,
                StatusCode = 500,
                Message = "Failed to retrieve stores: " + ex.Message
            });
        }
    }
}
