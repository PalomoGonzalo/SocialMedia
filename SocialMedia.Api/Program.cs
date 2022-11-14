using System.Collections.Immutable;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructura.Filtro;
using SocialMedia.Infraestructura.Options;
using SocialMedia.Infraestructura.Repositorios;
using System.Text;
using System.Net;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureAppConfiguration((hostingContext, config) =>
    {
        var env = hostingContext.HostingEnvironment;

        config.SetBasePath(env.ContentRootPath)
            .AddJsonFile("Secrets.json", optional: true, reloadOnChange: true);

    });



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// config heroku 
var port = Environment.GetEnvironmentVariable("PORT") ?? "3000";

builder.WebHost.UseKestrel()
    .ConfigureKestrel((context,options)=>
    {
        options.Listen(IPAddress.Any, Int32.Parse(port),listenOptions=>
        {

        });
        
    });



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialMedia_APi", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                   Id = "Bearer",
                   Type = ReferenceType.SecurityScheme
                },
                Scheme = "Oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }

     });

});


builder.Services.AddCors(x => x.AddPolicy("EnableCors", builder =>
{
    builder.SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyOrigin()
            //.WithOrigins("https://codestack.com")
            .AllowAnyMethod()
            //.WithMethods("PATCH", "DELETE", "GET", "HEADER")
            .AllowAnyHeader();
    //.WithHeaders("X-Token", "content-type")
}));

var pass= builder.Configuration["Authentication:SecretKey"];


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(pass))
    };
});

var section = builder.Configuration.GetSection("PassOptions");


builder.Services.Configure<PassOptions>(section);


builder.Services.AddHealthChecks();
///RateLimit Services
builder.Services.AddOptions();

// needed to store rate limit counters and ip rules
builder.Services.AddMemoryCache();

//load general configuration from appsettings.json
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

//load ip rules from appsettings.json
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

// inject counter and rules stores
builder.Services.AddInMemoryRateLimiting();
//services.AddDistributedRateLimiting<AsyncKeyLockProcessingStrategy>();
//services.AddDistributedRateLimiting<RedisProcessingStrategy>();
//services.AddRedisRateLimiting();

// Add framework services.


// configuration (resolvers, counter key builders)
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddScoped<IPublicacionRepositorio, PublicacionRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ISeguridadRepositorio, SeguridadRepositorio>();
builder.Services.AddTransient<IPasswordHasherRepositorio, PasswordHaserRepositorio>();
builder.Services.AddScoped<ILlavesRepositorio, LlavesRepositorio>();





builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidacionFiltro>();
});



var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseIpRateLimiting();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("../swagger/v1/swagger.json", "Social Media API V1");
});

app.MapHealthChecks(pattern: "/health");

app.UseHttpsRedirection();

app.UseCors("EnableCors");

app.UseAuthentication();


app.UseAuthorization();


app.MapControllers();

app.Run();
