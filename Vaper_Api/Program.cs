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

// ? HABILITAR COMPRESIÓN PARA SOMEE
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

// ? TU BASE DE DATOS
builder.Services.AddDbContext<VaperContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// =======================
// ? MANEJADOR GLOBAL DE EXCEPCIONES (SEGURIDAD)
// =======================
// Esto evita que si hay un error en C#, el código fuente o las consultas SQL se filtren hacia el navegador.
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = 500; // Internal Server Error
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            // Opcional: Aquí podrías guardar el contextFeature.Error en un log interno.
            
            // Retornamos un mensaje de error genérico en formato JSON
            await context.Response.WriteAsync("{\"message\":\"Ocurrió un error interno en el servidor. Por favor, inténtelo de nuevo más tarde.\"}");
        }
    });
});

// ? SWAGGER SIEMPRE ACTIVO
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vaper API v1");
    c.RoutePrefix = string.Empty;
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// ? ACTIVAR COMPRESIÓN (Debe ir antes del ruteo/autorización)
app.UseResponseCompression();

app.UseAuthorization();
app.MapControllers();
app.Run();