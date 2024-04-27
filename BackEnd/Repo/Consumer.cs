using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BackApi.Repo;

[Table("consumer")]
public class Consumer : BaseModel
{
    [PrimaryKey("Id")]
    public int Id { get; set; }

    [Column("CreateDate")]
    public DateTime CreateDate { get; set; }

    [Column("UpdateDate")]
    public DateTime UpdateDate { get; set; }

    [Column("FirstName")]
    public string FirstName { get; set; }

    [Column("LastName")]
    public string LastName { get; set; }

    [Column("Phone")]
    public string Phone { get; set; }

    [Column("Email")]
    public string Email { get; set; }

    [Column("Password")]
    public string Password { get; set; }
}