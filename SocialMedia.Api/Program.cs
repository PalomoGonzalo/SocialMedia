using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructura.Filtro;
using SocialMedia.Infraestructura.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPublicacionRepositorio, PublicacionRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidacionFiltro>();
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
