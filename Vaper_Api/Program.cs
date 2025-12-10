using Microsoft.EntityFrameworkCore;
using Vaper_Api.Models;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

// =======================
// ? CLOUDINARY CONFIG
// =======================
var cloudName = builder.Configuration["CloudinarySettings:CloudName"];
var apiKey = builder.Configuration["CloudinarySettings:ApiKey"];
var apiSecret = builder.Configuration["CloudinarySettings:ApiSecret"];

var account = new Account(cloudName, apiKey, apiSecret);
var cloudinary = new Cloudinary(account);
cloudinary.Api.Secure = true;

builder.Services.AddSingleton(cloudinary);
builder.Services.AddScoped<CloudinaryService>();

// =======================
// ? SERVICIOS EXISTENTES
// =======================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? TU BASE DE DATOS
builder.Services.AddDbContext<VaperContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// ? SWAGGER SIEMPRE ACTIVO
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vaper API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();