using Microsoft.EntityFrameworkCore;
using SinaiAPI;
using SinaiAPI.Services;

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

builder.Services.AddControllers();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SinaiDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseCors("AllowAlways");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
