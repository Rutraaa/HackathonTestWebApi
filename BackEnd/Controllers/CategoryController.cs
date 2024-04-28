using BackEnd;
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
    public class CategoryController : ControllerBase
    {
        private readonly HashService _hashService;
        private Client _supaBaseClient;
        private SupaBaseConnection _supaBaseConnection;

        public CategoryController(HashService hashService, Client supaBaseClient, SupaBaseConnection supaBaseConnection)
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

        [HttpGet]
        public async Task<IActionResult> GetCategoryList()
        {
            try
            {
                if (!await IsAuthorized(_supaBaseConnection.Session))
                    return Unauthorized("Not authorized user");

                var response = (await _supaBaseClient.From<Category>().Get()).Content;
                var output = JsonConvert.DeserializeObject<List<CategoryDTO>>(response);

                return Ok(output);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}