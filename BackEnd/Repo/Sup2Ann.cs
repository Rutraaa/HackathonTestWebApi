using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BackApi.Repo;

[Table("supp2annnoun")]
public class Sup2Ann : BaseModel
{

    [PrimaryKey("Id")]
    public int Id { get; set; }

    [Column("AnnouncementId")]
    public int AnnouncementId { get; set; }

    [Column("SupplierId")]
    public int SupplierId { get; set; }
}