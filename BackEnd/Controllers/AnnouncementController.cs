using BackApi.DataTypes;
using BackApi.Repo;
using Microsoft.AspNetCore.Mvc;
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
           var response  = await _supaBaseClient.From<Announcement>().Get();
           List<Announcement> listResult = response.Models.Select(item => new Announcement
           {
               ConsumerId = item.ConsumerId,
               CategoryId = item.CategoryId,
               Title = item.Title,
               Subtitle = item.Subtitle,
               Description = item.Description,
               Tags = item.Tags,
               Status = item.Status
           }).ToList();
           return Ok(listResult);
        }
    }
}