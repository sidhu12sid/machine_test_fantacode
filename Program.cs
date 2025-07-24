using login_app.Interfaces;
using login_app.Models;
using login_app.Repositories;
using login_app.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // optional, if you need cookies or auth headers
    });
});

// Add services to the container.

builder.Services.AddControllers();

//Dependency injections

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJWTHandler, JWTHandler>();
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();

var env = builder.Environment;

if (!env.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    var dbPath = Path.Combine(Environment.GetEnvironmentVariable("HOME") ?? ".", "data", "app.db");
    Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite($"Data Source={dbPath}"));
 
}


//jwt setup start

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")) 
    };
});

//jwt setup end


builder.Services.AddRateLimiter(options =>
{

    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(15); 
        opt.PermitLimit = 4; 
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; 
        opt.QueueLimit = 0; 
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseCors("AllowAngularClient");
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();
