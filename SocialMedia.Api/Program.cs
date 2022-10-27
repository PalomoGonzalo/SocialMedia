using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructura.Filtro;
using SocialMedia.Infraestructura.Repositorios;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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


builder.Services.AddCors(x => x.AddPolicy("EnableCors", builder => {
    builder.SetIsOriginAllowedToAllowWildcardSubdomains()
            .AllowAnyOrigin()
            //.WithOrigins("https://codestack.com")
            .AllowAnyMethod()
            //.WithMethods("PATCH", "DELETE", "GET", "HEADER")
            .AllowAnyHeader();
    //.WithHeaders("X-Token", "content-type")
}));


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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]))
    };
});

builder.Services.AddScoped<IPublicacionRepositorio, PublicacionRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidacionFiltro>();
});



var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI(options=>
{
    options.SwaggerEndpoint("../swagger/v1/swagger.json", "Social Media API V1");
});

app.UseHttpsRedirection();

app.UseCors("EnableCors");

app.UseAuthentication();


app.UseAuthorization();


app.MapControllers();

app.Run();
