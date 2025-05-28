using System.Text.Json.Serialization;
using ADITUS.CodeChallenge.API.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddHttpClient<IEventStatisticsService, EventStatisticsService>();
builder.Services.AddSingleton<IHardwareReservationService, HardwareReservationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ADITUS Code Challenge API", Version = "v1" });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ADITUS Code Challenge API v1");
});

app.MapControllers();

app.Run();
