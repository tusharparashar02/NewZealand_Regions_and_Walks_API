using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewZealand.Data;
using NewZealand.Interface;
using NewZealand.Mappings;
using NewZealand.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Serilog;
using NewZealand.Middlewares;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "NZ_Log.txt");
Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);

var logger = new LoggerConfiguration()
    .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(Options=>{
    Options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "NZ API", Version = "v1"});
    Options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    Options.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme, 
                    Id = JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<NZDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("NewZealandConnectionString")));

builder.Services.AddDbContext<NZAuthDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("NZAuthConnectionString")));

builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//identity
builder.Services.AddIdentityCore<IdentityUser>()
.AddRoles<IdentityRole>()
.AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
.AddEntityFrameworkStores<NZAuthDbContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(Options => {
    Options.Password.RequireDigit = false;
    Options.Password.RequireLowercase = false;
    Options.Password.RequireNonAlphanumeric = false;
    Options.Password.RequiredLength = 6;
    Options.Password.RequiredUniqueChars = 1;
});

//Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(Options => Options.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"], 
        IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});
app.MapControllers();
app.Run();


