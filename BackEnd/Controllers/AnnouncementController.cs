using BackEnd.Repo;
using BackEnd.Contracts.Announcement;
using BackEnd.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Supabase.Gotrue;
using Client = Supabase.Client;
using BackApi.SupaBaseContext;
using Supabase.Postgrest;
using BackEnd.Services;

namespace BackApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly ILogger<Announcement> _logger;
        private Client _supaBaseClient;
        private SupaBaseConnection _supaBaseConnection;
        private readonly RequestFilltering _requestFilltering;

        public AnnouncementController(ILogger<Announcement> logger, Client supaBaseClient, SupaBaseConnection supaBaseConnection, RequestFilltering requestFilltering)
        {
            _logger = logger;
            _supaBaseConnection = supaBaseConnection;
            _supaBaseClient = supaBaseClient;
            _requestFilltering = requestFilltering;
        }

        private async Task<bool> IsAuthorized(Session session)
        {
            if (session != null)
            {
                await _supaBaseClient.Auth.SetSession(session.AccessToken, session.RefreshToken, false);
                return true;
            }
            return false;
        }

        [HttpGet("/announcements/all")]
        public async Task<IActionResult> GetAnnouncementAll()
        {
            if (!await IsAuthorized(_supaBaseConnection.Session))
                return Unauthorized("Not authorized user");

            var response = await _supaBaseClient.From<Announcement>().Get();

            var announcementsString = response.Content;
            var announcements = JsonConvert.DeserializeObject<List<AnnouncementDTO>>(announcementsString);

            return Ok(announcements);
        }

        [HttpPost("/announcements/list")]
        public async Task<IActionResult> GetAnnouncementPaginationList([FromBody] GetAnnouncementRequest request)
        {
            if (!await IsAuthorized(_supaBaseConnection.Session))
                return Unauthorized("Not authorized user");

            var baseQuery = await _requestFilltering.GetSearchShceme(request.CategoryId, request.SearchString, _supaBaseClient);

            int offset = (request.currentPage - 1) * request.pageSize;

            int totalCount = baseQuery.Count;

            baseQuery = baseQuery.Skip(offset).Take(offset + request.pageSize - 1).ToList();

            var result = new GetAnnouncementResponse
            {
                items = baseQuery,
                totalCount = totalCount,
                pageNumber = request.currentPage,
                pageSize = request.pageSize
            };

            return Ok(result);
        }

        [HttpGet("/announcements/{id}")]
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            if (!await IsAuthorized(_supaBaseConnection.Session))
                return Unauthorized("Not authorized user");

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
            if (!await IsAuthorized(_supaBaseConnection.Session))
                return Unauthorized("Not authorized user");

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
            if (!await IsAuthorized(_supaBaseConnection.Session))
                return Unauthorized("Not authorized user");

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