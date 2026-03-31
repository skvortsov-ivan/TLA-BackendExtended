using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TLA_BackendExtended.Filters;



//█▄▄ █░█ █ █░░ █▀▄ █▀▀ █▀█
//█▄█ █▄█ █ █▄▄ █▄▀ ██▄ █▀▄

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>{options.Filters.Add<ExecutionTimeFilter>();});
builder.Services.AddSwaggerGen();

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
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MainCorsPolicy");
app.MapControllers();
app.Run();

