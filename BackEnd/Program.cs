using BackApi.Repo;
using BackApi.SupaBaseContext;
using Microsoft.Extensions.DependencyInjection;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

//Connection to supabase
var supaBase = builder.Configuration.GetSection("SupaBaseConnection").Get<SupaBaseConnection>();
var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
};

builder.Services.AddScoped(_ => new Client(supaBase.SupaBaseUrl, supaBase.SupaBaseKey, options));

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();