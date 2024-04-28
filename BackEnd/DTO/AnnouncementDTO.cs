using BackApi.DataTypes;
using Newtonsoft.Json;

namespace BackEnd.DTO
{
    public class AnnouncementDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("consumer_id")]
        public int ConsumerId { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("images")]
        public List<string> Images { get; set; }

        [JsonProperty("status")]
        public AnnouncementStatus Status { get; set; }

        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }
    }
}
