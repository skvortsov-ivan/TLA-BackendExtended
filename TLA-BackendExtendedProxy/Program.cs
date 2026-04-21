using TLA_BackendExtendedProxy.Clients;
using TLA_BackendExtendedProxy.Middlewares;
using TLA_BackendExtendedProxy.Services;
using Scalar.AspNetCore; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient<ApiNinjasClient>();
builder.Services.AddScoped<ICaloriesService, CaloriesService>();


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