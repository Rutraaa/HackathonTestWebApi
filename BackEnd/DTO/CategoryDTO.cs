using BackApi.DataTypes;
using Newtonsoft.Json;
using Supabase.Postgrest.Attributes;

namespace BackEnd.DTO
{
    public class CategoryDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
