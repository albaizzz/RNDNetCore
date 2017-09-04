using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace bal.business
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