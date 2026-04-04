using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TLA_BackendExtended.Services;
using System.Text;



//█▄▄ █░█ █ █░░ █▀▄ █▀▀ █▀█
//█▄█ █▄█ █ █▄▄ █▄▀ ██▄ █▀▄

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();

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
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MainCorsPolicy");
app.MapControllers();
app.Run();

