using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BackApi.Repo;

[Table("category")]
public class Category : BaseModel
{
    [PrimaryKey("Id")]
    public int Id { get; set; }

    [Column("Name")]
    public string Name { get; set; }
}