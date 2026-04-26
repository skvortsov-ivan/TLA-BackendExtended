using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using TLA_BackendExtended.Clients;
using TLA_BackendExtended.Middlewares;
using TLA_BackendExtended.Services;



//█▄▄ █░█ █ █░░ █▀▄ █▀▀ █▀█
//█▄█ █▄█ █ █▄▄ █▄▀ ██▄ █▀▄

var builder = WebApplication.CreateBuilder(args);

// CONTROLLERS ---------------------------------------------------------
builder.Services.AddControllers();

// AUTHENTICATION BUTTON ---------------------------------------------------------
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Klistra in din JWT-token här!"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
    });
});

// SERVICES ---------------------------------------------------------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITimerService, TimerService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IBobService, BobService>();

// RATE LIMITING ---------------------------------------------------------
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.ContentType = "application/problem+json";

        var problem = new ProblemDetails
        {
            Status = StatusCodes.Status429TooManyRequests,
            Title = "Too Many Requests",
            Detail = "You have exceeded the allowed number of requests. Please try again later."
        };

        await context.HttpContext.Response.WriteAsJsonAsync(problem, cancellationToken: token);
    };


    options.AddSlidingWindowLimiter("sliding", config =>
    {
        config.Window = TimeSpan.FromMinutes(1);
        config.SegmentsPerWindow = 6;
        config.PermitLimit = 2;
    });
});

// API PROXY --------------------------------------------------------
builder.Services.AddHttpClient<IWorkoutClient, WorkoutClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7194/");
}).AddStandardResilienceHandler();

builder.Services.AddHttpClient<IAiBobClient, AiBobClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7194/");
}).AddStandardResilienceHandler();

// HYBRID CACHE -----------------------------------------------------
#pragma warning disable EXTEXP0018
builder.Services.AddHybridCache();
#pragma warning restore EXTEXP0018

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
            ValidIssuer = "TLA_Backend_App",
            ValidAudience = "TLA_Frontend",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("adminSecurityKey123thisIsASuperSafeKeyOnGod"))
        };
    });

// CORS POLICY ------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("ServiceACorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// VERSIONING --------------------------------------------------------
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})

.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


//▄▀█ █▀█ █▀█
//█▀█ █▀▀ █▀▀
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MainCorsPolicy");
app.MapControllers();
app.Run();

