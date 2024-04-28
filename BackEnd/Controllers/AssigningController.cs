using System.Net;
using BackApi.DataTypes;
using BackEnd;
using BackEnd.Contracts.Assigning;
using BackEnd.Repo;
using BackEnd.Contracts.Consumer;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Client = Supabase.Client;
using BackEnd.DTO;
using Newtonsoft.Json;
using Supabase.Gotrue;

namespace BackApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssigningController : ControllerBase
    {
        private Client _supaBaseClient;
        private SupaBaseConnection _supaBaseConnection;

        public AssigningController(Client supaBaseClient, SupaBaseConnection supaBaseConnection)
        {
            _supaBaseClient = supaBaseClient;
            _supaBaseConnection = supaBaseConnection;
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

        [HttpPost("/assign-supplier")]
        public async Task<IActionResult> GetCategoryList([FromBody] AssigningRequest request)
        {
            try
            {
                if (!await IsAuthorized(_supaBaseConnection.Session))
                    return Unauthorized("Not authorized user");

                if (!(await _supaBaseClient.From<Supplier>().Get()).Models.Select(item => item.Id).Contains(request.SupplierId))
                    return NotFound("Current user isn't supplier");

                if (!(await _supaBaseClient.From<Announcement>().Get()).Models.Select(item => item.Id).Contains(request.AnnouncementId))
                    return NotFound("Current announcement doesn't found");

                var response = await _supaBaseClient.From<Sup2Ann>().Insert(new Sup2Ann
                {
                    AnnouncementId = request.AnnouncementId,
                    SupplierId = request.SupplierId
                });

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("/update-status")]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] UpdateAnnouncementStatusRequest request)
        {
            if (!await IsAuthorized(_supaBaseConnection.Session))
                return Unauthorized("Not authorized user");

            var model = await _supaBaseClient
                .From<Announcement>()
                .Where(task => task.Id == request.AnnouncementId)
                .Single();

            model.Status = (AnnouncementStatus)request.Status;

            await model.Update<Announcement>();

            return Ok();
        }
    }
}