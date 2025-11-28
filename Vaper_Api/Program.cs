using Microsoft.EntityFrameworkCore;
using Vaper_Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar VaperContext en el contenedor de DI
builder.Services.AddDbContext<VaperContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// --- HABILITAR SWAGGER SIEMPRE ---
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vaper API v1");
    c.RoutePrefix = string.Empty; //  Swagger será la página de inicio
});
// ----------------------------------

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
