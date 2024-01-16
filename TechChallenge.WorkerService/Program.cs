using MassTransit;
using TechChallenge.Api.Settings;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Contracts.Services;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Service;
using TechChallenge.Infrastructure.Repositories;
using TechChallenge.WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        AppSettings.ConnectionStringServiceBus = configuration.GetSection("AzureServiceBus:ConnectionString").Value ?? string.Empty;
        AppSettings.NomeFilaServiceBus = configuration.GetSection("AzureServiceBus:NomeFila").Value ?? string.Empty;
        AppSettings.ConnectionStrings = configuration.GetSection("ConnectionStrings:UsuarioConnection").Value ?? string.Empty;

        services.AddTransient<IUsuarioApplicationService, UsuarioApplicationService>();
        services.AddTransient<INoticiaApplicationService, NoticiaApplicationService>();
        services.AddTransient<IAzureBlobApplicationService, AzureBlobApplicationService>();
        services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
        services.AddTransient<INoticiaDomainService, NoticiaDomainService>();
        services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        services.AddTransient<INoticiaRepository, NoticiaRepository>();
        services.AddScoped<TokenApplicationService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
