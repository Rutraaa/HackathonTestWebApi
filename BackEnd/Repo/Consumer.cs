using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BackApi.Repo;

[Table("consumer")]
public class Consumer : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("created_date")]
    public DateTime CreateDate { get; set; }

    [Column("update_date")]
    public DateTime UpdateDate { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; }

    [Column("last_name")]
    public string LastName { get; set; }

    [Column("phone")]
    public string Phone { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("password")]
    public string Password { get; set; }
}