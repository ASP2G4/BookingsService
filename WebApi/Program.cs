using Infrastructure.Data.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<BookingRepo>();

builder.Services.AddScoped<BookingService>();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

var app = builder.Build();

app.MapOpenApi();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
