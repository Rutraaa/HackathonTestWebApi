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

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}
