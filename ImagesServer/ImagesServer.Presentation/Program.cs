using Application.Options;
using ImagesServer.Extensions;
using ImagesServer.IoC;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("cors_api", policy => { policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Services.RunMigrations();
}

app.UseCors("cors_api");

app.MapControllers();

app.Run();