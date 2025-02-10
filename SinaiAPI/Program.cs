using Microsoft.EntityFrameworkCore;
using SinaiAPI;
using SinaiAPI.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAlways",
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<GuideService>();
builder.Services.AddScoped<WorkplaceService>();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SinaiDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseCors("AllowAlways");
app.UseHttpsRedirection();
app.UseDeveloperExceptionPage();
app.UseAuthorization();
app.MapControllers();
app.Run();
