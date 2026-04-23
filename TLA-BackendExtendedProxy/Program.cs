using System.Net.Http.Headers;
using TLA_BackendExtendedProxy.Clients;
using TLA_BackendExtendedProxy.Middlewares;
using TLA_BackendExtendedProxy.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOllamaClient(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddHttpClient<ApiNinjasClient>();

builder.Services.AddScoped<ICaloriesService, CaloriesService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{  
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();

app.Run();

