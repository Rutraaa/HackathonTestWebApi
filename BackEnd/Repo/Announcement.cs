using BackApi.DataTypes;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BackEnd.Repo;

[Table("announcement")]
public class Announcement: BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("consumer_id")]
    public int ConsumerId { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("status")]
    public AnnouncementStatus Status { get; set; }
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }

    [Column("images")]
    public List<string> Images { get; set; }

}