using BackApi.DataTypes;
using BackApi.Repo;
using BackEnd.Contracts.Announcement;
using BackEnd.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Supabase;
using System.Diagnostics;
using System.Net;
using System.Reflection;

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

        [HttpGet("/announcements")]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var response = await _supaBaseClient.From<Announcement>().Get();

            var announcementsString = response.Content;
            var announcements = JsonConvert.DeserializeObject<List<AnnouncementDTO>>(announcementsString);
            +
            return Ok(announcements);
        }

        [HttpGet("/announcements/{id}")]
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            var response = await _supaBaseClient
                .From<Announcement>()
                .Where(x => x.Id == id)
                .Get();

            var announcementString = response.Content;
            var announcement = JsonConvert.DeserializeObject<List<AnnouncementDTO>>(announcementString);

            return Ok(announcement);
        }

        [HttpPost("/announcement/create")]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementRequest request)
        {
            var announcement = new Announcement
            {
                ConsumerId = request.ConsumerId,
                CategoryId = request.CategoryId,
                Title = request.Title,
                Subtitle = request.Subtitle,
                Description = request.Description,
                Status = request.Status,
                Tags = request.Tags,
                CreatedDate = DateTime.Now
            };

            var response = await _supaBaseClient.From<Announcement>().Insert(announcement);

            return Ok();
        }

        [HttpPut("/announcement/update/{id}")]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] UpdateAnnouncementRequest request, int id)
        {
            var model = await _supaBaseClient
                    .From<Announcement>()
                    .Where(task => task.Id == id)
                    .Single();

            model.CategoryId = request.CategoryId;
            model.Title = request.Title;
            model.Subtitle = request.Subtitle;
            model.Description = request.Description;
            model.Tags = request.Tags;

            await model.Update<Announcement>();

            return Ok();
        }
    }
}