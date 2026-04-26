using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

// 
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITimerService, TimerService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IBobService, BobService>();
builder.Services.AddScoped<ICaloriesService, CaloriesService>();

// 
builder.Services.AddHttpClient<IWorkoutClient, WorkoutClient>(client => {
    client.BaseAddress = new Uri("https://localhost:7194/");
}).AddStandardResilienceHandler();

builder.Services.AddHttpClient<IAiBobClient, AiBobClient>(client => {
    client.BaseAddress = new Uri("https://localhost:7194/");
}).AddStandardResilienceHandler();

// (Caching) -
builder.Services.AddHybridCache();

// Rate Limiting 
builder.Services.AddRateLimiter(options => {
    options.AddFixedWindowLimiter(policyName: "fixed", fixedOptions => {
        fixedOptions.PermitLimit = 10;
        fixedOptions.Window = TimeSpan.FromSeconds(10);
        fixedOptions.QueueLimit = 2;
    });
});

// AUTHENTICATION & SWAGGER GEN 
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Token "
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
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

builder.Services.AddCors(options => {
    options.AddPolicy("MainCorsPolicy", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddApiVersioning(options => {
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

//▄▀█ █▀█ █▀█
//█▀█ █▀▀ █▀▀ 

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // 
    app.MapScalarApiReference(options => {
        options.WithTitle("TLA API Reference")
               .WithTheme(ScalarTheme.Moon);
    });
}

app.UseHttpsRedirection();
app.UseCors("MainCorsPolicy");
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();