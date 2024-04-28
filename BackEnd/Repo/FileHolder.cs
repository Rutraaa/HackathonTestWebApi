using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BackApi.Repo;

[Table("fileholder")]
public class FileHolder : BaseModel
{

    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("announcement_id")]
    public int AnnouncementId { get; set; }
    [Column("content")]
    public string Content { get; set; }
}   