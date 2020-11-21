using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DiaryApp.API.Extensions.ConfigureServices
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                var security =
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    },
                                    UnresolvedReference = true
                                },
                            new List<string>()
                        }
                    };
                c.AddSecurityRequirement(security);
            });
        }
    }
}
