using BackEnd.Repo;
using BackEnd.Contracts.Announcement;
using BackEnd.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Supabase.Gotrue;
using Client = Supabase.Client;
using BackEnd;
using BackEnd.Services;

namespace BackApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private Client _supaBaseClient;
        private SupaBaseConnection _supaBaseConnection;
        private readonly RequestFilltering _requestFilltering;

        public AnnouncementController(Client supaBaseClient, SupaBaseConnection supaBaseConnection, RequestFilltering requestFilltering)
        {
            _supaBaseConnection = supaBaseConnection;
            _supaBaseClient = supaBaseClient;
            _requestFilltering = requestFilltering;
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

        [HttpPost("/list")]
        public async Task<IActionResult> GetAnnouncementPaginationList([FromBody] GetAnnouncementRequest request)
        {
            if (!await IsAuthorized(_supaBaseConnection.Session))
                return Unauthorized("Not authorized user");

            var baseQuery = await _requestFilltering.GetSearchShceme(request.CategoryId, request.SearchString, _supaBaseClient);

            int offset = (request.currentPage - 1) * request.pageSize;

            int totalCount = baseQuery.Count;

            baseQuery = baseQuery.Skip(offset).Take(request.pageSize).ToList();

            var result = new GetAnnouncementResponse
            {
                items = baseQuery,
                totalCount = totalCount,
                pageNumber = request.currentPage,
                pageSize = request.pageSize
            };

            return Ok(result);
        }

        [HttpGet("getItem/{id}")]
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            if (!await IsAuthorized(_supaBaseConnection.Session))
                return Unauthorized("Not authorized user");

            var announcement = await _supaBaseClient
                .From<Announcement>()
                .Where(x => x.Id == id)
                .Single();

            if (announcement == null)
            {
                return NotFound(); 
            }

            var pes = new AnnouncementDTO
            {
                Id = announcement.Id,
                ConsumerId = announcement.ConsumerId,
                CategoryId = announcement.CategoryId,
                Title = announcement.Title,
                Description = announcement.Description,
                Images = announcement.Images,
                Status = announcement.Status,
                CreatedDate = announcement.CreatedDate,
                Phone = announcement.Phone,
                Email = announcement.Email,
                FirstName = announcement.FirstName,
                LastName = announcement.LastName
            };

            return Ok(pes);
        }

        [HttpPost("/create")]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementRequest request)
        {
            if (!await IsAuthorized(_supaBaseConnection.Session))
                return Unauthorized("Not authorized user");

            var user = await _supaBaseClient.From<Consumer>().Where(item => item.Id == request.ConsumerId).Single();


            var announcement = new Announcement
            {
                ConsumerId = request.ConsumerId,
                CategoryId = request.CategoryId,
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Images = request.Images,
                Phone = user.Phone,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedDate = DateTime.Now,

            };
                
            var response = await _supaBaseClient.From<Announcement>().Insert(announcement);
            return Ok();
        }

        [HttpPut("/update/{id}")]
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
            model.Images = request.Images;
            model.Description = request.Description;

            await model.Update<Announcement>();

            return Ok();
        }
    }
}