using Infrastructure.Data.Contexts;
using Infrastructure.Messaging;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Booking Service API",
        Description = "Documentation for Booking Service",
    });
});


builder.Services.AddScoped<BookingRepo>();

builder.Services.AddScoped<BookingService>();

builder.Services.AddScoped<InvoiceServiceBus>(x => new InvoiceServiceBus(
    builder.Configuration["AzureServiceBusSettings:ConnectionString"],
    builder.Configuration["AzureServiceBusSettings:InvoiceQueueName"]));

builder.Services.AddScoped<EmailServiceBus>(x => new EmailServiceBus(
    builder.Configuration["AzureServiceBusSettings:ConnectionString"],
    builder.Configuration["AzureServiceBusSettings:EmailQueueName"]));

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking Service API");
    c.RoutePrefix = string.Empty;
});
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
