using FluentValidation.AspNetCore;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManagement.BuisnessLogic.Services;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using TaskManagement.BuisnessLogic.DataSantization;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Identity.UI.Services;
using TaskManagement.DataAccess.Constants;
using MediatR;
using TaskManagement.BuisnessLogic.Contracts.Tasks.Handlers;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.DataAccess.Interfaces;
using TaskManagement.BuisnessLogic.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>          //data santization
{
    options.Filters.Add(typeof(InputSanitizationFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter Your Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
          new OpenApiSecurityScheme{
             Reference = new OpenApiReference{
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              }

            },
            new List<string>()
          }
        });
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddOptions<JWT>().BindConfiguration("JWT")
    .ValidateDataAnnotations().ValidateOnStart();
builder.Services.AddIdentity<User,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddHangfire(configuration => configuration
      .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
      .UseSimpleAssemblyNameTypeSerializer()
      .UseRecommendedSerializerSettings()
      .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));
builder.Services.AddHangfireServer();
builder.Services.Configure<IdentityOptions>(options =>   // register identity
{
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedEmail = true;
    options.User.RequireUniqueEmail = true;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]!)),
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"]
    };
});
//builder.Services.AddAuthorization(options =>                  // role based-authorize
//{
//    options.AddPolicy("AdminOrTeamLeader", policy =>
//        policy.RequireRole("Admin", "TeamLeader"));
//});

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())  // Main assembly
        .RegisterServicesFromAssembly(Assembly.Load("TaskManagement.BuisnessLogic")) 
);



builder.Services.AddScoped<ITaskService, TaskService>();   // services injection
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ITimelogService, TimelogService>();
builder.Services.AddScoped<InputSanitizationFilter>();
builder.Services.AddScoped<INotficationService,NotficationService>();
builder.Services.AddScoped<IEmailSender,EmailSender>();


builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.AddMapster();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();
var mappingconfig = TypeAdapterConfig.GlobalSettings;
mappingconfig.Scan(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IMapper>(new Mapper(mappingconfig));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    Authorization = [
        new HangfireCustomBasicAuthenticationFilter{
            User = app.Configuration.GetValue<string>("HangfireSettings:Username"),
            Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
        }
    ],
    DashboardTitle = "SurveyBasketJobs"
});

var factory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scope = factory.CreateScope();
var Notficationservices = scope.ServiceProvider.GetRequiredService<INotficationService>();
RecurringJob.AddOrUpdate("SendNewTaskNotfications", () => Notficationservices.HandleTaskDeadline(), Cron.Daily());
RecurringJob.AddOrUpdate("SendTaskReminderDate", () => Notficationservices.SendTaskReminderDate(), Cron.Daily(9, 0));
app.UseAuthorization();
app.MapControllers();

app.Run();
