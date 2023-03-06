using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.Interfce;
using UniversityManagementSystemPortal.ModelDto;
using UniversityManagementSystemPortal.Repository;
using System.Text.Json.Serialization;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal;
using UniversityManagementSystemPortal.PictureManager;
using Microsoft.AspNetCore.Http.Features;

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
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    builder.Services.AddCors();
    builder.Services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    // configure DI for application services
    builder.Services.AddScoped<IUserInterface, UserRepository>();
    builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
    builder.Services.AddScoped<IRoleInterface, RoleRepository>();
    builder.Services.AddScoped<IUserRoleInterface, UserRoleRepository>();
    builder.Services.AddScoped<IInstituteAdminRepository, InstituteAdminRepository>();
    builder.Services.AddScoped<IInstituteRepository, InstituteRepository>();
    builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IPictureManager, PictureManager>();
    builder.Services.AddScoped<IStudentRepository, StudentRepository>();
    builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
    services.AddScoped<JwtMiddleware>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    //    builder.Services.AddIdentity<User, Role>(options =>
    //    {
    //        // Configure identity options
    //    })
    //.AddEntityFrameworkStores<UmspContext>()
    //.AddDefaultTokenProviders();
}
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "University Portal API", Version = "1.0" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    // Enable file upload in Swagger
    c.OperationFilter<FileUploadOperationFilter>();
});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1048576; // 1 MB limit
});

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
