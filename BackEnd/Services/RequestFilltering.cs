using BackEnd.DTO;
using BackEnd.Repo;
using Newtonsoft.Json;
using Client = Supabase.Client;

namespace BackEnd.Services
{
    public class RequestFilltering
    {
        public async Task<List<AnnouncementDTO>> GetSearchShceme(int CategoryId, string? SearchString, Client supabaseClient)
        {
            var announcementsString = (await supabaseClient.From<Announcement>().Get()).Content;
            var basequery = JsonConvert.DeserializeObject<List<AnnouncementDTO>>(announcementsString);
            basequery = CategoryId != 0 ? basequery : basequery.Where(x => x.CategoryId == CategoryId).ToList();
            basequery = basequery.Where(item => item.Title.Contains(SearchString)).ToList();

            return basequery;
        }
    }
}
