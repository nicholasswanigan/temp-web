using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SidApi.Data;
using SidApi.Data.Entities;
using SidApi.Models;

namespace SidApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchoolController : ControllerBase
{
    private readonly SidContext _context;

    public SchoolController(SidContext context) => _context = context;

    [HttpPost]
    public async Task<ActionResult<CreateSchoolResult>> CreateSchool([FromBody] CreateSchoolRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new CreateSchoolResult
            {
                Status = false,
                StatusCode = 400,
                Message = "School name is required."
            });
        }

        // Verify the creating user exists
        var creatingUser = await _context.Users.FindAsync(request.CreatedBy);
        if (creatingUser is null)
        {
            return BadRequest(new CreateSchoolResult
            {
                Status = false,
                StatusCode = 400,
                Message = $"User with ID {request.CreatedBy} not found."
            });
        }

        try
        {
            var schoolInfo = new SchoolInfo
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Zip = request.Zip,
                Tax = request.Tax,
                Website = request.Website,
                BandWebsite = request.BandWebsite,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                SpringBreakStart = request.SpringBreakStart,
                SpringBreakEnd = request.SpringBreakEnd,
                CreatedBy = request.CreatedBy
            };

            _context.SchoolInfos.Add(schoolInfo);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetSchools", new { id = schoolInfo.Id }, new CreateSchoolResult
            {
                Status = true,
                StatusCode = 201,
                SchoolInfoId = schoolInfo.Id,
                Message = "School created successfully."
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new CreateSchoolResult
            {
                Status = false,
                StatusCode = 500,
                Message = "School creation failed: " + ex.Message
            });
        }
    }

    [HttpGet(Name = "GetSchools")]
    public async Task<ActionResult> GetAllSchools()
    {
        try
        {
            var schools = await _context.SchoolInfos.ToListAsync();
            return Ok(new
            {
                Status = true,
                StatusCode = 200,
                Schools = schools
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Status = false,
                StatusCode = 500,
                Message = "Failed to retrieve schools: " + ex.Message
            });
        }
    }
}