using System.Net;
using BackApi.Repo;
using BackApi.SupaBaseContext;
using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using Client = Supabase.Client;

namespace BackApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly ILogger<Announcement> _logger;
        private Client _supaBaseClient;
        private SupaBaseConnection _supaBaseConnection;

        public AnnouncementController(ILogger<Announcement> logger, Client supaBaseClient, SupaBaseConnection supaBaseConnection)
        {
            _logger = logger;
            _supaBaseConnection = supaBaseConnection;
            _supaBaseClient = supaBaseClient;
        }

        private async Task<bool> IsAuthorized(Session session)
        {
            if (_supaBaseConnection.Session != null)
            {
                await _supaBaseClient.Auth.SetSession(_supaBaseConnection.Session.AccessToken, _supaBaseConnection.Session.RefreshToken, false);
                return true;
            }
            return false;
        }

        [HttpGet("/list")]
        public async Task<IActionResult> GetAnnouncementList()
        {
            if (!await IsAuthorized(_supaBaseConnection.Session)) return Unauthorized();

            var response = await _supaBaseClient.From<Announcement>().Get();
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