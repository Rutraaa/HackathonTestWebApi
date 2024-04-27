using BackApi.Repo;
using BackApi.SupaBaseContext;
using BackEnd.Contracts.Supplier;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Client = Supabase.Client;

namespace BackApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthSupplierController : ControllerBase
    {
        private readonly HashService _hashService;
        private Client _supaBaseClient;
        private SupaBaseConnection _supaBaseConnection;

        public AuthSupplierController(ILogger<Announcement> logger, HashService hashService,
            Client supaBaseClient, SupaBaseConnection supaBaseConnection)
        {
            _supaBaseClient = supaBaseClient;
            _supaBaseConnection = supaBaseConnection;
            _hashService = hashService;
        }

        [HttpPost("/supplier/signUp")]
        public async Task<IActionResult> SignUp(CreatingSupplierRequest request)
        {
            try
            {
                var session = await _supaBaseClient.Auth.SignUp(request.Email, request.Password);
                if (session == null)
                {
                    return NotFound();
                }

                await _supaBaseClient.From<Supplier>().Insert(new Supplier
                {
                    CreateDate = request.CreateDate,
                    UpdateDate = request.UpdateDate,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Phone = request.Phone,
                    Email = request.Email,
                    Password = _hashService.GetMd5Hash(request.Password)
                });

                return Ok();
            }
            catch (Exception)
            {
                return NotFound("The current email already exist");
            }
        }

        [HttpPost("/supplier/signIn")]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            try
            {
                var session = await _supaBaseClient.Auth.SignIn(email, password);
                if (session == null)
                {
                    return NotFound();
                }
                _supaBaseConnection.Session = session;
                return Ok(session.AccessToken);
            }
            catch (Exception)
            {
                return NotFound("Invalid login credentials");
            }
        }
    }
}