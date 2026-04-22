namespace SidApi.Models;

public class CreateSchoolRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zip { get; set; }
    public decimal? Tax { get; set; }
    public string? Website { get; set; }
    public string? BandWebsite { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public DateOnly? SpringBreakStart { get; set; }
    public DateOnly? SpringBreakEnd { get; set; }
    public int CreatedBy { get; set; }
}