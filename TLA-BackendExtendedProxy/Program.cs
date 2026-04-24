using TLA_BackendExtendedProxy.Clients;
using TLA_BackendExtendedProxy.Middlewares;
using TLA_BackendExtendedProxy.Services;
using Scalar.AspNetCore; 
using TLA_BackendExtendedProxy.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOllamaClient(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddHttpClient<ApiNinjasClient>();
builder.Services.AddScoped<ICaloriesService, CaloriesService>();
builder.Services.AddScoped<IOllamaService, OllamaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ServiceBCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddOpenApi();

builder.Services.AddMemoryCache();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();


app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();