using Luftborn.Data;
using Luftborn.UnitofWorkF;
using Microsoft.EntityFrameworkCore;
namespace Luftborn.Extensisons;
public static class ApplicationServices
{

    public static  IServiceCollection addApplicationService(this IServiceCollection services, IConfiguration config)
    {


                    services.AddControllers();
                    services.AddEndpointsApiExplorer();
                    services.AddDbContext<DataContext>(options=>{
                    options.UseSqlServer(config.GetConnectionString("DefaultConnectionString"));
                            
                    });
                services.AddCors();
                // here we will add all the services as this will be the service container file 

                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies() );
                services.AddScoped<IUnitofWork,UnitofWork>();
        return services;

    }


}