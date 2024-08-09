/* using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Services;
using DatingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class IdentyServiceExtensions
    {
       public static IServiceCollection  AddApplicationServices(this IServiceCollection services,IConfiguration config){
        services.AddScoped<ITokenService,TokenService>();
        services.AddDbContext<DataContext>(options =>{
          var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        });

       }
    }
} */