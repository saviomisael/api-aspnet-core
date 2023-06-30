using System.Reflection;
using Application.Options;
using ImagesServer.Extensions;
using ImagesServer.IoC;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 5 * 1024 * 1024;
});

var domainOptions = new DomainOptions();
builder.Configuration.GetSection("DomainOptions").Bind(domainOptions);
builder.Services.AddSingleton(domainOptions);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ImagesServerDB"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("ImagesServer.Presentation");
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    });
});
builder.Services.AddInfraDependencies();
builder.Services.AddApplicationDependencies();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("cors_api", policy => { policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Images Server",
        Description = "An Images Server for saving images."
    });
    
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Services.RunMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors_api");

app.MapControllers();

app.Run();