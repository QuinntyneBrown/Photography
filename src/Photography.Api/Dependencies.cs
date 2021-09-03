using Photography.Api.Data;
using Photography.Api.Extensions;
using Photography.Api.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.AspNetCore.Hosting;
using Photography.Api.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace Photography.Api
{
    public static class Dependencies
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Photography",
                    Description = "Photography Sample App",
                    TermsOfService = new Uri("https://photography.quinntynebrown.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Quinntyne Brown",
                        Email = "quinntynebrown@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                });

                options.CustomSchemaIds(x => x.FullName);
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(isOriginAllowed: _ => true)
                .AllowCredentials()));

            services.AddValidation(typeof(Startup));

            services.AddHttpContextAccessor();

            services.AddMediatR(typeof(IPhotographyDbContext));

            services.AddTransient<IPhotographyDbContext, PhotographyDbContext>();

            services.AddDbContext<PhotographyDbContext>(options =>
            {
                options.UseInMemoryDatabase(nameof(Photography.Api))
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging();
            });

            services.AddControllers();
        }

        public static void ConfigureAuth(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddSingleton<ITokenProvider, TokenProvider>();

            services.AddTransient<ITokenBuilder, TokenBuilder>();

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler
            {
                InboundClaimTypeMap = new Dictionary<string, string>()
            };

            if (webHostEnvironment.IsDevelopment() || webHostEnvironment.IsProduction())
            {
                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.SecurityTokenValidators.Clear();
                    options.SecurityTokenValidators.Add(jwtSecurityTokenHandler);
                    options.TokenValidationParameters = GetTokenValidationParameters(configuration);
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Request.Query.TryGetValue("access_token", out StringValues token);

                            if (!string.IsNullOrEmpty(token)) context.Token = token;

                            return Task.CompletedTask;
                        }
                    };
                });
            }
        }

        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration[$"{nameof(Authentication)}:{nameof(Authentication.JwtKey)}"])),
                ValidateIssuer = true,
                ValidIssuer = configuration[$"{nameof(Authentication)}:{nameof(Authentication.JwtIssuer)}"],
                ValidateAudience = true,
                ValidAudience = configuration[$"{nameof(Authentication)}:{nameof(Authentication.JwtAudience)}"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                NameClaimType = JwtRegisteredClaimNames.UniqueName
            };

            return tokenValidationParameters;
        }
    }

}
