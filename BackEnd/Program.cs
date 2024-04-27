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

var a = "https://tjkflmlvjnjkpsgdmldu.supabase.co";
var b = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InRqa2ZsbWx2am5qa3BzZ2RtbGR1Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTQxOTgwMjksImV4cCI6MjAyOTc3NDAyOX0.ABVmw7RBPDqAVOwK9rvrkibGBo7Kx8XbcOpdPYeFdmM";

builder.Services.AddScoped(_ => new Client(a, b, options));


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
