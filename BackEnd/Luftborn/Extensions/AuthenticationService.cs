using Luftborn.Data;
using Luftborn.UnitofWorkF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
namespace Luftborn.Extensisons;
public static class AuthenticationService
{

    public static  IServiceCollection AuthenticationServices(this IServiceCollection services, IConfiguration config)
    {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApi(config.GetSection("AzureAd"));
        return services;

    }


}