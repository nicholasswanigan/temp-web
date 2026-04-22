using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace SidApi.Data.Entities;
 
[Table("app_user")]
public class User
{
    [Key]
    [Column("usr_pk")]
    public int Id { get; set; }
 
    [Column("usr_type")]
    [MaxLength(20)]
    public string Type { get; set; } = string.Empty;
 
    [Column("usr_name")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
 
    [Column("usr_fname")]
    [MaxLength(50)]
    public string? FirstName { get; set; }
 
    [Column("usr_lname")]
    [MaxLength(50)]
    public string? LastName { get; set; }
 
    [Column("usr_email")]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;
 
    [Column("usr_phone")]
    [MaxLength(25)]
    public string? Phone { get; set; }
 
    [Column("usr_active")]
    public bool Active { get; set; } = true;
}