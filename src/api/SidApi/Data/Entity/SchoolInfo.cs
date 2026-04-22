using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SidApi.Data.Entities;

[Table("school_info")]
public class SchoolInfo
{
    [Key]
    [Column("sch_info_pk")]
    public int Id { get; set; }

    [Column("sch_name")]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Column("sch_address")]
    [MaxLength(200)]
    public string? Address { get; set; }

    [Column("sch_city")]
    [MaxLength(100)]
    public string? City { get; set; }

    [Column("sch_state")]
    [MaxLength(2)]
    public string? State { get; set; }

    [Column("sch_zip")]
    [MaxLength(10)]
    public string? Zip { get; set; }

    [Column("sch_tax", TypeName = "decimal(9,4)")]
    public decimal? Tax { get; set; }

    [Column("sch_website")]
    [MaxLength(512)]
    public string? Website { get; set; }

    [Column("sch_bandwebsite")]
    [MaxLength(512)]
    public string? BandWebsite { get; set; }

    [Column("sch_start_date")]
    public DateOnly? StartDate { get; set; }

    [Column("sch_end_date")]
    public DateOnly? EndDate { get; set; }

    [Column("sch_sprbrk_start")]
    public DateOnly? SpringBreakStart { get; set; }

    [Column("sch_sprbrk_end")]
    public DateOnly? SpringBreakEnd { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [ForeignKey(nameof(CreatedBy))]
    public User? CreatedByUser { get; set; }

    [Column("previous_edit_by")]
    public int? PreviousEditBy { get; set; }
}