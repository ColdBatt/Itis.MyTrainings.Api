using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Services;
using Itis.MyTrainings.Api.PostgreSql;
using MediatR;
using Microsoft.OpenApi.Models;

namespace Itis.MyTrainings.Api.Web.Extensions;

public static class CoreConfigurator
{
    /// <summary>
    /// Добавить службы проекта
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigureCore(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(typeof(User));

        builder.Services.AddScoped<IDbContext, EfContext>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services
            .AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<EfContext>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCustomSwagger();
    }
    
    private static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        => services
            .AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });
            });
}