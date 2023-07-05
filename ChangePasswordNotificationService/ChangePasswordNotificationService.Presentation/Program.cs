using System.Reflection;
using ChangePasswordNotificationService.IoC;
using ChangePasswordNotificationService.Main;
using Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var config = new ConfigurationBuilder().SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables().Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingletonOptions(config);
        services.AddDependencies();
        services.AddHostedService<Main>();
    });

var app = host.Build();

await app.RunAsync();