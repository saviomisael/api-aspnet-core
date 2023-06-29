using System.Reflection;
using GamesWebApi.IoC;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("GamesWebApi"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("GamesWebApi.Presentation");
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    });
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("cors_api", policy => { policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddInfraDependencies();
builder.Services.AddAppDependencies();
builder.Services.AddValidatorDependencies();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Games API Rest",
        Description = "An API Rest for creation of reviews for games."
    });

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

app.UseCors("cors_api");

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.Services.RunMigrations();
}

app.Run();