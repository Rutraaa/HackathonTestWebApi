using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BackEnd.Repo;

[Table("category")]
public class Category : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
}