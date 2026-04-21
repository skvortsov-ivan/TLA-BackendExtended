using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TLA_BackendExtended.Clients;
using TLA_BackendExtended.Middlewares;
using TLA_BackendExtended.Services;
using Microsoft.AspNetCore.RateLimiting; 
using System.Threading.RateLimiting;
using Scalar.AspNetCore;


//█▄▄ █░█ █ █░░ █▀▄ █▀▀ █▀█
//█▄█ █▄█ █ █▄▄ █▄▀ ██▄ █▀▄

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITimerService, TimerService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();

//  Rate Limiting 
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "fixed", fixedOptions =>
    {
        fixedOptions.PermitLimit = 10; 
        fixedOptions.Window = TimeSpan.FromSeconds(10); 
        fixedOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        fixedOptions.QueueLimit = 2; 
});
    });

// (Caching) 
builder.Services.AddMemoryCache();


// API PROXY -------------------------------------------------------
builder.Services.AddHttpClient<IWorkoutClient, WorkoutClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7194/");
});

// AUTHENTICATION ---------------------------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "DinApp",
            ValidAudience = "DinFrontend",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("adminSecurityKey123"))
        };
    });

// CORS POLICY ------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("MainCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});



//▄▀█ █▀█ █▀█
//█▀█ █▀▀ █▀▀
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

//  Scalar instead of Swagger
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MainCorsPolicy");
app.MapControllers();
app.Run();

