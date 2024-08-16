using Microsoft.Extensions.Logging;
using THY.GatePlanner.API;
using THY.GatePlanner.API.Hubs;
using THY.GatePlanner.API.RabbitMQ;
using THY.GatePlanner.API.Utils;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddCors(o => o.AddPolicy("Thy-Policy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddAppServices();

var host = builder.Configuration["RabbitMQ:Host"];
var username = builder.Configuration["RabbitMQ:Username"];
var password = builder.Configuration["RabbitMQ:Password"];
var port = builder.Configuration["RabbitMQ:Port"];

builder.Services.AddSingleton<IRabbitMqService>(provider =>
{
    return new RabbitMQService(host, username, password, Convert.ToInt32(port));
});

builder.Services.AddHostedService<QueueConsumerService>();


builder.AddDataLayer();


var app = builder.Build();

DbInitializer.InitializeDb(app);

app.UseCors("Thy-Policy");

app.UseSwagger();
app.UseSwaggerUI();


app.AddMiddlewares();

app.UseAuthorization();

app.MapHub<PlaneGateHub>("/communicate").RequireCors("Thy-Policy");
app.MapControllers();

app.Run();

