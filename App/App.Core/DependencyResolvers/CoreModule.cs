using App.Core.ApiDoc;
using App.Core.CrossCuttingConcerns.Caching;
using App.Core.CrossCuttingConcerns.Caching.Memory;
using App.Core.Utilities.IoC;
using App.Core.Utilities.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace App.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddTransient<ITokenHelper, JwtHelper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Stopwatch>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Movie API",
                    Description = "Movie Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Halil Koca",
                        Email = string.Empty,
                        Url = new Uri("http://halilkoca.com")
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = "App.Api.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.UseInlineDefinitionsForEnums();

                c.OperationFilter<AddAuthHeaderOperationFilter>();
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "`Token only!!!` - without `Bearer_` prefix",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
            });
        }
    }
}
