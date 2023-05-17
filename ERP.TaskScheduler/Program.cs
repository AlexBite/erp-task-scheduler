using ERP.TaskScheduler.Clients;
using ERP.TaskScheduler.Database;
using ERP.TaskScheduler.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services here
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt
    .UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
    .UseSnakeCaseNamingConvention());
builder.Services.AddSingleton<SchedulerBackgroundService>();
builder.Services.AddHostedService(
    provider => provider.GetRequiredService<SchedulerBackgroundService>());
builder.Services.AddSingleton<IRabbitMqClient, RabbitMqClient>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseCors(builder => builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin());

    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction()) app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();