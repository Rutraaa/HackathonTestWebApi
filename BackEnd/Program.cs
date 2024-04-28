using BackApi.SupaBaseContext;
using BackEnd;
using BackEnd.Services;
using Microsoft.Extensions.DependencyInjection;
using Supabase;
using Client = Supabase.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//Connection to supabase
SupaBaseConnection supaBase = builder.Configuration.GetSection("SupaBaseConnection").Get<SupaBaseConnection>();

builder.Services.AddScoped(provider => supaBase);

var options = new SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
};

builder.Services.AddScoped(_ => new Client(supaBase.SupaBaseUrl, supaBase.SupaBaseKey, options));


builder.Services.AddSingleton<HashService>();

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
