using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace webapi.data.DataAccess
{      
    public class BaseDAL
    {
        public static IConfiguration Configuration; 
        public BaseDAL()
        {
            var builder = new ConfigurationBuilder();
            builder.Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
            .Build();
            Configuration = builder.Build();
        }
    }
}