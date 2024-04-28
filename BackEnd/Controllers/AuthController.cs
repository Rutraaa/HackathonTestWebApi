using BackEnd;
using BackEnd.Repo;
using BackEnd.Contracts.Consumer;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Client = Supabase.Client;

namespace BackApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HashService _hashService;
        private Client _supaBaseClient;
        private SupaBaseConnection _supaBaseConnection;

        public AuthController(HashService hashService, Client supaBaseClient, SupaBaseConnection supaBaseConnection)
        {
            _supaBaseClient = supaBaseClient;
            _supaBaseConnection = supaBaseConnection;
            _hashService = hashService;
        }

        [HttpPost("/signUp")]
        public async Task<IActionResult> SignUp(CreatingUserRequest request)
        {
            try
            {
                var session = await _supaBaseClient.Auth.SignUp(request.Email, request.Password);
                if (session == null)
                {
                    return NotFound();
                }

                if (request.isSupplier)
                {
                    await _supaBaseClient.From<Supplier>().Insert(new Supplier
                    {
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Phone = request.Phone,
                        Email = request.Email,
                        Password = _hashService.GetMd5Hash(request.Password)
                    });
                    return Ok();
                }
                await _supaBaseClient.From<Consumer>().Insert(new Consumer
                {
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
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

        [HttpPost("/signIn")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            try
            {
                var session = await _supaBaseClient.Auth.SignIn(request.Email, request.Password);
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

        [HttpGet("/logout")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _supaBaseClient.Auth.SignOut();
                _supaBaseConnection.Session = null;
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}