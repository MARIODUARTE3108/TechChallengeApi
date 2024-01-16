using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechChallenge.Api.Properties;
using TechChallenge.Api.Settings;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.Services;
using TechChallenge.Domain.Contracts.Datas;
using TechChallenge.Domain.Contracts.Services;
using TechChallenge.Domain.Entities;
using TechChallenge.Domain.Service;
using TechChallenge.Infrastructure.Context;
using TechChallenge.Infrastructure.Repositories;



var builder = WebApplication.CreateBuilder(args);

AppSettings.ConnectionStrings = builder.Configuration.GetSection("ConnectionStrings:UsuarioConnection").Value ?? string.Empty;
AppSettings.ConnectionStringServiceBus = builder.Configuration.GetSection("AzureServiceBus:ConnectionString").Value ?? string.Empty;
AppSettings.NomeFilaServiceBus = builder.Configuration.GetSection("AzureServiceBus:NomeFila").Value ?? string.Empty;

string securityKey = builder.Configuration.GetSection("AppSettings:SecurityKey").Value ?? string.Empty;

builder.Services.AddMassTransit((x =>
{
    x.UsingAzureServiceBus((context, config) =>
    {
        config.Host(AppSettings.ConnectionStringServiceBus);
    });
}
));

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey)),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };

});
builder.Services.AddAuthorization();
builder.Services.AddTransient<IUsuarioApplicationService, UsuarioApplicationService>();
builder.Services.AddTransient<INoticiaApplicationService, NoticiaApplicationService>();
builder.Services.AddTransient<IAzureBlobApplicationService, AzureBlobApplicationService>();
builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
builder.Services.AddTransient<INoticiaDomainService, NoticiaDomainService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<INoticiaRepository, NoticiaRepository>(); 
builder.Services.AddScoped<TokenApplicationService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
SwaggerSettings.AddSwaggerSetup(builder);
CorsSettings.AddCorsSetup(builder);

var app = builder.Build();

SwaggerSettings.UseSwaggerSetup(app);
CorsSettings.UseCorsSetup(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }