using BackApi.Repo;
using BackEnd;
using BackEnd.Contracts.FileHolder;
using BackEnd.Contracts.Supplier;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Supabase.Gotrue;
using Client = Supabase.Client;

namespace BackApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileHolderController : ControllerBase
    {
        private readonly HashService _hashService;
        private Client _supaBaseClient;
        private SupaBaseConnection _supaBaseConnection;

        public FileHolderController(ILogger<Announcement> logger, HashService hashService,
            Client supaBaseClient, SupaBaseConnection supaBaseConnection)
        {
            _supaBaseClient = supaBaseClient;
            _supaBaseConnection = supaBaseConnection;
            _hashService = hashService;
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

        [HttpPost("/uploadFiles")]
        public async Task<IActionResult> SetFiles([FromBody] UploadFilesRequest request)
        {
            if (!(await IsAuthorized(_supaBaseConnection.Session))) return Unauthorized("not authorized");

            foreach (var file in request.FileNameList)
            {
                FileHolder record = new FileHolder
                {
                    AnnouncementId = request.AnnouncementId,
                    Content = file
                };
               var result =  await _supaBaseClient.From<FileHolder>().Insert(record);
            }

            return Ok();
        }

        [HttpGet("/getFiles")]
        public async Task<IActionResult> PostFiles(string password)
        {
            if (!(await IsAuthorized(_supaBaseConnection.Session))) return Unauthorized("not authorized");



        }
    }
}