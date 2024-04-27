using BackApi.DataTypes;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BackApi.Repo;

[Table("announcement_duplicate")]
public class Announcement: BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("consumer_id")]
    public int ConsumerId { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("subtitle")]
    public string Subtitle { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("tags")]
    public List<string> Tags { get; set; }

    [Column("status")]
    public AnnouncementStatus Status { get; set; }
}