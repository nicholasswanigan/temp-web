using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace SidApi.Data.Entities;
 
[Table("store")]
public class Store
{
    [Key]
    [Column("store_pk")]
    public int Id { get; set; }
 
    [Column("store_name")]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
 
    [Column("store_address")]
    [MaxLength(200)]
    public string? Address { get; set; }
 
    [Column("store_city")]
    [MaxLength(100)]
    public string? City { get; set; }
 
    [Column("store_state")]
    [MaxLength(2)]
    public string? State { get; set; }
 
    [Column("store_zip")]
    [MaxLength(10)]
    public string? Zip { get; set; }
 
    [Column("store_phone")]
    [MaxLength(25)]
    public string? Phone { get; set; }
 
    [Column("created_by")]
    public int CreatedBy { get; set; }
 
    [ForeignKey(nameof(CreatedBy))]
    public User? CreatedByUser { get; set; }
 
    [Column("previous_edit_by")]
    public int? PreviousEditBy { get; set; }
}