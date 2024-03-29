﻿using Microsoft.OpenApi.Models;

namespace TechChallenge.Api.Settings
{
    public class SwaggerSettings
    {
        public static void AddSwaggerSetup(WebApplicationBuilder builder)
        {
            if (builder.Services == null) throw new ArgumentNullException(nameof(builder.Services));

            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api Tech Challenge FASE-3",
                    Description = "FIAP - BLOG",
                    Contact = new OpenApiContact { Name = "STI", Email = "" }
                });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header",
                    Name = "Autorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }
        public static void UseSwaggerSetup(WebApplication app)
        {
            if (app == null) throw new ArgumentException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProject");
            });
        }
    }
}
