using THY.GatePlanner.API.Utils;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddAppServices();

builder.AddDataLayer();

var app = builder.Build();

DbInitializer.InitializeDb(app);
 
app.UseSwagger();
app.UseSwaggerUI();

app.AddMiddlewares();

app.UseAuthorization();

app.MapControllers();

app.Run();

