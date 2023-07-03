using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Domain.Entity;
using GamesWebApi.IoC;
using GamesWebApi.Options;
using Infrastructure.Data;
using Infrastructure.ImagesServerApi;
using Infrastructure.ImagesServerApi.Contracts;
using Infrastructure.ImagesServerApi.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("GamesWebApi"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("GamesWebApi.Presentation");
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
    });
});
builder.Services.AddIdentity<Reviewer, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("cors_api", policy => { policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); });
});

var imagesServerOptions = new ImagesServerOptions();
builder.Configuration.GetSection("ImagesServerOptions").Bind(imagesServerOptions);
builder.Services.AddSingleton(imagesServerOptions);
builder.Services.AddHttpClient<IImagesServerApiClient, ImagesServerApiClient>();

builder.Services.AddInfraDependencies();
builder.Services.AddAppDependencies();
builder.Services.AddValidatorDependencies();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "games_api_issuer",
            ValidAudience = "games_api_audience",
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<JwtOptions>("JWT").Key))
        };
    });
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Games API Rest",
        Description = "An API Rest for creation of reviews for games."
    });

    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Services.RunMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dataSeeder = scope.ServiceProvider.GetService<DataSeeder>();
dataSeeder?.Seed();

app.UseCors("cors_api");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();