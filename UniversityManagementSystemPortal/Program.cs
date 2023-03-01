using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto;
using UniversityManagementSystemPortal.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
{
    var services = builder.Services;
    var env = builder.Environment;
    builder.Services.AddDbContext<UmspContext>(opt =>
     opt.UseSqlServer("UniversityManagementSystemPortal"));
    builder.Services.AddControllersWithViews()
   .AddNewtonsoftJson(options =>
   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    // Add services to the container.
    // configure strongly typed settings object
    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    builder.Services.AddCors();
    builder.Services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    // configure DI for application services
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
    //builder.Services.AddScoped<IUserRoleManager, UserRoleManager>();
    services.AddScoped<JwtMiddleware>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    //    builder.Services.AddIdentity<User, Role>(options => {
    //        // Configure identity options
    //    })
    //.AddEntityFrameworkStores<UmspContext>()
    //.AddDefaultTokenProviders();
}
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "University Portal API", Version = "PRO" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var secret = new byte[32];
using (var rng = RandomNumberGenerator.Create())
{
    rng.GetBytes(secret);
}
//var appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
//var key = Encoding.ASCII.GetBytes(appSettings.Secret);
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.RequireHttpsMetadata = false;
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(key),
//            ValidateIssuer = false,
//            ValidateAudience = false,
//            ClockSkew = TimeSpan.Zero
//        };
//    });
var app = builder.Build();
// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();
// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();
