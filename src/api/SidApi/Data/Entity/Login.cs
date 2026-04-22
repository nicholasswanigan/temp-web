using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SidApi.Data.Entities
{
[Table("login_for_users")]
public class Login
{
    [Key]
    [Column("login_pk")]
    public int Id { get; set; }

    [Column("usr_fk")]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    [Column("usr_usrname")]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Column("usr_pword")]
    [MaxLength(256)]
    public string Password { get; set; } = string.Empty;
}
}