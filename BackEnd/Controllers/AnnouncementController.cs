using BackApi.DataTypes;
using BackApi.Repo;
using BackEnd.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Supabase;

namespace BackApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly ILogger<Announcement> _logger;
        private readonly Client _supaBaseClient;

        public AnnouncementController(ILogger<Announcement> logger, Client supaBaseClient)
        {
            _logger = logger;
            _supaBaseClient = supaBaseClient;
        }

        [HttpGet("/list")]
        public async Task<IActionResult> GetAnnouncementList()
        {
            var response = await _supaBaseClient.From<Announcement>().Get();
            
            var announcementString = response.Content;
            var announcement = JsonConvert.DeserializeObject<List<AnnouncementDTO>>(announcementString);

            return Ok(announcement);
        }
    }
}