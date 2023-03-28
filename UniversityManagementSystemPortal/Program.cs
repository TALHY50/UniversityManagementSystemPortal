using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using UniversityManagementSystemPortal;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto;
using UniversityManagementSystemPortal.Models.DbContext;
using UniversityManagementSystemPortal.PictureManager;
using UniversityManagementSystemPortal.Repository;
using UniversityManagementSystemPortal.SwaggerFileUploadFilter;

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
    builder.Host.UseSerilog(SerilogConfig.CreateLogger()); 
    builder.Services.AddLogging();
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
    builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
    builder.Services.AddScoped<IInstituteAdminRepository, InstituteAdminRepository>();
    builder.Services.AddScoped<IInstituteRepository, InstituteRepository>();
    builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IPictureManager, PictureManager>();
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<IStudentProgramRepository, StudentProgramRepository>();
    builder.Services.AddScoped<IPositionRepository, PositionRepository>();
    builder.Services.AddScoped<IStudentRepository, StudentRepository>();
    services.AddMediatR(typeof(AddStudentCommandHandler).Assembly);
    builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
    builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddScoped(typeof(IIdentityServices), typeof(IdentityServices));
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);
    services.AddAutoMapper(typeof(Program));
    //builder.Services.AddScoped(typeof(CreateLogger());

}
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "University Portal API",
        Version = "ProLevel",
        Description = "A simple API for managing universities and Institute",
        Contact = new OpenApiContact
        {
            Name = "Talha Asif",
            Email = "talhaasif.ibs@gmail.com",
            Url = new Uri("https://github.com/")

        }
    });

    // Add a security definition for JWT tokens
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    // Add a security requirement for JWT tokens
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

    // Add an operation filter for handling file uploads
    c.OperationFilter<FileUploadOperationFilter>();

});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1048576; // 1 MB limit
});

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
//app.UseMiddleware<ExceptionHandlingMiddleware>();
// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
    {
        { ".csv", "text/csv" },
    })
});
app.Run();

