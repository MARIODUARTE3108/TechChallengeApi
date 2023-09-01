using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

var connectionString = builder.Configuration.GetConnectionString("UsuarioConnection");
string securityKey = builder.Configuration.GetSection("AppSettings:SecurityKey").Value;
builder.Services.AddDbContext<SqlServerContext>
    (opts =>
    {
        opts.UseSqlServer(connectionString);
    });



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
