using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace webapi.business.BAL
{
    
    public class BaseBAL
    {
        protected IConfiguration Configuration;
        public BaseBAL()
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }    
    }
}