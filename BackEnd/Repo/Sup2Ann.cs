using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BackEnd.Repo;

[Table("supp2annnoun")]
public class Sup2Ann : BaseModel
{

    [PrimaryKey("Id", false)]
    public int Id { get; set; }

    [Column("announcement_id")]
    public int AnnouncementId { get; set; }

    [Column("supplie_id")]
    public int SupplierId { get; set; }
}