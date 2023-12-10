using System.Security.Claims;
using Itis.MyTrainings.Api.Core.Constants;

namespace Itis.MyTrainings.Api.Web.Extensions;

public static class AuthConfigurator
{
    /// <summary>
    /// Добавить и настроить авторизацию
    /// </summary>
    /// <param name="builder">WebApplicationBuilder</param>
    public static void ConfigureAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthorization(opt =>
            {
                opt.AddPolicy(Roles.Administrator, policyBuilder =>
                {
                    policyBuilder.RequireClaim(ClaimTypes.Role, Roles.Administrator);
                });
                opt.AddPolicy(Roles.User, policyBuilder =>
                {
                    policyBuilder
                        .RequireAssertion(
                            x => 
                                x.User.HasClaim(ClaimTypes.Role, Roles.User) || 
                                x.User.HasClaim(ClaimTypes.Role, Roles.Administrator));
                });
            });
    }
}